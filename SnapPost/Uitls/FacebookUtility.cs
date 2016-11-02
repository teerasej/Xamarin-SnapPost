using System;
namespace Uitls
{
	public class FacebookUtility
	{
		public FacebookUtility()
		{
		}

		public static string ExtractAccessTokenFromUrl(string url)
		{
			if (url.Contains("access_token") && url.Contains("&expires_in="))
			{
				var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

				// Check if target platform is Windows or Windows Phone
				//if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
				//{
				//	at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
				//}

				var accessToken = at.Remove(at.IndexOf("&expires_in="));

				return accessToken;
			}

			return string.Empty;
		}
	}
}
