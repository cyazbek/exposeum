using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Exposeum.Fragments
{
    public class SearchingForBeaconFragment : DialogFragment
    {
        public delegate void Callback();
        private Callback _callback;

        public SearchingForBeaconFragment(Callback callback)
        {
            _callback = callback;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.OutOfOrderPointPopup, container, false);
            var textview = view.FindViewById<TextView>(Resource.Id.DirectToLastPointDesc);
            textview.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            textview.VerticalScrollBarEnabled = true;
            textview.HorizontalFadingEdgeEnabled = true;
            var cancelButton = view.FindViewById<Button>(Resource.Id.SearchingForBeaconCancelButton);

            cancelButton.Click += (sender, e) =>
            {
                _callback();
                Dismiss();
            };

            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }

    }
}