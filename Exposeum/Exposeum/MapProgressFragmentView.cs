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
        private const int LEFT_STARTING_POSITION = 100;
        private const int INTER_CIRCLE_DISTANCE = 160;

        public MapProgressionFragmentView(Context context) : base(context)
        {
            int numberOfPOIs = MapController.GetInstance().Model.CurrentStoryline.MapElements.OfType<PointOfInterest>().ToList().Count;
            int fragmentWidth = LEFT_STARTING_POSITION + (numberOfPOIs*INTER_CIRCLE_DISTANCE) - 60;

            LayoutParameters = new ViewGroup.LayoutParams(fragmentWidth, LayoutParams.MatchParent);
            Orientation = Orientation.Horizontal;
            SetWillNotDraw(false); //causes the OnDraw override below to be called
            SetMinimumHeight(220);
            SetBackgroundColor(Color.ParseColor("#da1f1c"));

            ResetPaint();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            canvas.Save();

            Paint text = new Paint();
            text.TextSize = 60;
            text.Color = Color.ParseColor("#CC0000");

            int currentCentreX = LEFT_STARTING_POSITION; //left starting point
            bool unvisitedTripped = false;

            List<PointOfInterest> currentPoIs = MapController.GetInstance().Model.CurrentStoryline.MapElements.OfType<PointOfInterest>().ToList();

            for (int i = 0; i < currentPoIs.Count; i++)
            {

                MapElement current = currentPoIs[i];

                if (i < currentPoIs.Count - 1)
                    canvas.DrawLine(currentCentreX, 130, currentCentreX + INTER_CIRCLE_DISTANCE, 130, _bgLine);

                if (!current.Visited)
                    unvisitedTripped = true;

                if (unvisitedTripped)
                {

                    text.Color = Color.White;

                    _circle.SetStyle(Paint.Style.Fill);
                    _circle.Color = Color.ParseColor("#CC0000");

                    canvas.DrawCircle(currentCentreX, 130, 50, _circle);

                    _circle.Color = Color.White;
                    _circle.SetStyle(Paint.Style.Stroke);
                }

                canvas.DrawCircle(currentCentreX, 130, 50, _circle);
                // textSpacing logic for values with 2 digits.
                int textSpacing = i < 9 ? 16 : 34;
                canvas.DrawText("" + (i + 1), currentCentreX - textSpacing, 130 + 16, text);

                currentCentreX += INTER_CIRCLE_DISTANCE;
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
            _bgLine.StrokeWidth = 17;

            _circle = new Paint();
            _circle.SetStyle(Paint.Style.FillAndStroke);
            _circle.Color = Color.White;
            _circle.StrokeWidth = 11;
        }

        public override void Invalidate()
        {
            
        }
    }
}