using Android.Views;
using Android.Widget;
using Android.Webkit;
using Android.Content;
using Exposeum.Models;

namespace Exposeum.Views
{
	public class BeaconPopup : View
	{
		private PopupWindow _pwindow;

		public BeaconPopup (Context context, PointOfInterest POI) : base (context)
		{
			var inflater = LayoutInflater.From(context);
			View popup_view  = inflater.Inflate(Resource.Layout.BeaconSummaryPopupView, null);

			popup_view.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
			_pwindow = new PopupWindow(popup_view, WindowManagerLayoutParams.WrapContent, WindowManagerLayoutParams.WrapContent);

			Button dismissButton = popup_view.FindViewById<Button>(Resource.Id.beacon_popup_dismiss);

			dismissButton.Click += (sender, e) =>
			{
				_pwindow.Dismiss();
			};

		    
            WebView popupWebView = popup_view.FindViewById<WebView>(Resource.Id.beacon_popup_webview);
			popupWebView.LoadData(POI.description.getHTML(), "text/html", "utf-8");
		}

		public void Show()
		{
			_pwindow.ShowAtLocation(this, GravityFlags.Center,  0, 0);
		}
	}
}
