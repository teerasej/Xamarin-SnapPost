using Xamarin.Forms;
using Plugin.Media;
using System;
using Views;

namespace SnapPost
{
	public partial class SnapPostPage : ContentPage
	{
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

		void HandleAction(Views.FacebookLoginPage arg1, string accessToken)
		{
			MessagingCenter.Unsubscribe<FacebookLoginPage, string>(this, FacebookLoginPage.LOGIN_COMPLETE);


		}
	}
}
