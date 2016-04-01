﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Exposeum.Tables;
using Android.Graphics.Drawables;
using Exposeum.TDGs;

namespace Exposeum
{

	public class MapJSONParser
	{

		private readonly String JSON_BASE_URL = "http://mowbray.tech/exposeum";
		private readonly String JSON_SCHEMA_FILENAME = "Map.json";

		private readonly String LOCAL_VENUE_DATA_PATH = Environment.GetFolderPath (Environment.SpecialFolder.Personal) + "/venue_data";

		private List<Floor> floors;
		private List<MapElements> mapelements;
		private List<Storyline> storylines;

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

			//send the parsed data to be saved in the database
			StoreParsedDataDB();
		}

		private async void ParseFloors(JObject JSONPayload){

			floors = new List<Floor>();

			foreach (var floorOBJ in JSONPayload["floorPlan"]) {

				Floor newFloor = new Floor ();

				newFloor.ImagePath = (String)floorOBJ ["imagePath"];
				newFloor.Id = int.Parse ((String)floorOBJ ["floorID"]);

				floors.Add (newFloor);

				await SaveAndGetImage (newFloor.ImagePath);

			}

		}

		private void ParseMapElements (JObject JSONPayload)
		{
			mapelements = new List<MapElements>();

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

				Storyline newStoryline = new Storyline ();

				int storylineID = int.Parse ((String)storylineOBJ ["id"]);

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

		//download, save, and return the insance of the image
		private async Task<BitmapDrawable> SaveAndGetImage(string path)
		{
			BitmapDrawable image = new BitmapDrawable ();

			using (HttpClient client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(JSON_BASE_URL + path))
			{
				response.EnsureSuccessStatusCode ();

				var inputStream = await response.Content.ReadAsByteArrayAsync ();

				string localPath = LOCAL_VENUE_DATA_PATH + path;

				System.IO.Directory.CreateDirectory (Path.GetDirectoryName (localPath)); //create folder if it doesn't already exist

				FileStream fs = new FileStream (localPath, FileMode.Create);

				await fs.WriteAsync (inputStream, 0, inputStream.Length); //write the downloaded data to the device
				fs.Close ();

				image = (BitmapDrawable)BitmapDrawable.CreateFromPath (localPath); //instanciate the drawable from the just-saved data

				return image;
			}
		}

		//send the parsed objects to the DB to be serialized
		private void StoreParsedDataDB(){

			//save the floors
			FloorTdg floorTdg = FloorTdg.GetInstance();

			foreach (var floor in floors) {
				floorTdg.Add (floor);
			}

			//TODO: save the mapelements

			//TODO: save the storylines
		}
	}
}
