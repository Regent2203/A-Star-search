using System.Threading.Tasks;

namespace ThisProject.SaveSystem
{
    public interface ISaver
    {
        public Task SaveAsync();
    }
}