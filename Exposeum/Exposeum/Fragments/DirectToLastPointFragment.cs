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
using Exposeum.Models;

namespace Exposeum.Fragments
{
    public class DirectToLastPointFragment : DialogFragment
    {


		public delegate void Callback(FragmentTransaction transaction);
        private readonly Callback _callback;

        public DirectToLastPointFragment(Callback callback)
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
            var yesButton = view.FindViewById<Button>(Resource.Id.DirectYesButton);
            var noButton = view.FindViewById<Button>(Resource.Id.DirectNoButton);

            noButton.Click += (sender, e) =>
            {
                Dismiss();
                // resume normal storyline
            };

            yesButton.Click += (sender, e) =>
            {
                // do a path to last point
				FragmentTransaction transaction = FragmentManager.BeginTransaction();
				_callback(transaction);
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