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
using SQLiteNetExtensions.Attributes;

namespace Exposeum.Data
{
    class PoiStoryData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(StoryData))]
        public int storyId { get; set; }
        [ForeignKey(typeof(POIData))]
        public int poiId { get; set; }

    }
}