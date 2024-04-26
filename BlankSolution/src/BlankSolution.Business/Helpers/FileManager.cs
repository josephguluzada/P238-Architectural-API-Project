using Microsoft.AspNetCore.Http;

namespace BlankSolution.Business.Helpers;

public static class FileManager
{
	public static string SaveFile(this IFormFile file, string rootPath, string folderName)
	{
		string fileName = file.FileName;
		fileName = fileName.Length > 64 ? fileName.Substring(fileName.Length - 64, 64) : fileName;
		fileName = Guid.NewGuid().ToString() + fileName; // 100

		string path = Path.Combine(rootPath, folderName, fileName);

		using (FileStream stream = new FileStream(path, FileMode.Create))
		{
			file.CopyTo(stream);
		}

		return fileName;
	}

	public static void DeleteFile(string rootPath, string folderName, string fileName)
	{
		string deletePath = Path.Combine(rootPath, folderName, fileName);

		if (System.IO.File.Exists(deletePath))
		{
			System.IO.File.Delete(deletePath);
		}
	}
}
