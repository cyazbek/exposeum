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
using Exposeum.Controllers;

namespace Exposeum
{
    class DialogStoryline : DialogFragment
    {
        StoryLine _storyLine;
        Context _context; 
		StorylineController _storylineController = StorylineController.GetInstance();
        
        public DialogStoryline(StoryLine storyLine, Context context)
        {
            this._storyLine = storyLine;
            this._context = context;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.StoryLine_Dialog, container, false);
            view.FindViewById<ImageView>(Resource.Id.sotryLineDialogPic).SetImageResource(_storyLine.ImageId);
            view.FindViewById<TextView>(Resource.Id.storyLineDialogTitle).Text = _storyLine.GetName();
            view.FindViewById<TextView>(Resource.Id.storyLineDialogAudience).Text = _storyLine.GetAudience();
            view.FindViewById<TextView>(Resource.Id.storyLineDialogDuration).Text = _storyLine.Duration + " min";
            view.FindViewById<TextView>(Resource.Id.storyLineDialogDescription).Text = _storyLine.GetDescription();
            var textview = view.FindViewById<TextView>(Resource.Id.storyLineDialogDescription);
            textview.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            textview.VerticalScrollBarEnabled = true;
            textview.HorizontalFadingEdgeEnabled = true;
            var button = view.FindViewById<Button>(Resource.Id.storyLineDialogButton);
            if (Language.GetLanguage() == "fr")
                button.Text = "Commencez l'Expérience";
            else
                button.Text = "Begin Journey";
            button.Click += delegate {
				_storylineController.SetActiveStoryLine();
                var intent = new Intent(_context, typeof(MapActivity));
                StartActivity(intent);
            };
            this.Dialog.SetCanceledOnTouchOutside(true);
            return view;
        }

        

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }
        
    }
}