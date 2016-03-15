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

namespace Exposeum
{
    /// <summary>
    /// Class which tracks the current state of the app (storyline status, last visited POI, etc.)
    /// </summary>
    public class AppState
    {
        private static AppState _instance;

        /// <summary>
        /// Boolean to check if the app's language is French
        /// </summary>
        public Language IsFrench { get; set; }

        protected AppState()
        {
            IsFrench = false;
        }

        /// <summary>
        /// Singleton adhering GetInstance() method
        /// </summary>
        /// <returns></returns>
       public AppState GetInstance()
        {
            return _instance ?? (_instance = new AppState());
        }
    }


}