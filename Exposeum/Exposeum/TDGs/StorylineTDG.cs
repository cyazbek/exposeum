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
    class StorylineTDG:TDG
    {
        private static StorylineTDG _instance;

        public static StorylineTDG GetInstance()
        {
            if (_instance == null)
                _instance = new StorylineTDG();
            return _instance;
        }
    }
}