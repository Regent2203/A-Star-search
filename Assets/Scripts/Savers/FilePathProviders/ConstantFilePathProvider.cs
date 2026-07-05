using UnityEngine;

namespace ThisProject.Savers.FilePathProviders
{
    public class ConstantFilePathProvider : IFilePathProvider
    {
        private readonly string _filePath;


        public ConstantFilePathProvider(string filePath)
        {
            _filePath = filePath;
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
            return Application.dataPath + _filePath; //todo
        }
    }
}