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
		StorylineController _storylineController = StorylineController.GetInstance();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            ListView listView;
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.StoryLineListView);
            listView = FindViewById<ListView>(Resource.Id.List);
			listView.Adapter = _storylineController.GetStoryLines(this);
            listView.ItemClick += ListViewItemClick;
        }

        private void ListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
			_storylineController.SelectStoryLine (e.Position);
			_storylineController.ShowSelectedStoryLineDialog (transaction, this);

        }
        
    }
}

