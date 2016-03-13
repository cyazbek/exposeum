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

namespace Exposeum.TDGs
{
    class StorylineDescriptionListTDG:TDG
    {
        private static StorylineDescriptionListTDG _instance;

        public static StorylineDescriptionListTDG GetInstance()
        {
            if (_instance == null)
                _instance = new StorylineDescriptionListTDG();
            return _instance;
        }
    }

}