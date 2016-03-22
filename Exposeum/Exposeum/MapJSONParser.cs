using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Exposeum
{

	public class MapJSONParser
	{

		private static String JSON_URL = "http://mowbray.tech/exposeum/exposeum.json";

		public async void ParseMapJSON(){

			String JSONData = await DownloadJSONAsync (JSON_URL);

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
