using System;
using SQLite;

namespace Exposeum.Models
{
    class StoryLineDbControl
    {
        private SQLiteConnection dbconnection;
        private String folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public StoryLineDbControl()
        {
            this.dbconnection = connect();
        }

        private SQLiteConnection connect()
        {
            return new SQLiteConnection(System.IO.Path.Combine(folder, "StoryLine.db"));
        }
        public void createTable()
        {
            dbconnection.CreateTable<StoryLine>();
        }
        public void addStory(StoryLine story)
        {
            var storyLine = story;
            this.dbconnection.Insert(storyLine);
        }
        public StoryLine getStory(int id)
        {
            return dbconnection.Get<StoryLine>(id);
        }

    }
}