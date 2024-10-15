namespace Demo.PL.Helper
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
            //1.get located folder path
            var folderPath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Files" ,folderName);
            //2.get file name and make it unique
            var fileName=$"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
            //3.get file path
            var filePath=Path.Combine(folderPath,fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;
        }
    }
}
