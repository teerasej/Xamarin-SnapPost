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

			App.photoPath = file.Path;

			image.Source = ImageSource.FromStream(() =>
			{
				var stream = file.GetStream();
				file.Dispose();
				return stream;
			});

		}

		async void LoginToFacebook_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Subscribe<FacebookLoginPage, string>(this, FacebookLoginPage.LOGIN_COMPLETE, HandleAction);

			await Navigation.PushModalAsync(new FacebookLoginPage());
		}

		async void HandleAction(Views.FacebookLoginPage arg1, string accessToken)
		{
			MessagingCenter.Unsubscribe<FacebookLoginPage, string>(this, FacebookLoginPage.LOGIN_COMPLETE);

			fbServices = new FacebookServices();

			var facebookProfile = await fbServices.GetFacebookProfileAsync(accessToken);

			labelFacebookName.Text = facebookProfile.Name;
			imageFacebookProfile.Source = facebookProfile.Picture.Data.Url;
		}
	}
}
