using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace Exposeum.Resources.layout
{
    public class FloorSelectorFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.FloorSelectorLayout, container, false);

            var wv_bg = view.FindViewById<WebView>(Resource.Id.floor_bg_web);
            wv_bg.LoadDataWithBaseURL("file:///android_asset/",
                "<html><body><img src=\"floorSelectionBackground.gif\" style=\"max-width: 100%; max-height: 100%; \">" +
                "</img></body></html>", "text/html", "utf-8", null);

            return view;
        }
    }
}