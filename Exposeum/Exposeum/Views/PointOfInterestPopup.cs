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

		public PointOfInterestPopup (Context context, PointOfInterest poi) : base (context)
		{
			var inflater = LayoutInflater.From(context);
			View popupView  = inflater.Inflate(Resource.Layout.BeaconSummaryPopupView, null);

			popupView.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
			_pwindow = new PopupWindow(popupView, WindowManagerLayoutParams.WrapContent, WindowManagerLayoutParams.WrapContent);

			Button dismissButton = popupView.FindViewById<Button>(Resource.Id.pointofinterest_popup_dismiss);

			dismissButton.Click += (sender, e) =>
			{
				_pwindow.Dismiss();
			};

            WebView popupWebView = popupView.FindViewById<WebView>(Resource.Id.pointofinterest_popup_webview);

            if(poi.Visited)
                popupWebView.LoadDataWithBaseURL("file:///android_asset/", poi.Description.GetFullDescriptionHtml(), "text/html", "utf-8", null);
            //popupWebView.LoadData(POI.description.getFullDescriptionHTML(), "text/html", "utf-8");

            
            else
                popupWebView.LoadData(poi.Description.GetOnlySummaryHtml(), "text/html", "utf-8");
        }

		public void Show()
		{
			_pwindow.ShowAtLocation(this, GravityFlags.Center,  0, 0);
		}

        public bool IsShowing()
        {
            return _pwindow.IsShowing;
        }
	}
}
