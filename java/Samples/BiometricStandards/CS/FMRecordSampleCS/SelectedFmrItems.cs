using Neurotec.Biometrics.Standards;

namespace Neurotec.Samples
{
	public class SelectedFmrItem
	{
		protected readonly FmrFingerView FingerView;
		protected readonly int Index;

		public SelectedFmrItem(FmrFingerView fingerView, int index)
		{
			FingerView = fingerView;
			Index = index;
		}
	}

	public class SelectedFmrDelta : SelectedFmrItem
	{
		public SelectedFmrDelta(FmrFingerView fingerView, int index)
			: base(fingerView, index)
		{
		}

		public FmrDelta Delta
		{
			get { return FingerView.Deltas[Index]; }
			set { FingerView.Deltas[Index] = value; }
		}
	}

	public class SelectedFmrCore : SelectedFmrItem
	{
		public SelectedFmrCore(FmrFingerView fingerView, int index)
			: base(fingerView, index)
		{
		}

		public FmrCore Core
		{
			get { return FingerView.Cores[Index]; }
			set { FingerView.Cores[Index] = value; }
		}
	}

	public class SelectedFmrMinutia : SelectedFmrItem
	{
		public SelectedFmrMinutia(FmrFingerView fingerView, int index)
			: base(fingerView, index)
		{
		}

		public FmrMinutia Minutia
		{
			get { return FingerView.Minutiae[Index]; }
			set { FingerView.Minutiae[Index] = value; }
		}
	}
}
