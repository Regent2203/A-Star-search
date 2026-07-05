namespace ThisProject.Savers.FilePathProviders
{
    public interface IFilePathProvider
    {
        public string GetSaveFilePath();
        public string GetLoadFilePath();
    }
}