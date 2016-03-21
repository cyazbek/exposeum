using System;
using Android.App;
using ZXing.Mobile;
using Android.Content;

namespace Exposeum.Controllers
{
	public class QrController
	{
		private static QrController _instance;
		private readonly MobileBarcodeScanner _scanner;
		private readonly Context _context;

		public static QrController GetInstance(Context context){
			if (_instance == null)
				_instance = new QrController (context);
			return _instance;
		}

		private QrController (Context context)
		{
			_context = context;
			MobileBarcodeScanner.Initialize (((Activity)context).Application);

		    _scanner = new MobileBarcodeScanner
		    {
		        UseCustomOverlay = false,
		        TopText = "Top Text",
		        BottomText = "Bottom Text"
		    };
		}

		public async void BeginQrScanning()
		{
			var result = await _scanner.Scan();
			HandleScanResult (result);
		}

		private void HandleScanResult(ZXing.Result result)
		{
			if (result != null && !string.IsNullOrEmpty (result.Text) && Android.Util.Patterns.WebUrl.Matcher(result.Text).Matches()) {

				_context.StartActivity(Intent.CreateChooser(new Intent(Intent.ActionView, Android.Net.Uri.Parse (result.Text.ToLower())), String.Empty));

			}
		}
	}
}
