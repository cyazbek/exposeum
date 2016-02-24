using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace Exposeum.UITests
{
	[TestFixture]
	public class Tests
	{
		AndroidApp app;

		[SetUp]
		public void BeforeEachTest ()
		{
			app = ConfigureApp.Android.StartApp ();
		}

		[Test]
		public void ClickingButtonTwiceShouldChangeItsLabel ()
		{

			//app.Repl ();
			app.Flash(marked:"list");
			app.Flash(marked:"Map Activity");
			app.Tap(marked:"Map Activity");
			app.PinchToZoomIn("content");
			app.SwipeRight ();
			app.SwipeLeft ();

			//-------------------------------------------------------------------------------------
			//Next Plan of action is to tap on the right coordinate to change the color of the POI.
			//-------------------------------------------------------------------------------------

			/*
			Func<AppQuery, AppQuery> MyButton = c => c.Button ("myButton");

			app.Tap (MyButton);
			app.Tap (MyButton);
			AppResult[] results = app.Query (MyButton);
			app.Screenshot ("Button clicked twice.");

			Assert.AreEqual ("2 clicks!", results [0].Text);

			*/
		}
	}
}

