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
    public class StoryLineListAdapter : BaseAdapter<StoryLine>
    {
        List<StoryLine> items;
        Activity context;
        public StoryLineListAdapter(Activity context, List<StoryLine> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override StoryLine this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            var icon = 0;
            if (item.currentStatus.Equals(Status.inProgress))
            {
                icon = Resource.Drawable.inProgress;
            }
            else if (item.currentStatus.Equals(Status.isVisited))
            {
                icon = Resource.Drawable.completed;
            }
            else
                icon = Resource.Drawable.start;


            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.StoryLineRow, null);
            view.FindViewById<ImageView>(Resource.Id.StoryLineIcon).SetImageResource(item.ImageId);
            view.FindViewById<TextView>(Resource.Id.TitleTextView).Text = item.getName();
            view.FindViewById<TextView>(Resource.Id.AudienceTextView).Text = item.getAudience();
            view.FindViewById<TextView>(Resource.Id.durationTextView).Text = item.duration + " min";
            view.FindViewById<ImageView>(Resource.Id.statusIcon).SetImageResource(icon);
            return view;
        }

    }
}