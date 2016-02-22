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
using Android.Support.V4.View;

namespace Exposeum
{
    public class WalkthroughtFragmentAdapter: PagerAdapter
    {
        private List<int> IMAGES;
        private LayoutInflater inflater;
        private Context context;

        public override int Count
        {
            get
            {
                return IMAGES.Count;
            }
        }

        public WalkthroughtFragmentAdapter(Context context, List<int> IMAGES)
        {
            this.context = context;
            this.IMAGES = IMAGES;
            inflater = LayoutInflater.From(context);
        }
        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            container.RemoveView((View)@object);
        }


        public override Java.Lang.Object InstantiateItem(ViewGroup view, int position)
        {
            View imageLayout = inflater.Inflate(Resource.Layout.WalkThroughFragment, view, false);

            ImageView imageView = (ImageView)imageLayout.FindViewById(Resource.Id.WalkThroughImageView);
            imageView.SetImageResource(IMAGES[position]);

            view.AddView(imageLayout, 0);

            return imageLayout;
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view.Equals(@object);
        }

    }
}
