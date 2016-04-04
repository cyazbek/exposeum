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
		String JSONData;

		private readonly String JSON_BASE_URL = "http://mowbray.tech/exposeum";
		private readonly String JSON_SCHEMA_FILENAME = "Map.json";

		private readonly String LOCAL_VENUE_DATA_PATH = Environment.GetFolderPath (Environment.SpecialFolder.Personal) + "/venue_data";

		private List<Floor> floors;
		private List<MapElements> mapelements;
		private List<Storyline> storylines;
		private List<Beacon> beacons;

		private List<PoiDescriptionEn> englishPOIDescriptions;
		private List<PoiDescriptionFr> frenchPOIDescriptions;

		private List<ExhibitionContentEn> englishExhibitionContent;
		private List<ExhibitionContentFr> frenchExhibitionContent;

		private int newBeaconId = 0; //we aren't given but we need new beacon Ids
		private int newPoiDescriptionId = 0, newStorylineDescriptionId = 0;

		private int audioId = 0, videoId = 0, imageId = 0;

		public async void FetchAndParseMapJSON(){

			//fetch the JSON file from the internet
			JSONData = await DownloadJSONAsync (JSON_BASE_URL + "/" + JSON_SCHEMA_FILENAME);

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
			englishPOIDescriptions = new List<PoiDescriptionEn>();
			frenchPOIDescriptions = new List<PoiDescriptionFr>();

			englishExhibitionContent = new List<ExhibitionContentEn>();
			frenchExhibitionContent = new List<ExhibitionContentFr> ();

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

				PoiDescriptionEn newPOIDescriptionEN = new PoiDescriptionEn
				{
					Id = newPoiDescriptionId, //TODO: extract data from the html??
					Title = poiOBJ["description"].First["description"].ToString(),
					Summary = poiOBJ["description"].First["description"].ToString(),
					Description = poiOBJ["description"].First["description"].ToString()
				};

				PoiDescriptionFr newPOIDescriptionFR = new PoiDescriptionFr
				{
					Id = newPoiDescriptionId,
					Title = poiOBJ["description"].First["description"].ToString(), //TODO: extract data from the html??
					Summary = poiOBJ["description"].First["description"].ToString(),
					Description = poiOBJ["description"].First["description"].ToString()
				};

				englishPOIDescriptions.Add (newPOIDescriptionEN);
				frenchPOIDescriptions.Add (newPOIDescriptionFR);

				MapElements newPOI = new MapElements {
					
					Id = int.Parse (poiOBJ ["id"].ToString ()),
					Discriminator = "PointOfInterest",
					Visited = 0, //default unvisited
					BeaconId = currentPOIsBeacon.Id,
					StoryLineId = int.Parse(poiOBJ["storyPoint"].First["storylineID"].ToString()),
					PoiDescription = newPoiDescriptionId++, //increment
					FloorId = int.Parse (poiOBJ ["floorID"].ToString ()),
					UCoordinate = float.Parse(poiOBJ ["x"].ToString ()) / getFloorWidth(int.Parse (poiOBJ ["floorID"].ToString ())),
					VCoordinate = float.Parse(poiOBJ ["y"].ToString ()) / getFloorHeight(int.Parse (poiOBJ ["floorID"].ToString ()))
				};
		
				AddExhibitionContent (newPOI.Id, poiOBJ ["media"]);

				mapelements.Add (newPOI);

			}

			foreach (var waypointOBJ in JSONPayload["node"].First["pot"]) {

				MapElements newWaypoint = new MapElements {

					Id = int.Parse (waypointOBJ ["id"].ToString ()),
					IconPath = String.Empty,
					Discriminator = "Waypoint",
					Visited = 0, //default unvisited
					Label = (waypointOBJ ["label"].ToString ()),
					FloorId = int.Parse (waypointOBJ ["floorID"].ToString ()),
					UCoordinate = float.Parse(waypointOBJ ["x"].ToString ()) / getFloorWidth(int.Parse (waypointOBJ ["floorID"].ToString ())),
					VCoordinate = float.Parse(waypointOBJ ["y"].ToString ()) / getFloorHeight(int.Parse (waypointOBJ ["floorID"].ToString ()))
				};

				mapelements.Add (newWaypoint);

			}

		}

		private async void ParseStorylines(JObject JSONPayload){

			storylines = new List<Storyline>();

			foreach (var storylineOBJ in JSONPayload["storyline"]) {

				StoryLineDescriptionEn newStorylineDescriptionEN = new StoryLineDescriptionEn
				{
					Id = newStorylineDescriptionId, //TODO: extract data from the html??
					Title = storylineOBJ["title"].ToString(),
					Description = storylineOBJ["description"].ToString(),
				};

				StoryLineDescriptionFr newStorylineDescriptionFR = new StoryLineDescriptionFr
				{
					Id = newStorylineDescriptionId, //increment
					Title = storylineOBJ["title"].ToString(),
					Description = storylineOBJ["description"].ToString(),
				};

				Storyline newStoryline = new Storyline {

					Id = int.Parse (storylineOBJ ["id"].ToString ()),
					Duration = int.Parse (storylineOBJ ["walkingTimeInMinutes"].ToString ()),
					ImagePath = storylineOBJ ["thumbnail"].ToString (),
					FloorsCovered = int.Parse (storylineOBJ ["floorsCovered"].ToString ()),
					Status = 2,
					DescriptionId = newStorylineDescriptionId++

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

		private void AddExhibitionContent(int PoiID, JToken JsonObj){

			//loop for video

			foreach (var videoObj in JsonObj["video"]) {
				
				ExhibitionContentEn newEnglishExContent = new ExhibitionContentEn {
					Id = videoId,
					Title = (videoObj["caption"]).ToString(),
					PoiId = PoiID,
					Filepath = (videoObj["path"]).ToString(),
					Duration = -1, //not given by map team
					Encoding = string.Empty, //not gven by map team
					Resolution = -1 //not given by map team
				};

				ExhibitionContentFr newFrenchExContent = new ExhibitionContentFr {
					Id = videoId,
					Title = (videoObj["caption"]).ToString(),
					PoiId = PoiID,
					Filepath = (videoObj["path"]).ToString(),
					Duration = -1, //not given by map team
					Encoding = string.Empty, //not gven by map team
					Resolution = -1 //not given by map team
				};

				englishExhibitionContent.Add (newEnglishExContent);
				frenchExhibitionContent.Add (newFrenchExContent);

				FetchAndSaveImage (newEnglishExContent.Filepath);
				FetchAndSaveImage (newFrenchExContent.Filepath);
					
			}

			//loop for audio

			foreach (var audioObj in JsonObj["audio"]) {
				//not yet implemented by map team
			}

			//loop for images

			foreach (var imageObj in JsonObj["image"]) {

				ExhibitionContentEn newEnglishExContent = new ExhibitionContentEn {
					Id = imageId,
					Title = (imageObj["caption"]).ToString(),
					PoiId = PoiID,
					Filepath = (imageObj["path"]).ToString(),
				};

				ExhibitionContentFr newFrenchExContent = new ExhibitionContentFr {
					Id = imageId++,
					Title = (imageObj["caption"]).ToString(),
					PoiId = PoiID,
					Filepath = (imageObj["path"]).ToString(),
				};

				englishExhibitionContent.Add (newEnglishExContent);
				frenchExhibitionContent.Add (newFrenchExContent);

				FetchAndSaveImage (newEnglishExContent.Filepath);
				FetchAndSaveImage (newFrenchExContent.Filepath);
			}
		}

		private float getFloorWidth(int floorID){

			var JSONPayload = JsonConvert.DeserializeObject (JSONData) as JObject;

			foreach (var floorOBJ in JSONPayload["floorPlan"]) {

				if (int.Parse ((String)floorOBJ ["floorID"]) == floorID) {
					return float.Parse ((String)floorOBJ ["imageWidth"]);
				}
			}

			return -1.0f; //not found

		}

		private float getFloorHeight(int floorID){
			var JSONPayload = JsonConvert.DeserializeObject (JSONData) as JObject;

			foreach (var floorOBJ in JSONPayload["floorPlan"]) {

				if (int.Parse ((String)floorOBJ ["floorID"]) == floorID) {
					return float.Parse ((String)floorOBJ ["imageHeight"]);
				}
			}

			return -1.0f; //not found
		}
	}
}