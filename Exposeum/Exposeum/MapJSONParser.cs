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
			var json = JsonConvert.DeserializeObject (JSONData) as JObject;

			foreach (var floorOBJ in json["floorPlan"]) {
				String drawableString = (String)floorOBJ ["imagePath"];
			}

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
