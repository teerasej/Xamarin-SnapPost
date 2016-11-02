using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Uitls;

namespace Views
{
	public partial class FacebookLoginPage : ContentPage
	{

		public static string LOGIN_COMPLETE = "login_complete";

		private string appID = "1731240773866467";

		public FacebookLoginPage()
		{
			InitializeComponent();


			// public_profile,user_photos,publish_actions

			var apiRequest =
				"https://www.facebook.com/dialog/oauth?client_id="
				+ appID
				+ "&scope=public_profile"
				+ "&response_type=token"
				+ "&redirect_uri=https://www.facebook.com/connect/login_success.html";

			var webView = new WebView
			{
				Source = apiRequest,
				HeightRequest = 1
			};

			webView.Navigated += WebView_Navigated;

			Content = webView;
		}

		async void WebView_Navigated(object sender, WebNavigatedEventArgs e)
		{
			var accessToken = FacebookUtility.ExtractAccessTokenFromUrl(e.Url);

			if (accessToken != "")
			{
				MessagingCenter.Send<FacebookLoginPage, string>(this, FacebookLoginPage.LOGIN_COMPLETE, accessToken);
				await Navigation.PopModalAsync();
			}
		}


	}
}
