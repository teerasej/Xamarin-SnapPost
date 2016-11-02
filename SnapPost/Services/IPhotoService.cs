using System;
namespace Services
{
	public interface IPhotoService
	{
		byte[] GetByteArrayFromFilePath(string filePath);
	}
}
