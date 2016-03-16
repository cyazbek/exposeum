using System;
using Android.App;
using ZXing.Mobile;
using Android.Content;

namespace Exposeum.Controllers
{
	public class QRController
	{
		private static QRController _instance;
		private MobileBarcodeScanner _scanner;

		public static QRController GetInstance(Context context){
			if (_instance == null)
				_instance = new QRController (context);
			return _instance;
		}

		public QRController (Context context)
		{
			MobileBarcodeScanner.Initialize (((Activity)context).Application);

			_scanner = new MobileBarcodeScanner ();
			_scanner.UseCustomOverlay = false;
			_scanner.TopText = "Top Text";
			_scanner.BottomText = "Bottom Text";
		}

		public async void BeginQRScanning()
		{
			var result = await _scanner.Scan();
			HandleScanResult (result);
		}

		async void HandleScanResult(ZXing.Result result)
		{
			string msg = "";

			if (result != null && !string.IsNullOrEmpty(result.Text))
				msg = "Found Barcode: " + result.Text;
			else
				msg = "Scanning Canceled!";
		}
	}
}
