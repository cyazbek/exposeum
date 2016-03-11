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
using Exposeum.Controllers;
using Exposeum.Models;

namespace Exposeum
{
    class DialogPauseStorylineConfirmation : DialogFragment
    {
        StoryLine _storyLine;
        readonly StorylineController _storylineController = StorylineController.GetInstance();
        private Context _context;

        public DialogPauseStorylineConfirmation(StoryLine storyLine, Context context)
        {
            this._storyLine = storyLine;
            _context = context;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Pause_confirmation, container, false);
            var buttonToConfirmPause = view.FindViewById<Button>(Resource.Id.confirm_pause);
            var buttonToRejectPause = view.FindViewById<Button>(Resource.Id.reject_pause);

            if (Language.GetLanguage() == "fr")
            {
                view.FindViewById<TextView>(Resource.Id.pause_text).Text = "Suspendre";
                buttonToConfirmPause.Text = "Oui";
                buttonToRejectPause.Text = "Non";

            }
            else
            {
                view.FindViewById<TextView>(Resource.Id.pause_text).Text = "Pause";
                buttonToConfirmPause.Text = "Yes";
                buttonToRejectPause.Text = "No";
            }


            buttonToConfirmPause.Click += delegate
            {
                _storylineController.PauseStorylineBeacons();
                Toast.MakeText(_context, "Paused", ToastLength.Short).Show();

                // Redirecting to list of storylines
                var intent = new Intent(_context, typeof(StoryLineListActivity));
                StartActivity(intent);
            };

            buttonToRejectPause.Click += delegate {
                Dialog.Dismiss();
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