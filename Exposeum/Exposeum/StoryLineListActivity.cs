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
using Exposeum.Models;

namespace Exposeum
{
    [Activity(Label = "StoryLineListActivity", Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
    public class StoryLineListActivity : Activity
    {
        public Map map;
        protected override void OnCreate(Bundle bundle)
        {

            StoryLine story = new StoryLine("Nipper the dog", "Le Chien Nipper","Adults", "Adultes", "A walk through different sections of RCA Victor’s production site, constructed over a period of roughly 25 years. This tour takes you through three different time zones, the 1920s, back when Montreal was the world’s largest grain hub and Canada’s productive power house, Montreal’s entertainment rich 1930s and 1943, when production at RCA Victor diversified to serve military needs.", "Une promenade à travers les différentes sections du site de production de RCA Victor, construit sur une période d’environ 25 ans. Ce circuit vous emmène à travers trois fuseaux horaires différents, les années 1920, époque où Montréal était le plus grand centre de grains du monde et la maison de puissance productive du Canada, de divertissement riches années 1930 à Montréal et 1943, lorsque la production chez RCA Victor diversifiée pour répondre aux besoins militaires.", "120", Resource.Drawable.NipperTheDog);
            StoryLine story2 = new StoryLine("Story of Berliner","Histoire de Berliner", "Kids","Enfants", "Description in english","Description en français", "60", Resource.Drawable.EmileBerliner);
            StoryLine story3 = new StoryLine("Pink Panther", "Panthère Rose","All Audience", "Toute Audience", "Description in english", "Description en français", "120", Resource.Drawable.Pink_Panther);
            story2.currentStatus = Status.inProgress;
            story3.currentStatus = Status.isVisited;
            map = new Map();
            map.addStoryLine(story);
            map.addStoryLine(story2);
            map.addStoryLine(story3);
            map.addStoryLine(story);
            map.addStoryLine(story2);
            map.addStoryLine(story3);
            base.OnCreate(bundle);
            ListView listView;
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.StoryLineListView);
            listView = FindViewById<ListView>(Resource.Id.List);
            listView.Adapter = new StoryLineListAdapter(this, map.getStoryLineList);
            listView.ItemClick += ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var storyLine = map.getStoryLineList[e.Position];
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialog_storyline dialog = new dialog_storyline(storyLine);
            dialog.Show(transaction, "Story Line title");
        }
    }
}

