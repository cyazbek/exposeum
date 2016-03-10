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
    public class PoiStoryData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(StoryData))]
        public int StoryId { get; set; }
        [ForeignKey(typeof(PoiData))]
        public int PoiId { get; set; }

    }
}