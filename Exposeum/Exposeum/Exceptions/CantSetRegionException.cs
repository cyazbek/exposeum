using System;
using EstimoteSdk;
using Android.Util;
using Android.OS;
using Android.Content.PM;
using JavaObject = Java.Lang.Object;
using Android.Content;
using System.Collections.Generic;
using Android.App;
using Android.Support.V4.App;
using Exposeum.Models;
using Java.Util.Concurrent;

namespace Exposeum
{
	public class CantSetRegionException : Exception
	{

		public CantSetRegionException(string message): base(message){}
	}

}