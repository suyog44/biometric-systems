using System;
using System.IO;
using Neurotec.Biometrics;
using Neurotec.IO;

namespace Neurotec.Samples.Connections
{
	public class DirectoryEnumerator : ITemplateLoader
	{
		#region Private fields

		private readonly object _lockObject = new object();
		private readonly string[] _files;

		private int _index = -1;

		#endregion

		#region Public constructor

		public DirectoryEnumerator(string directory)
		{
			if (directory == null || !Directory.Exists(directory))
				throw new ArgumentException("Directory doesn't exist");

			Dir = directory;
			_files = Directory.GetFiles(Dir);
		}

		#endregion

		#region Public properties

		public string Dir { get; private set; }

		#endregion

		#region ITemplateLoader members

		public void BeginLoad()
		{
			lock (_lockObject)
			{
				if (_index != -1) throw new InvalidOperationException();
				_index = 0;
			}
		}

		public void EndLoad()
		{
			lock (_lockObject)
			{
				_index = -1;
			}
		}

		public bool LoadNext(out NSubject[] subjects, int n)
		{
			lock (_lockObject)
			{
				if (_index == -1) throw new InvalidOperationException();
				subjects = null;
				if (_files.Length == 0 || _files.Length <= _index) return false;

				int count = _files.Length - _index;
				count = count > n ? n : count;
				subjects = new NSubject[count];
				for (int i = 0; i < count; i++)
				{
					string file = _files[_index++];
					subjects[i] = new NSubject();
					subjects[i].SetTemplateBuffer(new NBuffer(File.ReadAllBytes(file)));
					subjects[i].Id = Path.GetFileNameWithoutExtension(file);
				}
				return true;
			}
		}

		public void Dispose()
		{
		}

		public int TemplateCount
		{
			get { return _files.Length; }
		}

		#endregion

		#region Public methods

		public override string ToString()
		{
			return Dir;
		}

		#endregion
	}
}
