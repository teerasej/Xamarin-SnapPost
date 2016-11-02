using System;
using System.IO;
using Services;

[assembly: Xamarin.Forms.Dependency(typeof(PhotoService))]
namespace Services
{
	public class PhotoService : IPhotoService
	{
		public PhotoService()
		{
		}

		public byte[] GetPhotoAsByteArray(string filePath)
		{
			var result = File.ReadAllBytes(filePath);
			return result;
		}
	}
}
