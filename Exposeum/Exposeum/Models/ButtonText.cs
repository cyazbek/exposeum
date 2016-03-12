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
    public class ButtonText
    {
        public string _id { get; set; }
        public string _text { get; set; }
        public ButtonText()
        {

        }
        public ButtonText(string id, string text)
        {
            this._id = id;
            this._text = text; 
        }
    }
}