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
        private static Language instance = new Language();
        private static string lang = "";
        private Language()
        {

        }
        public static Language getInstance()
        {
            return instance;
        }
        public static string getLanguage()
        {
            return lang; 
        }
        public static void setLanguage(string langSelected)
        {
            lang = langSelected;
        }
        public static void toogleLanguag()
        {
            if (lang == "fr")
                lang = "en";
            else lang = "fr";
        }

    }
}