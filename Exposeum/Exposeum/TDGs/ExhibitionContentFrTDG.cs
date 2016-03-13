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
    class ExhibitionContentFrTDG:TDG
    {
        private static ExhibitionContentFrTDG _instance;

        public static ExhibitionContentFrTDG GetInstance()
        {
            if (_instance == null)
                _instance = new ExhibitionContentFrTDG();
            return _instance;
        }
    }
}