using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Exposeum.Data
{
    class StoryData
    {
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get; set;
        }
        public string name_en
        {
            get; set;
        }
        public string name_fr
        {
            get; set;
        }
        public string audience_en
        {
            get; set;
        }
        public string audience_fr
        {
            get; set;
        }
        public string desc_en
        {
            get; set;
        }
        public string desc_fr
        {
            get; set;
        }
        public string duration
        {
            get; set;
        }
    }
}