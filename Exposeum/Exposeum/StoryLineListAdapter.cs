using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Exposeum.Models;

namespace Exposeum
{
    public class StoryLineListAdapter : BaseAdapter<StoryLine>
    {
        readonly List<StoryLine> _items;
        readonly Activity _context;
        public StoryLineListAdapter(Activity context, List<StoryLine> items)
            : base()
        {
            _context = context;
            _items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override StoryLine this[int position]
        {
            get { return _items[position]; }
        }
        public override int Count
        {
            get { return _items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            var icon = 0;
            if (item.CurrentStatus.Equals(Status.InProgress))
            {
                icon = Resource.Drawable.inProgress;
            }
            else if (item.CurrentStatus.Equals(Status.IsVisited))
            {
                icon = Resource.Drawable.completed;
            }
            else
                icon = Resource.Drawable.start;


            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = _context.LayoutInflater.Inflate(Resource.Layout.StoryLineRow, null);
            view.FindViewById<ImageView>(Resource.Id.StoryLineIcon).SetImageResource(item.ImageId);
            view.FindViewById<TextView>(Resource.Id.TitleTextView).Text = item.GetName();
            view.FindViewById<TextView>(Resource.Id.AudienceTextView).Text = item.GetAudience();
            view.FindViewById<TextView>(Resource.Id.durationTextView).Text = item.Duration + " min";
            view.FindViewById<ImageView>(Resource.Id.statusIcon).SetImageResource(icon);
            return view;
        }

    }
}