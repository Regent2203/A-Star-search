using SFB;
using System;
using UnityEngine;

namespace EasyField.SaveSystem.FilePathProviders
{
    public class SFBDialogueFilePathProvider : IFilePathProvider
    {
        private readonly string _folderPath = string.Empty;

        private readonly ExtensionFilter[] _extensions = new[]
        {
            new ExtensionFilter("JSON Files", "json"),
            new ExtensionFilter("All Files", "*")
        };


        public SFBDialogueFilePathProvider()
        {
            _folderPath = Application.streamingAssetsPath;
            //_folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }


        public string GetSaveFilePath()
        {
            string path = StandaloneFileBrowser.SaveFilePanel("Save field", _folderPath, "", _extensions);

            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            return path;
        }

        public string GetLoadFilePath()
        {
            string[] paths = StandaloneFileBrowser.OpenFilePanel("Load field", _folderPath, _extensions, false);

            if (paths == null || paths.Length == 0 || string.IsNullOrEmpty(paths[0]))
            {
                return null;
            }

            return paths[0];
        }
    }
}