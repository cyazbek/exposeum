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
    class MapElementsTDG:TDG
    {
        private static MapElementsTDG _instance;

        public static MapElementsTDG GetInstance()
        {
            if (_instance == null)
                _instance = new MapElementsTDG();
            return _instance;
        }
    }
}