using Android.Views;
using Android.Widget;
using Android.Webkit;
using Android.Content;
using Exposeum.Models;

namespace Exposeum.Views
{
	public class PointOfInterestPopup : View
	{
		private PopupWindow _pwindow;

		public PointOfInterestPopup (Context context, PointOfInterest POI) : base (context)
		{
			var inflater = LayoutInflater.From(context);
			View popup_view  = inflater.Inflate(Resource.Layout.BeaconSummaryPopupView, null);

			popup_view.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
			_pwindow = new PopupWindow(popup_view, WindowManagerLayoutParams.WrapContent, WindowManagerLayoutParams.WrapContent);

			Button dismissButton = popup_view.FindViewById<Button>(Resource.Id.pointofinterest_popup_dismiss);

			dismissButton.Click += (sender, e) =>
			{
				_pwindow.Dismiss();
			};

            WebView popupWebView = popup_view.FindViewById<WebView>(Resource.Id.pointofinterest_popup_webview);

            if(POI.Visited)
                popupWebView.LoadDataWithBaseURL("file:///android_asset/", POI.description.getFullDescriptionHTML(), "text/html", "utf-8", null);
            //popupWebView.LoadData(POI.description.getFullDescriptionHTML(), "text/html", "utf-8");

            
            else
                popupWebView.LoadData(POI.description.getOnlySummaryHTML(), "text/html", "utf-8");
        }

		public void Show()
		{
			_pwindow.ShowAtLocation(this, GravityFlags.Center,  0, 0);
		}

        public bool isShowing()
        {
            return _pwindow.IsShowing;
        }
	}
}
