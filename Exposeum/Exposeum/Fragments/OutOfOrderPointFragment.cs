using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Exposeum.Models;

namespace Exposeum.Fragments
{
    public class OutOfOrderPointFragment : DialogFragment
    {

        private readonly PointOfInterest _point;

        public OutOfOrderPointFragment(PointOfInterest point)
        {
            _point = point;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.OutOfOrderPointPopup, container, false);
            view.FindViewById<TextView>(Resource.Id.wrongPointDesc).Text += _point.NameEn;
            var textview = view.FindViewById<TextView>(Resource.Id.wrongPointDesc);
            textview.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            textview.VerticalScrollBarEnabled = true;
            textview.HorizontalFadingEdgeEnabled = true;
            var button = view.FindViewById<Button>(Resource.Id.wrongPointButton);
            button.Text = User.GetInstance().GetButtonText("wrongPointButton");
            
            button.Click += (sender, e) =>
            {
                this.Dismiss();
            };
            this.Dialog.SetCanceledOnTouchOutside(true);
            return view;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
        }

    }
}