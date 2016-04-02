using System;
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
		private List<Beacon> beacons;

		private int newBeaconId = 0; //we aren't given but we need new beacon Ids

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

				await FetchAndSaveImage (newFloor.ImagePath);

			}

		}

		private void ParseMapElements (JObject JSONPayload)
		{
			mapelements = new List<MapElements>();
			beacons = new List<Beacon> ();

			foreach (var poiOBJ in JSONPayload["node"].First["poi"]) {

				String newPOIBeaconUUID = poiOBJ ["ibeacon"] ["uuid"].ToString ();
				int newPOIBeaconMaj = int.Parse (poiOBJ ["ibeacon"] ["major"].ToString ());
				int newPOIBeaconMin = int.Parse (poiOBJ ["ibeacon"] ["minor"].ToString ());

				Beacon currentPOIsBeacon = beacons.FirstOrDefault (beacon => beacon.Uuid == newPOIBeaconUUID && beacon.Major == newPOIBeaconMin && beacon.Minor == newPOIBeaconMin);

				//if the current POI's beacon is not already in the list, add it to the list and use it

				if (currentPOIsBeacon == null) {
					currentPOIsBeacon = new Beacon {
						Uuid = newPOIBeaconUUID,
						Major = newPOIBeaconMaj,
						Minor = newPOIBeaconMin,
						Id = newBeaconId++ //use and then increment the new beacon ID counter
					};

					beacons.Add (currentPOIsBeacon);
				}

				MapElements newPOI = new MapElements {
					
					Id = int.Parse (poiOBJ ["id"].ToString ()),
					UCoordinate = 0.0f, //TODO: FIX
					VCoordinate = 0.0f,
					IconPath = poiOBJ ["description"].ToString (),
					Discriminator = "PointOfInterest",
					Visited = 0, //default unvisited
					BeaconId = currentPOIsBeacon.Id,

					//TODO: Storyline ID!
					//TODO: POIDescription!
					//TODO: Label!

					FloorId = int.Parse (poiOBJ ["floorID"].ToString ())

				};
		
				mapelements.Add (newPOI);
			}

			foreach (var waypointOBJ in JSONPayload["node"].First["pot"]) {

				//TODO: deserialize each waypoint

			}

		}

		private async void ParseStorylines(JObject JSONPayload){

			storylines = new List<Storyline>();

			foreach (var storylineOBJ in JSONPayload["storyline"]) {

				Storyline newStoryline = new Storyline {

					Id = int.Parse (storylineOBJ ["id"].ToString ()),
					Duration = int.Parse (storylineOBJ ["walkingTimeInMinutes"].ToString ()),
					ImagePath = storylineOBJ ["thumbnail"].ToString (),
					FloorsCovered = int.Parse (storylineOBJ ["floorsCovered"].ToString ()),

					//TODO: lastVisitedPOI!

					Status = 0 //TODO: Check w/ Andrea or Firas
					
					//TODO: DescriptionId!

				};

				await FetchAndSaveImage (newStoryline.ImagePath); //fetch and save the thumbnail to the device

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

		//download and save an image to the device's storage
		private async Task FetchAndSaveImage(string path)
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
			}
		}

		//send the parsed objects to the DB to be serialized
		private void StoreParsedDataDB(){

			//save the floors
			FloorTdg floorTdg = FloorTdg.GetInstance();

			foreach (var floor in floors) {
				floorTdg.Add (floor);
			}

			//save the mapelements
			MapElementsTdg mapElementsTdg = MapElementsTdg.GetInstance();

			foreach (var mapelement in mapelements) {
				mapElementsTdg.Add (mapelement);
			}

			//save the storylines

			StorylineTdg storylineTdg = StorylineTdg.GetInstance();

			foreach (var storyline in storylines) {
				storylineTdg.Add (storyline);
			}

			//save the beacons

			BeaconTdg beaconTdg = BeaconTdg.GetInstance();

			foreach (var beacon in beacons) {
				beaconTdg.Add (beacon);
			}
		}
	}
}
