namespace ArticleProject.Web.Helper
{
    public static class FileSettings
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
            // folder path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", folderName);
            // file nameing
            var namingFile = $"{Guid.NewGuid()}-{file.FileName}";
            // get file path
            var filePath = Path.Combine(folderPath, namingFile);
            // save file to stream
            using var asStream = new FileStream(filePath,FileMode.Create);
            file.CopyTo(asStream);

            return namingFile;
        }

        public static void DeleteFile (string fileName , string folderName)
        {
            // file path
            var filePath = Path.Combine (Directory.GetCurrentDirectory(), "wwwroot\\img", folderName,fileName);
            if(File.Exists(filePath) )
            {
                File.Delete(filePath);
            }
        }
    }
}
