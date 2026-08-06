namespace EasyField.SaveSystem.FilePathProviders
{
    public interface IFilePathProvider
    {
        public string GetSaveFilePath();
        public string GetLoadFilePath();
    }
}