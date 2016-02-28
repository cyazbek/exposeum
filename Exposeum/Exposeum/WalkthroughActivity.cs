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
        private static ViewPager mPager;
        private List<int> ImagesArray = new List<int>();
        private List<int> ImagesFrench = new List<int>();
        private List<int> ImagesEnglish = new List<int>();
        private List<int> ImagesToDisplay;
        Intent intent; 
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WalkThrough);
            var WalkthroughButton = FindViewById<Button>(Resource.Id.WalkThroughButton);
            string buttonText;
            ImagesFrench.Add(Resource.Drawable.first);
            ImagesFrench.Add(Resource.Drawable.second);
            ImagesFrench.Add(Resource.Drawable.third);
            ImagesFrench.Add(Resource.Drawable.fourth);

            ImagesEnglish.Add(Resource.Drawable.first);
            ImagesEnglish.Add(Resource.Drawable.second);
            ImagesEnglish.Add(Resource.Drawable.third);
            ImagesEnglish.Add(Resource.Drawable.fourth);
            if (Language.getLanguage()=="fr")
            {
                buttonText = "Avancez";
                intent = new Intent(this, typeof(VisitActivity_fr));
                ImagesToDisplay = ImagesFrench; 
            }
            else
            {
                buttonText = "Skip";
                intent = new Intent(this, typeof(VisitActivity_en));
                ImagesToDisplay = ImagesEnglish;
            }
            WalkthroughButton.Text = buttonText;

            init();
            WalkthroughButton.Click += (sender, e) =>
            {
                StartActivity(intent);
            };
            
        }
        private void init()
        {
            foreach(var x in ImagesToDisplay)
            {
                ImagesArray.Add(x); 
            }
            mPager = (ViewPager)FindViewById(Resource.Id.WviewPager);
            mPager.Adapter = new WalkthroughtFragmentAdapter(this, ImagesArray);


        }
    }
}