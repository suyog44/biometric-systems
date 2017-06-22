using System;
using System.Windows.Forms;
using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public partial class AddFaceImageForm : Form
	{
		public AddFaceImageForm()
		{
			InitializeComponent();

			string[] items = Enum.GetNames(typeof(FcrFaceImageType));
			cbFaceImageType.Items.AddRange(items);
			cbImageDataType.Items.AddRange(Enum.GetNames(typeof(FcrImageDataType)));
			cbFaceImageType.SelectedIndex = 0;
			cbImageDataType.SelectedIndex = 0;
		}

		public FcrFaceImageType FaceImageType
		{
			get
			{
				return (FcrFaceImageType)Enum.Parse(typeof(FcrFaceImageType), cbFaceImageType.SelectedItem.ToString());
			}
			set
			{
				cbFaceImageType.SelectedItem = Enum.GetName(typeof(FcrFaceImage), value);
			}
		}

		public FcrImageDataType ImageDataType
		{
			get
			{
				return (FcrImageDataType)Enum.Parse(typeof(FcrImageDataType), cbImageDataType.SelectedItem.ToString());
			}
			set
			{
				cbImageDataType.SelectedItem = Enum.GetName(typeof(FcrImageDataType), value);
			}
		}
	}
}
