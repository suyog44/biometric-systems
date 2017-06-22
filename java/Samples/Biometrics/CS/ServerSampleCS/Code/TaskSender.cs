using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Samples.Connections;

namespace Neurotec.Samples.Code
{
	public delegate void Finished();
	public delegate void ErrorOccured(Exception ex);
	public delegate void ProgressChanged();
	public delegate void MatchingTasksCompleted(NBiometricTask[] tasks);

	public class TaskSender : UserControl
	{
		#region Public events

		public event Finished Finished;
		public event ProgressChanged ProgressChanged;
		public event ErrorOccured ExceptionOccured;
		public event MatchingTasksCompleted MatchingTasksCompleted;

		#endregion

		#region Public constructor

		public TaskSender(NBiometricClient biometricClient, ITemplateLoader templateLoader, NBiometricOperations operation)
		{
			BunchSize = 100;
			if (biometricClient == null) throw new ArgumentNullException("biometricClient");
			if (templateLoader == null) throw new ArgumentNullException("templateLoader");

			TemplateLoader = templateLoader;
			BiometricClient = biometricClient;
			_operation = operation;
			_worker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
			_worker.DoWork += WorkerDoWork;
			_worker.ProgressChanged += WorkerProgressChanged;
			_worker.RunWorkerCompleted += RunWorkerCompleted;
		}

		#endregion

		#region Private fields

		private readonly BackgroundWorker _worker;
		private readonly NBiometricOperations _operation;
		private readonly object _exceptionLock = new object();
		Queue<NBiometricTask> _completedTasks;

		private const int TasksAccumulate = 10;

		private int _tasksReceived;
		private int _maxActiveTaskCount = 1000;
		private long _activeTasks;
		private bool _stop;
		private ITemplateLoader _templateLoader;
		private Stopwatch _stopWatch;

		#endregion

		#region Public properties

		public int BunchSize { get; set; }

		public ITemplateLoader TemplateLoader
		{
			get { return _templateLoader; }
			set
			{
				if (IsBusy) throw new InvalidOperationException("Task sender is busy");
				_templateLoader = value;
			}
		}

		public bool Successful { get; private set; }

		public bool Canceled { get; private set; }

		public bool IsBusy
		{
			get
			{
				if (_worker == null) return false;
				return _worker.IsBusy;
			}
		}

		public bool SendOneBunchOnly { get; set; }

		public int PerformedTaskCount
		{
			get { return _tasksReceived; }
		}

		public TimeSpan ElapsedTime
		{
			get
			{
				if (_stopWatch == null)
					return TimeSpan.Zero;
				return _stopWatch.Elapsed;
			}
		}

		public NBiometricClient BiometricClient { get; set; }

		public bool IsAccelerator { get; set; }

		#endregion

		#region Public methods

		public void Start()
		{
			if (IsBusy) throw new InvalidOperationException("Already started");
			_stop = false;
			_tasksReceived = 0;
			Successful = true;
			Canceled = false;
			_stopWatch = new Stopwatch();
			_maxActiveTaskCount = IsAccelerator ? 1000 : 100;
			SettingsAccesor.SetMatchingParameters(BiometricClient);
			_worker.RunWorkerAsync();
		}

		public void Cancel()
		{
			Canceled = true;
			BiometricClient.Cancel();
			_worker.CancelAsync();
			_stop = true;
		}

		#endregion

		#region Private methods

		private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (Finished != null)
			{
				Finished();
			}
		}

