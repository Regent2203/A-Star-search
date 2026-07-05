using System.Threading.Tasks;

namespace ThisProject.SaveSystem
{
    public interface ILoader<T>
    {
        public Task<T> LoadAsync();
    }
}