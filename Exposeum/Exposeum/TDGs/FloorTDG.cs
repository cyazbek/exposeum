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
    class FloorTDG:TDG
    {
        private static FloorTDG _instance;

        public static FloorTDG GetInstance()
        {
            if (_instance == null)
                _instance = new FloorTDG();
            return _instance;
        }
    }
}