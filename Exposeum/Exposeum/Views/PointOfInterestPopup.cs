﻿using Android.Views;
using Android.Widget;
using Android.Webkit;
using Android.Content;
using Exposeum.Models;

namespace Exposeum.Views
{
	public class PointOfInterestPopup : View
	{
		private readonly PopupWindow _pwindow;
		public delegate void DismissCallback();
		private DismissCallback _dismissCallback;

		public PointOfInterestPopup (Context context, PointOfInterest poi) : base (context)
		{
			var inflater = LayoutInflater.From(context);
			View popupView  = inflater.Inflate(Resource.Layout.BeaconSummaryPopupView, null);

			popupView.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
			_pwindow = new PopupWindow(popupView, ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);

			Button dismissButton = popupView.FindViewById<Button>(Resource.Id.pointofinterest_popup_dismiss);
            dismissButton.Text = User.GetInstance().GetButtonText("pointofinterest_popup_dismiss");


            dismissButton.Click += (sender, e) =>
			{
				_pwindow.Dismiss();
				if(_dismissCallback != null)
					_dismissCallback();
			};

            WebView popupWebView = popupView.FindViewById<WebView>(Resource.Id.pointofinterest_popup_webview);

            popupWebView.LoadDataWithBaseURL("file:///", poi.GetHtml(), "text/html", "utf-8", null);

        }

        public void Show()
		{
			_pwindow.ShowAtLocation(this, GravityFlags.Center,  0, 0);
		}

		public void SetDismissCallback(DismissCallback callback){
			_dismissCallback = callback;
		}

        public bool IsShowing()
        {
            return _pwindow.IsShowing;
        }
	}
}
