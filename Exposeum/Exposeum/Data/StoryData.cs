using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Exposeum.Data
{
    public class StoryData
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get; set;
        }
        public string NameEn
        {
            get; set;
        }
        public string NameFr
        {
            get; set;
        }
        public string AudienceEn
        {
            get; set;
        }
        public string AudienceFr
        {
            get; set;
        }
        public string DescEn
        {
            get; set;
        }
        public string DescFr
        {
            get; set;
        }
        public int Duration
        {
            get; set;
        }
    }
}