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
using Exposeum.Controllers;

namespace Exposeum
{
    [Activity(Label = "StoryLineListActivity", Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
    public class StoryLineListActivity : Activity
    {
        public Map map;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ListView listView;
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.StoryLineListView);
            listView = FindViewById<ListView>(Resource.Id.List);
			listView.Adapter = StoryLineController.GetStoryLines(this);
            listView.ItemClick += ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var storyLine = map.getStoryLineList[e.Position];
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            if(storyLine.currentStatus==Status.inProgress)
            {
                DialogStorylineInProgress dialog = new DialogStorylineInProgress(storyLine);
                dialog.Show(transaction, "Story Line title");
            }
            else
            {
                DialogStoryline dialog = new DialogStoryline(storyLine);
                dialog.Show(transaction, "Story Line title");
            }
        }
        
    }
}

