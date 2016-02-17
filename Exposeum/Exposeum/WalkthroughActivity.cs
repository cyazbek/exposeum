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

namespace Exposeum
{
    [Activity(Label = "WalkthroughActivity", Theme = "@android:style/Theme.Holo.NoActionBar")]
    public class WalkthroughActivity : Activity
    {
        private static ViewPager mPager;
        private List<int> ImagesArray = new List<int>();
        private List<int> ImagesFrench = new List<int>();
        private List<int> ImagesEnglish = new List<int>();
        private List<int> ImagesToDisplay; 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WalkThrough);
            var WalkthroughButton = FindViewById<Button>(Resource.Id.WalkThroughButton);
            string buttonText;
            ImagesFrench.Add(Resource.Drawable.BackgroudLanguageFix);
            ImagesFrench.Add(Resource.Drawable.BackgroudLanguageFix);
            ImagesFrench.Add(Resource.Drawable.BackgroudLanguageFix);
            ImagesFrench.Add(Resource.Drawable.BackgroudLanguageFix);
            ImagesFrench.Add(Resource.Drawable.BackgroudLanguageFix);

            ImagesEnglish.Add(Resource.Drawable.BackgroudLanguageFix);
            ImagesEnglish.Add(Resource.Drawable.Logo);
            ImagesEnglish.Add(Resource.Drawable.BackgroudLanguageFix);
            ImagesEnglish.Add(Resource.Drawable.Logo);
            ImagesEnglish.Add(Resource.Drawable.BackgroudLanguageFix);
            if (Language.getLanguage()=="fr")
            {
                buttonText = "Avancez";
                ImagesToDisplay = ImagesFrench; 
            }
            else
            {
                buttonText = "Skip";
                ImagesToDisplay = ImagesEnglish;
            }
            WalkthroughButton.Text = buttonText;

            init();
            // Create your application here
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