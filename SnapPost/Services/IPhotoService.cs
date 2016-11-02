using System;
namespace Services
{
	public interface IPhotoService
	{
		byte[] GetPhotoAsByteArray(string path);
	}
}
