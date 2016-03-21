using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
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
        private readonly List<int> _imagesArray = new List<int>();
        private List<int> _imagesToDisplay;
        Intent _intent;
        readonly User _user = User.GetInstance(); 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WalkThrough);
            var walkthroughButton = FindViewById<Button>(Resource.Id.WalkThroughButton);
            _imagesToDisplay = _user.GetImageList();
            walkthroughButton.Text = _user.GetButtonText("WalkThroughButton");
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