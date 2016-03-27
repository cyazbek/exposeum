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

		private static String JSON_BASE_URL = "http://mowbray.tech/exposeum/";
		private static String JSON_SCHEMA_FILENAME = "exposeum.json";

		public async void FetchAndParseMapJSON(){

			//fetch the JSON file from the internet
			String JSONData = await DownloadJSONAsync (JSON_BASE_URL + JSON_SCHEMA_FILENAME);

			//deserialize into JObject which we can iterate over
			var JSONPayload = JsonConvert.DeserializeObject (JSONData) as JObject;

			//deserialize and store the floors from the JSON data
			List<Floor> floors = ParseFloors (JSONPayload);

			//deserialize and store the storylines from the JSON data
			List<Storyline> storylines = ParseStorylines (JSONPayload);
		}

		private List<Floor> ParseFloors(JObject JSONPayload){

			List<Floor> floors = new List<Floor>();

			foreach (var floorOBJ in JSONPayload["floorPlan"]) {

				String drawableString = (String)floorOBJ ["imagePath"];

				Floor newFloor = new Floor ();

				newFloor.Plan = drawableString;
				newFloor.Id = int.Parse ((String)floorOBJ ["floorID"]); //get the floor ID for possible later use

				floors.Add (newFloor);
			}

			return floors;

		}

		private List<Storyline> ParseStorylines(JObject JSONPayload){

			List<Storyline> storylines = new List<Storyline>();

			foreach (var storylineOBJ in JSONPayload["storyline"]) {

				int storylineID = int.Parse ((String)storylineOBJ ["id"]);

				Storyline newStoryline = new Storyline ();

				//TODO: associate previously-parsed MapElements to this storyline

				String newStoryLineDescrition = (String)storylineOBJ ["description"];
				storylines.Add (newStoryline);
			}

			return storylines;
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
