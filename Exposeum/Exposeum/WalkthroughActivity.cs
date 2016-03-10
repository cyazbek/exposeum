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
        private List<int> _imagesFrench = new List<int>();
        private List<int> _imagesEnglish = new List<int>();
        private List<int> _imagesToDisplay;
        Intent _intent; 
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WalkThrough);
            var walkthroughButton = FindViewById<Button>(Resource.Id.WalkThroughButton);
            string buttonText;
            _imagesFrench.Add(Resource.Drawable.first_fr);
            _imagesFrench.Add(Resource.Drawable.second_fr);
            _imagesFrench.Add(Resource.Drawable.third_fr);
            _imagesFrench.Add(Resource.Drawable.fourth_fr);

            _imagesEnglish.Add(Resource.Drawable.first);
            _imagesEnglish.Add(Resource.Drawable.second);
            _imagesEnglish.Add(Resource.Drawable.third);
            _imagesEnglish.Add(Resource.Drawable.fourth);
            if (Language.GetLanguage()=="fr")
            {
                buttonText = "Sauter";
                _intent = new Intent(this, typeof(VisitActivityFr));
                _imagesToDisplay = _imagesFrench; 
            }
            else
            {
                buttonText = "Skip";
                _intent = new Intent(this, typeof(VisitActivityEn));
                _imagesToDisplay = _imagesEnglish;
            }
            walkthroughButton.Text = buttonText;

            Init();
            walkthroughButton.Click += (sender, e) =>
            {
                StartActivity(_intent);
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