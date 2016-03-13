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
    class PoiDescriptionEnTDG:TDG
    {
        private static PoiDescriptionEnTDG _instance;

        public static PoiDescriptionEnTDG GetInstance()
        {
            if (_instance == null)
                _instance = new PoiDescriptionEnTDG();
            return _instance;
        }
    }
}