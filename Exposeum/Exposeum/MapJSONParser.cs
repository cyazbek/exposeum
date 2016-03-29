using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Exposeum.TempModels;
using Android.Graphics.Drawables;

namespace Exposeum
{

	public class MapJSONParser
	{

		private static String JSON_BASE_URL = "http://mowbray.tech/exposeum";
		private static String JSON_SCHEMA_FILENAME = "Map.json";

		private List<Floor> floors;
		private List<MapElement> mapelements;
		List<Storyline> storylines;


		public async void FetchAndParseMapJSON(){

			//fetch the JSON file from the internet
			String JSONData = await DownloadJSONAsync (JSON_BASE_URL + "/" + JSON_SCHEMA_FILENAME);

			//deserialize into JObject which we can iterate over
			var JSONPayload = JsonConvert.DeserializeObject (JSONData) as JObject;

			//deserialize and store the floors from the JSON data
			ParseFloors (JSONPayload);

			//deserialize and store the mapelements from the JSON data
			ParseMapElements (JSONPayload);

			//deserialize and store the storylines from the JSON data
			ParseStorylines (JSONPayload);
		}

		private async void ParseFloors(JObject JSONPayload){

			floors = new List<Floor>();

			foreach (var floorOBJ in JSONPayload["floorPlan"]) {

				Floor newFloor = new Floor ();

				newFloor.Plan = (String)floorOBJ ["imagePath"];
				newFloor.Id = int.Parse ((String)floorOBJ ["floorID"]);

				///


				BitmapDrawable floorImage = await DownloadImage (JSON_BASE_URL + newFloor.Plan);

				///

				floors.Add (newFloor);
			}

		}

		private void ParseMapElements (JObject JSONPayload)
		{
			mapelements = new List<MapElement>();

			foreach (var poiOBJ in JSONPayload["node"].First["poi"]) {

				//TODO: deserialize each POI

			}

			foreach (var waypointOBJ in JSONPayload["node"].First["pot"]) {

				//TODO: deserialize each waypoint

			}

		}

		private void ParseStorylines(JObject JSONPayload){

			storylines = new List<Storyline>();

			foreach (var storylineOBJ in JSONPayload["storyline"]) {

				int storylineID = int.Parse ((String)storylineOBJ ["id"]);

				Storyline newStoryline = new Storyline ();

				//TODO: associate previously-parsed MapElements to this storyline

				String newStoryLineDescrition = (String)storylineOBJ ["description"];
				storylines.Add (newStoryline);
			}
		}

		private async Task<String> DownloadJSONAsync(string url)
		{
			
			using (HttpClient client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(url))
			{
				string result = await response.Content.ReadAsStringAsync();
				return result;
			}
		}

		private async Task<BitmapDrawable> DownloadImage(string url)
		{
			BitmapDrawable image = new BitmapDrawable ();

			using (HttpClient client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(url))
			{
				//response.EnsureSuccessStatusCode ();

				using (var inputStream = await response.Content.ReadAsStreamAsync())
				{
					image = (BitmapDrawable)BitmapDrawable.CreateFromStreamAsync(inputStream, String.Empty).Result;
				}
	
				return image;
			}
		}
	}
}
