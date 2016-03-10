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
using SQLite;
using Java.Util;

namespace Exposeum.Data
{
    public class BeaconData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Uuid
        {
            get; set;
        }
        public int Major
        {
            get; set;
        }
        public int Minor
        {
            get; set;
        }
        public int ConvertBeaconToData(Models.Beacon passedBeacon)
        {
            this.Uuid = passedBeacon.Uuid.ToString();
            this.Major = passedBeacon.Major;
            this.Minor = passedBeacon.Minor;
            return this.Id; 
        }
    }
}