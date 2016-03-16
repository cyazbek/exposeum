using System;
using Android.App;
using ZXing.Mobile;

namespace Exposeum.Controllers
{
	public class QRController
	{
		private QRController _instance;
		private MobileBarcodeScanner _scanner;

		public static QRController GetInstance(){
			if (_instance == null)
				_instance = new QRController ();
			return _instance;
		}

		public QRController ()
		{
			MobileBarcodeScanner.Initialize (Application);
			_scanner = new MobileBarcodeScanner ();
		}
	}
}
