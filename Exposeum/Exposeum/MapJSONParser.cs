using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Exposeum.Models;
using Android.Graphics.Drawables;

namespace Exposeum
{

	public class MapJSONParser
	{

		private static String JSON_URL = "http://mowbray.tech/exposeum/exposeum.json";

		public async void FetchAndParseMapJSON(){

			//fetch the JSON file from the internet
			String JSONData = await DownloadJSONAsync (JSON_URL);

			//deserialize into JObject which we can iterate over
			var JSONPayload = JsonConvert.DeserializeObject (JSONData) as JObject;

			//deserialize and store the floors from the JSON data
			List<Floor> floors = ParseFloors (JSONPayload);
		}

		private List<Floor> ParseFloors(JObject JSONPayload){

			List<Floor> floors = new List<Floor>();

			foreach (var floorOBJ in JSONPayload["floorPlan"]) {

				String drawableString = (String)floorOBJ ["imagePath"];

				Drawable floorDrawable; 

				try
				{
					floorDrawable = Android.App.Application.Context.Resources.GetDrawable(Android.App.Application.Context.Resources.GetIdentifier(drawableString, "drawable", "Exposeum"));

				}
				catch(Exception e)
				{
					floorDrawable = new ColorDrawable();
				}

				Floor newFloor = new Floor (floorDrawable);

				int floorID = int.Parse ((String)floorOBJ ["floorID"]); //get the floor ID for possible later use

				floors.Add (newFloor);
			}

			return floors;

		}
		private async Task<String> DownloadJSONAsync(string url)
		{
			
			using (HttpClient client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(url))
			using (HttpContent content = response.Content)
			{
				string result = await content.ReadAsStringAsync();
				return result;
			}
		}
	}
}
