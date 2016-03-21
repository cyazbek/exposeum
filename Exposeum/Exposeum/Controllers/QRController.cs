﻿using System;
using Android.App;
using ZXing.Mobile;
using Android.Content;

namespace Exposeum.Controllers
{
	public class QRController
	{
		private static QRController _instance;
		private MobileBarcodeScanner _scanner;
		private Context _context;

		public static QRController GetInstance(Context context){
			if (_instance == null)
				_instance = new QRController (context);
			return _instance;
		}

		private QRController (Context context)
		{
			this._context = context;
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

		private void HandleScanResult(ZXing.Result result)
		{
			if (result != null && !string.IsNullOrEmpty (result.Text) && Android.Util.Patterns.WebUrl.Matcher(result.Text).Matches()) {

				_context.StartActivity(Intent.CreateChooser(new Intent(Intent.ActionView, Android.Net.Uri.Parse (result.Text.ToLower())), String.Empty));

			}
		}
	}
}