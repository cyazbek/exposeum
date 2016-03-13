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
    class PoiDescriptionFrTDG:TDG
    {
        private static PoiDescriptionFrTDG _instance;

        public static PoiDescriptionFrTDG GetInstance()
        {
            if (_instance == null)
                _instance = new PoiDescriptionFrTDG();
            return _instance;
        }
    }
}