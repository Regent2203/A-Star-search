using System;
using System.IO;

namespace ThisProject.Savers.FilePathProviders
{
    public class ConstantFilePathProvider : IFilePathProvider
    {
        private readonly string _fileName;


        public ConstantFilePathProvider(string filePath)
        {
            _fileName = filePath;
        }

        public string GetLoadFilePath()
        {
            return GetFilePath();
        }

        public string GetSaveFilePath()
        {
            return GetFilePath();
        }

        private string GetFilePath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            return Path.Combine(folderPath, _fileName);
        }
    }
}