using System;
using System.Net.Http;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace Services
{
	public class FacebookServices
	{
		public FacebookServices()
		{
		}

		public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
		{
			var requestUrl =
				"https://graph.facebook.com/v2.8/me/?fields=id,name,gender,picture&access_token="
				+ accessToken;

			var httpClient = new HttpClient();

			var userJson = await httpClient.GetStringAsync(requestUrl);

			var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

			return facebookProfile;
		}
	}
}
