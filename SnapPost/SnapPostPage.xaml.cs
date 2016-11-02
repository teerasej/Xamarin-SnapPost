using Xamarin.Forms;
using Plugin.Media;
using System;
using Views;
using Services;

namespace SnapPost
{
	public partial class SnapPostPage : ContentPage
	{

		private FacebookServices fbServices;
		private string accessToken;
		private string photoPath;

		public SnapPostPage()
		{
			InitializeComponent();


		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			await CrossMedia.Current.Initialize();


			var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
			{
				Directory = "Nextflow",
				Name = "selfie.jpg"
			});

			if (file == null)
				return;

			this.photoPath = file.Path;

			image.Source = ImageSource.FromStream(() =>
			{
				var stream = file.GetStream();
				file.Dispose();
				return stream;
			});




		}

		async void PostToFacebook_Clicked(object sender, EventArgs e)
		{
			loading.IsRunning = true;
			var response = await fbServices.PostPhotoToMobile("Yahoo! Xamarin Dev Day", this.photoPath, this.accessToken);
			loading.IsRunning = false;

			if (response.IsSuccessStatusCode)
			{
				await DisplayAlert("Posted!", "Photo uploaded to Facebook", "OK");
			}
			else
			{
				await DisplayAlert("Oops!", "try again", "OK");
			}
		}

		async void LoginToFacebook_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Subscribe<FacebookLoginPage, string>(this, FacebookLoginPage.LOGIN_COMPLETE, HandleAction);

			await Navigation.PushModalAsync(new FacebookLoginPage());
		}

		async void HandleAction(Views.FacebookLoginPage arg1, string accessToken)
		{
			MessagingCenter.Unsubscribe<FacebookLoginPage, string>(this, FacebookLoginPage.LOGIN_COMPLETE);

			this.accessToken = accessToken;

			fbServices = new FacebookServices();

			var facebookProfile = await fbServices.GetFacebookProfileAsync(accessToken);

			labelFacebookName.Text = facebookProfile.Name;
			imageFacebookProfile.Source = facebookProfile.Picture.Data.Url;
		}
	}
}
