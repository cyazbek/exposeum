using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Exposeum.Controllers;
using Exposeum.Models;
namespace Exposeum
{
    class MapProgressionFragmentView : LinearLayout
    {
        private Paint _bgLine, _circle;

        public MapProgressionFragmentView(Context context) : base(context)
        {

            LayoutParameters = new ViewGroup.LayoutParams(1800, 250);
            Orientation = Orientation.Horizontal;
            SetWillNotDraw(false); //causes the OnDraw override below to be called
            SetMinimumHeight(220);
            SetBackgroundColor(Color.ParseColor("#CC0000"));
            //Settings.BuiltInZoomControls = true;
            //Settings.DisplayZoomControls = false;
            //SetGravity(GravityFlags.Bottom);

            ResetPaint();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            canvas.Save();
            canvas.Scale(0.20f, 0.20f);

            Paint text = new Paint();
            text.TextSize = 300;
            text.Color = Color.ParseColor("#CC0000");

            int currentCentreX = 400;
            bool unvisitedTripped = false;

            List<PointOfInterest> currentPoIs = MapController.GetInstance().Model.CurrentStoryline.MapElements.OfType<PointOfInterest>().ToList();

            for (int i = 0; i < currentPoIs.Count; i++)
            {

                MapElement current = currentPoIs[i];

                if (i < currentPoIs.Count - 1)
                    canvas.DrawLine(currentCentreX, 650, currentCentreX + 800, 650, _bgLine);

                if (!current.Visited)
                    unvisitedTripped = true;

                if (unvisitedTripped)
                {

                    text.Color = Color.White;

                    _circle.SetStyle(Paint.Style.Fill);
                    _circle.Color = Color.ParseColor("#CC0000");

                    canvas.DrawCircle(currentCentreX, 650, 250, _circle);

                    _circle.Color = Color.White;
                    _circle.SetStyle(Paint.Style.Stroke);
                }

                canvas.DrawCircle(currentCentreX, 650, 250, _circle);
                canvas.DrawText("" + (i + 1), currentCentreX - 100, 650 + 100, text);

                currentCentreX += 800;
            }

            canvas.Restore();

            ResetPaint();
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            ev.Dispose(); //dispose of the touch event, do not pass it to the map view underneath
            return true;
        }

        private void ResetPaint()
        {
            _bgLine = new Paint();
            _bgLine.SetStyle(Paint.Style.Stroke);
            _bgLine.Color = Color.White;
            _bgLine.StrokeWidth = 85;

            _circle = new Paint();
            _circle.SetStyle(Paint.Style.FillAndStroke);
            _circle.Color = Color.White;
            _circle.StrokeWidth = 55;
        }

        public override void Invalidate()
        {
            
        }
    }
}