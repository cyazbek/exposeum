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
using Android.Support.V4.View;
using Exposeum.Models;
using Android.Content.PM;

namespace Exposeum
{
    [Activity(Label = "WalkthroughActivity", Theme = "@android:style/Theme.Holo.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class WalkthroughActivity : Activity
    {
        private static ViewPager _mPager;
        private List<int> _imagesArray = new List<int>();
        private List<int> _imagesToDisplay;
        Intent _intent;
        User user = User.GetInstance(); 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WalkThrough);
            var walkthroughButton = FindViewById<Button>(Resource.Id.WalkThroughButton);
            _imagesToDisplay = user.GetImageList();
            walkthroughButton.Text = user.GetButtonText("WalkThroughButton");
            Init();
            walkthroughButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(VisitActivity));
                StartActivity(intent);
            };
            
        }
        private void Init()
        {
            foreach(var x in _imagesToDisplay)
            {
                _imagesArray.Add(x); 
            }
            _mPager = (ViewPager)FindViewById(Resource.Id.WviewPager);
            _mPager.Adapter = new WalkthroughtFragmentAdapter(this, _imagesArray);


        }
    }
}