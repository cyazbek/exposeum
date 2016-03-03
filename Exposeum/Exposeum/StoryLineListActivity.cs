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
            StoryLine story = new StoryLine("Nipper the dog", "khg kjhglg lhgluhg;p itu piug liyg uyfouyf oyuigiug piyug piu piutr df;kdsfhdsf;uh; ;iudfh;dfohuidfs ;dsfuiohfds;odfuidsfo ;ofdiu;dsfoihufds;ofdiu posfudi;dsfiod s;dsifu s;dfiuohdsf ;ofdih ou ouytpiug ouyf iytd oyiutpiu ouy ouyf khyf lugligiuyt yiuglygluy ouyglygluyf luygluyg", "18+", "45", Resource.Drawable.NipperTheDog);
            StoryLine story2 = new StoryLine("Emile Berliner", "khg kjhglg lhgluhg;p itu piug liyg uyfouyf oyuigiug piyug piu piutr df;kdsfhdsf;uh; ;iudfh;dfohuidfs ;dsfuiohfds;odfuidsfo ;ofdiu;dsfoihufds;ofdiu posfudi;dsfiod s;dsifu s;dfiuohdsf ;ofdih ou ouytpiug ouyf iytd oyiutpiu ouy ouyf khyf lugligiuyt yiuglygluy ouyglygluyf luygluyg", "All Audience", "60", Resource.Drawable.EmileBerliner);
            StoryLine story3 = new StoryLine("Pink Panther", "khg kjhglg lhgluhg;p itu piug liyg uyfouyf oyuigiug piyug piu piutr df;kdsfhdsf;uh; ;iudfh;dfohuidfs ;dsfuiohfds;odfuidsfo ;ofdiu;dsfoihufds;ofdiu posfudi;dsfiod s;dsifu s;dfiuohdsf ;ofdih ou ouytpiug ouyf iytd oyiutpiu ouy ouyf khyf lugligiuyt yiuglygluy ouyglygluyf luygluyg khg kjhglg lhgluhg;p itu piug liyg uyfouyf oyuigiug piyug piu piutr df;kdsfhdsf;uh; ;iudfh;dfohuidfs ;dsfuiohfds;odfuidsfo ;ofdiu;dsfoihufds;ofdiu posfudi;dsfiod s;dsifu s;dfiuohdsf ;ofdih ou ouytpiug ouyf iytd oyiutpiu ouy ouyf khyf lugligiuyt yiuglygluy ouyglygluyf luygluyg", "Kids", "60", Resource.Drawable.Pink_Panther);
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

