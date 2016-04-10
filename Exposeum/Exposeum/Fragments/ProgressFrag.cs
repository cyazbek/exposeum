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
using Android.Widget;
using Android.Support.V4.App;
using Fragment = Android.App.Fragment;

namespace Exposeum.Fragments
{
    public class ProgressFrag : Fragment
    {
        private Context context;
        public ProgressFrag(Context context)
        {
            this.context = context;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var ll = new MapProgressionFragmentView(context);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var ll = new MapProgressionFragmentView(context);
            View view = inflater.Inflate(Resource.Layout.ProgressFragLayout, container, false);
            LinearLayout frfr = view.FindViewById<LinearLayout>(Resource.Id.linearLayoutHH);
            frfr.AddView(ll);
            return view;
        }
    }
}