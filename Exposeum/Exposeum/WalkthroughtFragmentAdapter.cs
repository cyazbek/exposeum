using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;

namespace Exposeum
{
    public class WalkthroughtFragmentAdapter: PagerAdapter
    {
        private List<int> _images;
        private LayoutInflater _inflater;
        private Context _context;

        public override int Count
        {
            get
            {
                return _images.Count;
            }
        }

        public WalkthroughtFragmentAdapter(Context context, List<int> images)
        {
            _context = context;
            _images = images;
            _inflater = LayoutInflater.From(context);
        }
        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            container.RemoveView((View)@object);
        }


        public override Java.Lang.Object InstantiateItem(ViewGroup view, int position)
        {
            View imageLayout = _inflater.Inflate(Resource.Layout.WalkThroughFragment, view, false);

            ImageView imageView = (ImageView)imageLayout.FindViewById(Resource.Id.WalkThroughImageView);
            imageView.SetImageResource(_images[position]);

            view.AddView(imageLayout, 0);

            return imageLayout;
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view.Equals(@object);
        }

    }
}
