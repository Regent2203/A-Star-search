namespace ThisProject.SaveSystem.FilePathProviders
{
    public interface IFilePathProvider
    {
        public string GetSaveFilePath();
        public string GetLoadFilePath();
    }
}