		private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (ProgressChanged != null)
			{
				ProgressChanged();
			}
		}

		private void OnOperationCompleted(IAsyncResult r)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new AsyncCallback(OnOperationCompleted), r);
			}
			else
			{
				NBiometricTask task;
				try
				{
					task = BiometricClient.EndPerformTask(r);
				}
				catch (Exception ex)
				{
					FireExceptionOccuredEvent(ex);
					Successful = false;
					Interlocked.Decrement(ref _activeTasks);
					return;
				}
				NBiometricStatus status = task.Status;
				if (task.Error != null)
				{
					lock (_exceptionLock)
					{
						FireExceptionOccuredEvent(task.Error);
					}
				}
				if (status != NBiometricStatus.Ok && status != NBiometricStatus.MatchNotFound)
				{
					lock (_exceptionLock)
					{
						FireExceptionOccuredEvent(new Exception(status.ToString()));
					}
				}

				Interlocked.Add(ref _tasksReceived, task.Subjects.Count);
				_worker.ReportProgress(_tasksReceived);

				if (MatchingTasksCompleted != null)
				{
					lock (_completedTasks)
					{
						_completedTasks.Enqueue(task);
						if (_completedTasks.Count > TasksAccumulate)
						{
							MatchingTasksCompleted(_completedTasks.ToArray());
							_completedTasks.Clear();
						}
					}
				}
				else
				{
					task.Dispose();
				}
				Interlocked.Decrement(ref _activeTasks);
			}
		}

		private void WorkerDoWork(object sender, DoWorkEventArgs e)
		{
			var pendingTasks = new TaskQueue();
			long loaderWorking = 1;
			_completedTasks = new Queue<NBiometricTask>();

			try
			{
				TemplateLoader.BeginLoad();
			}
			catch (Exception ex)
			{
				FireExceptionOccuredEvent(ex);
				return;
			}

			#region Template loader thread

			var taskLoaderThread = new Thread(delegate()
						{
							try
							{
								while (!_stop)
								{
									if (pendingTasks.Count < 200)
									{
										NSubject[] subjects;
										if (TemplateLoader.LoadNext(out subjects, BunchSize))
										{
											if (_operation == NBiometricOperations.Identify)
											{
												foreach (var subject in subjects)
												{
													var task = BiometricClient.CreateTask(_operation, subject);
													pendingTasks.Enqueue(task);
													subject.Dispose();
												}
											}
											else
											{
												var task = BiometricClient.CreateTask(_operation, null);
												foreach (var subject in subjects)
												{
													task.Subjects.Add(subject);
													subject.Dispose();
												}
												pendingTasks.Enqueue(task);
											}
										}
										else break;
									}
									else
									{
										if (_stop) return;
										Thread.Sleep(250);
									}
									if (SendOneBunchOnly) return;
								}
							}
							catch (Exception ex)
							{
								lock (_exceptionLock)
								{
									FireExceptionOccuredEvent(ex);
								}
							}
							finally
							{
								Interlocked.Decrement(ref loaderWorking);
							}
						});
			taskLoaderThread.Start();

			#endregion

			if (SendOneBunchOnly)
				taskLoaderThread.Join();
			_stopWatch.Start();

			try
			{
				while (!_stop)
				{
					if (_activeTasks > _maxActiveTaskCount)
					{
						Thread.Sleep(500);
						continue;
					}

					NBiometricTask task;
					if ((task = pendingTasks.Dequeue()) == null)
					{
						if (Interlocked.Read(ref loaderWorking) == 0) break;
						Thread.Sleep(100);
						continue;
					}

					BiometricClient.BeginPerformTask(task, OnOperationCompleted, null);
					task.Dispose();
					Interlocked.Increment(ref _activeTasks);
				}
			}
			catch (Exception ex)
			{
				lock (_exceptionLock)
				{
					FireExceptionOccuredEvent(ex);
				}
			}

			while (Interlocked.Read(ref _activeTasks) > 0)
			{
				Thread.Sleep(100);
			}
			_stopWatch.Stop();

			if (MatchingTasksCompleted != null && _completedTasks.Count > 0)
			{
				MatchingTasksCompleted(_completedTasks.ToArray());
			}

			try
			{
				TemplateLoader.EndLoad();
			}
			catch (Exception ex)
			{
				FireExceptionOccuredEvent(ex);
			}

			if (!_worker.CancellationPending) return;

			Successful = false;
		}

		private void FireExceptionOccuredEvent(Exception ex)
		{
			Successful = false;
			if (ExceptionOccured != null)
			{
				ExceptionOccured.BeginInvoke(ex, null, null);
			}
		}

		#endregion

	}

	public class TaskQueue : Queue<NBiometricTask>
	{
		#region Private fields

		private readonly object _lockObject = new object();

		#endregion

		#region Public constructors

		public TaskQueue()
		{
		}

		public TaskQueue(IEnumerable<NBiometricTask> tasks)
			: base(tasks)
		{
		}

		#endregion

		#region Public methods

		public new NBiometricTask Dequeue()
		{
			lock (_lockObject)
			{
				return Count == 0 ? null : base.Dequeue();
			}
		}

		public new void Enqueue(NBiometricTask task)
		{
			lock (_lockObject)
			{
				base.Enqueue(task);
			}
		}

		#endregion
	}

}
