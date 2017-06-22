using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Neurotec.Samples.TenPrintCard
{
	public enum TwainCommand
	{
		Not				= -1,
		Null			= 0,
		TransferReady	= 1,
		CloseRequest	= 2,
		CloseOk			= 3,
		DeviceEvent		= 4
	}

	public class Twain
	{
		private const short CountryUSA		= 1;
		private const short LanguageUSA		= 13;

		public Twain()
		{
			_appid = new TwIdentity();
			_appid.Id				= IntPtr.Zero;
			_appid.Version.MajorNum	= 1;
			_appid.Version.MinorNum	= 1;
			_appid.Version.Language	= LanguageUSA;
			_appid.Version.Country	= CountryUSA;
			_appid.Version.Info		= "1.0";
			_appid.ProtocolMajor		= TwProtocol.Major;
			_appid.ProtocolMinor		= TwProtocol.Minor;
			_appid.SupportedGroups	= (int)(TwDG.Image | TwDG.Control);
			_appid.Manufacturer		= "Neurotechnology";
			_appid.ProductFamily		= "SDK";
			_appid.ProductName		= "AbisSampleCS";

			_srcds = new TwIdentity();
			_srcds.Id = IntPtr.Zero;

			_evtmsg.EventPtr = Marshal.AllocHGlobal( Marshal.SizeOf( _winmsg ) );
		}

		~Twain()
		{
			Marshal.FreeHGlobal(_evtmsg.EventPtr);
		}

		public bool Init( IntPtr hwndp )
		{
			Finish();
			TwRC rc = DSMparent( _appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.OpenDSM, ref hwndp );
			if (rc == TwRC.Success)
			{
				rc = DSMident(_appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetDefault, _srcds);
				if (rc == TwRC.Success)
					_hwnd = hwndp;
				else
					rc = DSMparent(_appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwndp);

				if (rc == TwRC.Success)
					return true;
			}
			else
				return false;

			return true;
		}

		public bool Select()
		{
			CloseSrc();
			if( _appid.Id == IntPtr.Zero )
			{
				Init( _hwnd );
				if( _appid.Id == IntPtr.Zero )
					return false;
			}
			TwRC rc = DSMident( _appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.UserSelect, _srcds );

			return rc == TwRC.Success;
		}

		public bool Acquire()
		{
			CloseSrc();
			if( _appid.Id == IntPtr.Zero )
			{
				Init( _hwnd );
				if( _appid.Id == IntPtr.Zero )
					return false;
			}
			TwRC rc = DSMident( _appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, _srcds );
			if( rc != TwRC.Success )
				return false;

			TwCapability cap = new TwCapability( TwCap.XferCount, 1 );
			rc = DScap( _appid, _srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap );
			if( rc != TwRC.Success )
			{
				CloseSrc();
				return false;
			}

			TwUserInterface	guif = new TwUserInterface();
			guif.ShowUI = 1;
			guif.ModalUI = 1;
			guif.ParentHand = _hwnd;
			rc = DSuserif( _appid, _srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif );
			if( rc != TwRC.Success )
			{
				CloseSrc();
				return false;
			}

			return true;
		}

		public class TransferedPicture
		{
			public TransferedPicture(IntPtr bitmap, int xres, int yres)
			{
				hBitmap = bitmap;
				xResolution = xres;
				yResolution = yres;
			}

			public IntPtr hBitmap;
			public int xResolution;
			public int yResolution;
		}

		public List<TransferedPicture> TransferPictures()
		{
			List<TransferedPicture> pics = new List<TransferedPicture>();
			if( _srcds.Id == IntPtr.Zero )
				return pics;

			TwRC rc;
			TwPendingXfers pxfr = new TwPendingXfers();
			
			do
			{
				pxfr.Count = 0;
				IntPtr hbitmap = IntPtr.Zero;

				TwImageInfo	iinf = new TwImageInfo();
				rc = DSiinf( _appid, _srcds, TwDG.Image, TwDAT.ImageInfo, TwMSG.Get, iinf );
				if( rc != TwRC.Success )
				{
					CloseSrc();
					return pics;
				}

				rc = DSixfer( _appid, _srcds, TwDG.Image, TwDAT.ImageNativeXfer, TwMSG.Get, ref hbitmap );
				if( rc != TwRC.XferDone )
				{
					CloseSrc();
					return pics;
				}

				rc = DSpxfer( _appid, _srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pxfr );
				if( rc != TwRC.Success )
				{
					CloseSrc();
					return pics;
				}

				pics.Add(new TransferedPicture(hbitmap, iinf.XResolution, iinf.YResolution));
			}
			while( pxfr.Count != 0 );

			rc = DSpxfer( _appid, _srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pxfr );
			if (rc != TwRC.Success)
			{
				CloseSrc();
				return pics;
			}
			return pics;
		}

		public TwainCommand PassMessage( ref Message m )
		{
			if( _srcds.Id == IntPtr.Zero )
				return TwainCommand.Not;

			int pos = GetMessagePos();

			_winmsg.hwnd		= m.HWnd;
			_winmsg.message	= m.Msg;
			_winmsg.wParam	= m.WParam;
			_winmsg.lParam	= m.LParam;
			_winmsg.time		= GetMessageTime();
			_winmsg.x		= (short) pos;
			_winmsg.y		= (short) (pos >> 16);
			
			Marshal.StructureToPtr( _winmsg, _evtmsg.EventPtr, false );
			_evtmsg.Message = 0;
			TwRC rc = DSevent( _appid, _srcds, TwDG.Control, TwDAT.Event, TwMSG.ProcessEvent, ref _evtmsg );
			if( rc == TwRC.NotDSEvent )
				return TwainCommand.Not;
			if( _evtmsg.Message == (short) TwMSG.XFerReady )
				return TwainCommand.TransferReady;
			if( _evtmsg.Message == (short) TwMSG.CloseDSReq )
				return TwainCommand.CloseRequest;
			if( _evtmsg.Message == (short) TwMSG.CloseDSOK )
				return TwainCommand.CloseOk;
			if( _evtmsg.Message == (short) TwMSG.DeviceEvent )
				return TwainCommand.DeviceEvent;

			return TwainCommand.Null;
		}

		public void CloseSrc()
		{
			TwRC rc;
			if( _srcds.Id != IntPtr.Zero )
			{
				TwUserInterface	guif = new TwUserInterface();
				rc = DSuserif( _appid, _srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.DisableDS, guif );
				rc = DSMident( _appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.CloseDS, _srcds );
			}
		}

		public void Finish()
		{
			TwRC rc;
			CloseSrc();
			if( _appid.Id != IntPtr.Zero )
				rc = DSMparent( _appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref _hwnd );
			_appid.Id = IntPtr.Zero;
		}

		private IntPtr		_hwnd;
		private readonly TwIdentity	_appid;
		private readonly TwIdentity	_srcds;
		private TwEvent		_evtmsg;
		private WINMSG		_winmsg;

		// ------ DSM entry point DAT_ variants:
		[DllImport("twain_32.dll", EntryPoint="#1")]
		private static extern TwRC DSMparent( [In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr refptr );

		[DllImport("twain_32.dll", EntryPoint="#1")]
		private static extern TwRC DSMident( [In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwIdentity idds );

		[DllImport("twain_32.dll", EntryPoint="#1")]
		private static extern TwRC DSMstatus( [In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat );

		// ------ DSM entry point DAT_ variants to DS:
		[DllImport("twain_32.dll", EntryPoint="#1")]
		private static extern TwRC DSuserif( [In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwUserInterface guif );

		[DllImport("twain_32.dll", EntryPoint="#1")]
		private static extern TwRC DSevent( [In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref TwEvent evt );

		[DllImport("twain_32.dll", EntryPoint="#1")]
		private static extern TwRC DSstatus( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat );

		[DllImport("twain_32.dll", EntryPoint="#1")]
		private static extern TwRC DScap( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwCapability capa );

		[DllImport("twain_32.dll", EntryPoint="#1")]
		private static extern TwRC DSiinf( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwImageInfo imginf );

		[DllImport("twain_32.dll", EntryPoint="#1")]
		private static extern TwRC DSixfer( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hbitmap );

		[DllImport("twain_32.dll", EntryPoint="#1")]
		private static extern TwRC DSpxfer( [In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwPendingXfers pxfr );

		[DllImport("kernel32.dll", ExactSpelling=true)]
		internal static extern IntPtr GlobalAlloc( int flags, int size );
		[DllImport("kernel32.dll", ExactSpelling=true)]
		internal static extern IntPtr GlobalLock( IntPtr handle );
		[DllImport("kernel32.dll", ExactSpelling=true)]
		internal static extern bool GlobalUnlock( IntPtr handle );
		[DllImport("kernel32.dll", ExactSpelling=true)]
		internal static extern IntPtr GlobalFree( IntPtr handle );

		[DllImport("user32.dll", ExactSpelling=true)]
		private static extern int GetMessagePos();
		[DllImport("user32.dll", ExactSpelling=true)]
		private static extern int GetMessageTime();

		[DllImport("gdi32.dll", ExactSpelling=true)]
		private static extern int GetDeviceCaps( IntPtr hDC, int nIndex );

		[DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		private static extern IntPtr CreateDC( string szdriver, string szdevice, string szoutput, IntPtr devmode );

		[DllImport("gdi32.dll", ExactSpelling=true)]
		private static extern bool DeleteDC( IntPtr hdc );

		public static int ScreenBitDepth
		{
			get
			{
				IntPtr screenDc = CreateDC( "DISPLAY", null, null, IntPtr.Zero );
				int bitDepth = GetDeviceCaps( screenDc, 12 );
				bitDepth *= GetDeviceCaps( screenDc, 14 );
				DeleteDC( screenDc );
				return bitDepth;
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack=4)]
		internal struct WINMSG
		{
			public IntPtr		hwnd;
			public int			message;
			public IntPtr		wParam;
			public IntPtr		lParam;
			public int			time;
			public int			x;
			public int			y;
		}
	} // class Twain
}
