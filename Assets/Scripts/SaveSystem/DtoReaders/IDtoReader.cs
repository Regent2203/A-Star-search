using System.Threading.Tasks;

namespace ThisProject.SaveSystem.DtoReaders
{
    public interface IDtoReader
    {
        Task<T> ReadFileAsync<T>(string path);
    }
}