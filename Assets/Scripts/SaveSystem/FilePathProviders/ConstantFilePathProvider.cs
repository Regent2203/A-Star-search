using System;
using System.IO;
using UnityEngine;

namespace EasyField.SaveSystem.FilePathProviders
{
    public class ConstantFilePathProvider : IFilePathProvider
    {
        private readonly string _filePath;


        public ConstantFilePathProvider(string fileName)
        {
            var folderPath = Application.streamingAssetsPath;
            //var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            _filePath = Path.Combine(folderPath, fileName);
        }

        public string GetLoadFilePath()
        {
            return _filePath;
        }

        public string GetSaveFilePath()
        {
            return _filePath;
        }
    }
}