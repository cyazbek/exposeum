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

namespace Exposeum.Models
{
    public class Language
    {
        private static Language _instance = new Language();
        private static string _lang = "";
        private Language()
        {

        }
        public static Language GetInstance()
        {
            return _instance;
        }
        public static string GetLanguage()
        {
            return _lang; 
        }
        public static void SetLanguage(string langSelected)
        {
            _lang = langSelected;
        }
        public static void ToogleLanguage()
        {
            if (_lang == "fr")
                _lang = "en";
            else _lang = "fr";
        }
    }
}