using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Gui;
using Neurotec.Images;

namespace Neurotec.Samples
{
	public partial class BiometricPreviewPage : Neurotec.Samples.PageBase
	{
		#region Public constructor

		public BiometricPreviewPage()
		{
			InitializeComponent();
			DoubleBuffered = true;
		}

		#endregion

		#region Private fields

		private SubjectTreeControl.Node _node;
		private NBiometric _biometric;
		private object _view;

		#endregion

		#region Public methods

		public override void OnNavigatedTo(params object[] args)
		{
			if (args == null || args.Length != 1) throw new ArgumentException("args");

			bool showZoomControls = true;
			bool showBinarizedCheckbox = false;
			bool canSave = false;
			_view = null;
			_node = (SubjectTreeControl.Node)args[0];
			if (_node == null || !_node.IsBiometricNode) throw new ArgumentException("args");
			panelView.Controls.Clear();
			generalizeProgressView.Clear();
			generalizeProgressView.Visible = _node.IsGeneralizedNode;
			icaoWarningView.Visible = false;
			_biometric = _node.Items.First();
			switch (_node.BiometricType)
			{
				case NBiometricType.Finger:
				case NBiometricType.Palm:
					{
						NFrictionRidge first = _biometric as NFrictionRidge;
						NFingerView view = new NFingerView()
						{
							Dock = DockStyle.Fill,
							ForeColor = Color.Red,
							Finger = first
						};
						_view = view;
						panelView.Controls.Add(view);
						horizontalZoomSlider.View = view;
						showBinarizedCheckbox = first.BinarizedImage != null;
						canSave = first.Image != null;
						if (generalizeProgressView.Visible)
						{
							var generalized = _node.GetAllGeneralized();
							generalizeProgressView.View = view;
							generalizeProgressView.Biometrics = _node.Items;
							generalizeProgressView.Generalized = generalized;
							generalizeProgressView.Selected = generalized.FirstOrDefault() ?? first;
						}

						if (showBinarizedCheckbox && chbShowBinarized.Checked)
							view.ShownImage = ShownImage.Result;
						break;
					}
				case NBiometricType.Iris:
					{
						NIrisView view = new NIrisView()
						{
							Dock = DockStyle.Fill,
							Iris = _biometric as NIris
						};
						panelView.Controls.Add(view);
						horizontalZoomSlider.View = view;
						_view = view;
						canSave = view.Iris.Image != null;
						break;
					}
				case NBiometricType.Face:
					{
						NFace first = _biometric as NFace;
						NFaceView view = new NFaceView()
						{
							Dock = DockStyle.Fill,
							ShowIcaoArrows = false,
							Face = first
						};
						_view = view;
						panelView.Controls.Add(view);
						horizontalZoomSlider.View = view;
						canSave = view.Face.Image != null;
						if (_node.AllItems.Cast<NFace>().Any(WasIcaoCheckPerformed))
						{
							icaoWarningView.Visible = true;
						}
						if (generalizeProgressView.Visible)
						{
							var generalized = _node.GetAllGeneralized();
							generalizeProgressView.View = view;
							generalizeProgressView.IcaoView = icaoWarningView;
							generalizeProgressView.Biometrics = _node.Items;
							generalizeProgressView.Generalized = generalized;
							generalizeProgressView.Selected = generalized.FirstOrDefault() ?? first;
						}
						else
						{
							icaoWarningView.Face = first;
						}
						break;
					}
				case NBiometricType.Voice:
					{
						showZoomControls = false;
						VoiceView view = new VoiceView() { Voice = _biometric as NVoice };
						panelView.Controls.Add(view);
						_view = view;
						canSave = view.Voice.SoundBuffer != null;
						break;
					}
			};

			horizontalZoomSlider.Visible = showZoomControls;
			chbShowBinarized.Visible = showBinarizedCheckbox;
			btnSave.Visible = canSave;
			btnSave.Text = _node.BiometricType == NBiometricType.Voice ? "Save audio file" : "Save image";

			generalizeProgressView.PropertyChanged += GeneralizeProgressViewPropertyChanged;

			base.OnNavigatedTo(args);
		}

		public override void OnNavigatingFrom()
		{
			generalizeProgressView.PropertyChanged -= GeneralizeProgressViewPropertyChanged;

			panelView.Controls.Clear();
			IDisposable viewControl = _view as IDisposable;
			if (viewControl != null)
				viewControl.Dispose();
			_view = null;
			icaoWarningView.Face = null;
			generalizeProgressView.IcaoView = null;
			generalizeProgressView.View = null;

			base.OnNavigatingFrom();
		}

		#endregion

		#region Private static methods

		private static bool WasIcaoCheckPerformed(NFace face)
		{
			var attributes = face.Objects.FirstOrDefault();
			if (attributes != null && !attributes.TokenImageRect.IsEmpty)
			{
				return true;
			}
			else
			{
				var parentObject = (NLAttributes)face.ParentObject;
				return parentObject != null && !parentObject.TokenImageRect.IsEmpty;
			}
		}

		#endregion

		#region Private events

		private void GeneralizeProgressViewPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Selected")
			{
				if (_node.BiometricType == NBiometricType.Finger || _node.BiometricType == NBiometricType.Palm)
				{
					NFingerView view = (NFingerView)_view;
					NFrictionRidge finger = view.Finger;
					_biometric = finger;
					chbShowBinarized.Visible = finger.BinarizedImage != null;
					if (!chbShowBinarized.Visible && chbShowBinarized.Checked)
						chbShowBinarized.Checked = false;
				}
				else if (_node.BiometricType == NBiometricType.Face)
				{
					NFaceView view = (NFaceView)_view;
					_biometric = view.Face;
				}
			}
		}

		private void BtnFinishClick(object sender, EventArgs e)
		{
			PageController.NavigateToStartPage();
			panelView.Controls.Clear();
		}

		private void ChbShowBinarizedCheckedChanged(object sender, EventArgs e)
		{
			NFingerView view = _view as NFingerView;
			if (view != null)
			{
				view.ShownImage = chbShowBinarized.Checked ? ShownImage.Result : ShownImage.Original;
			}
		}

		private void BtnSaveClick(object sender, EventArgs e)
		{
			saveFileDialog.Filter = _biometric is NVoice ? string.Empty : NImages.GetSaveFileFilterString();
			saveFileDialog.FileName = string.Empty;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if (_biometric.BiometricType == NBiometricType.Voice)
					{
						NVoice voice = _biometric as NVoice;
						voice.SoundBuffer.Save(saveFileDialog.FileName);
					}
					else
					{
						NImage image = null;
						if (_biometric.BiometricType == NBiometricType.Finger || _biometric.BiometricType == NBiometricType.Palm)
						{
							NFrictionRidge frictionRidge = (NFrictionRidge)_biometric;
							image = chbShowBinarized.Checked ? frictionRidge.BinarizedImage : frictionRidge.Image;
						}
						else if (_biometric.BiometricType == NBiometricType.Iris)
						{
							image = ((NIris)_biometric).Image;
						}
						else
						{
							image = ((NFace)_biometric).Image;
						}
						image.Save(saveFileDialog.FileName);
					}
				}
				catch (Exception ex)
				{
					Utilities.ShowError(ex);
				}
			}
		}

		#endregion
	}
}
