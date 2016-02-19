using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Exposeum.Models;

namespace Exposeum
{
    [Activity(Label = "Exposeum")]
    public class SplashActivity : Activity
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Splash);
            var imageView =
            FindViewById<ImageView>(Resource.Id.imageView);
            var buttonNext =
            FindViewById<Button>(Resource.Id.switchButton);
            List<int> imageList_en = new List<int>();
            imageList_en.Add(Resource.Drawable.first);
            imageList_en.Add(Resource.Drawable.second);
            imageList_en.Add(Resource.Drawable.third);
            imageList_en.Add(Resource.Drawable.fourth);
            List<int> imageList_fr= new List<int>();
            imageList_fr.Add(Resource.Drawable.first_fr);
            imageList_fr.Add(Resource.Drawable.second_fr);
            imageList_fr.Add(Resource.Drawable.third_fr);
            imageList_fr.Add(Resource.Drawable.fourth_fr);
            
            if(Models.Language.getLanguage()=="en")
            {
                int counter = 0;
                imageView.SetImageResource(imageList_en[0]);
                buttonNext.Text = "Next";
                buttonNext.Click += (o, e) =>
                {
                    counter += 1;
                    if (counter < imageList_en.Count -1)
                    {
                        imageView.SetImageResource(imageList_en[counter]);
                        buttonNext.Text = "Next";
                    }
                    else if (counter == imageList_en.Count - 1)
                    {
                        imageView.SetImageResource(imageList_en[counter]);
                        buttonNext.Text = "Start Journey";
                    }
                    else
                    {
                        counter = 5;
                        var intent = new Intent(this, typeof(VisitActivity_en));
                        StartActivity(intent);
                    }
                };
            }
            if (Models.Language.getLanguage() == "fr")
            {
                int counter = 0;
                imageView.SetImageResource(imageList_fr[0]);
                buttonNext.Text = "Suivant";
                buttonNext.Click += (o, e) =>
                {
                    counter += 1;
                    if (counter < imageList_fr.Count - 1)
                    {
                        imageView.SetImageResource(imageList_fr[counter]);
                        buttonNext.Text = "Suivant";
                    }
                    else if (counter == imageList_fr.Count - 1)
                    {
                        imageView.SetImageResource(imageList_fr[counter]);
                        buttonNext.Text = "Commencer la visite";
                    }
                    else
                    {
                        counter = 5;
                        var intent = new Intent(this, typeof(VisitActivity_fr));
                        StartActivity(intent);
                    }
                };
            }

        }
    }
}
