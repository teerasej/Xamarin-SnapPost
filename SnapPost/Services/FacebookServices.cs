using System;
using System.Net.Http;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;
using Xamarin.Forms;

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

		public async Task<HttpResponseMessage> PostPhotoToMobile(string caption , string filePath , string accessToken)
		{

			var myPicArray = DependencyService.Get<IPhotoService>().GetByteArrayFromFilePath(filePath);



			HttpClient httpClient = new HttpClient();
			httpClient.BaseAddress = new Uri("https://graph.facebook.com/v2.8/");

			MultipartFormDataContent form = new MultipartFormDataContent();
			HttpContent content = new ByteArrayContent(myPicArray);
			form.Add(content, "media", "test");

			var captionText = Uri.EscapeUriString(caption);
			HttpResponseMessage response = await httpClient.PostAsync(
				"me/photos?caption=" 
				+ captionText 
				+ "&access_token=" + accessToken, form);
			return response;
		}
	}
}
