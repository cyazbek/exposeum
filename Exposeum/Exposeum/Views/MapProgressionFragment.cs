using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Exposeum.Controllers;
using Exposeum.Models;

namespace Exposeum.Views
{
	public class MapProgressionFragment : Fragment
	{
		public StoryLine Storyline;
		private MapProgressionFragmentView _mapProgressionFragmentView;

		public MapProgressionFragment() { } //necessary due to default constructor called on rotate event

		public MapProgressionFragment(StoryLine storyline){
			Storyline = storyline;
		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			_mapProgressionFragmentView = new MapProgressionFragmentView (Activity);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return _mapProgressionFragmentView;
		}

		public void Update(){
			_mapProgressionFragmentView.Invalidate ();
		}

		
	}
}
