using SFB;
using System;

namespace ThisProject.SaveSystem.FilePathProviders
{
    public class DialogueFilePathProvider : IFilePathProvider
    {
        private readonly ExtensionFilter[] _extensions = new[]
        {
            new ExtensionFilter("JSON Files", "json"),
            new ExtensionFilter("All Files", "*")
        };


        public string GetSaveFilePath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string path = StandaloneFileBrowser.SaveFilePanel("Save field", folderPath, "", _extensions);

            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            return path;
        }

        public string GetLoadFilePath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string[] paths = StandaloneFileBrowser.OpenFilePanel("Load field", folderPath, _extensions, false);

            if (paths == null || paths.Length == 0 || string.IsNullOrEmpty(paths[0]))
            {
                return null;
            }

            return paths[0];
        }
    }
}
