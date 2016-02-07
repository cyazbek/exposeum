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
    class BeaconData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string uuid
        {
            get; set;
        }
        public int major
        {
            get; set;
        }
        public int minor
        {
            get; set;
        }
        public int convertBeaconToData(Models.Beacon passedBeacon)
        {
            this.uuid = passedBeacon.uuid.ToString();
            this.major = passedBeacon.major;
            this.minor = passedBeacon.minor;
            return this.ID; 
        }
    }
}