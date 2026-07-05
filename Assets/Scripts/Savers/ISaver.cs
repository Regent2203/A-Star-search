using System.Threading.Tasks;

namespace ThisProject.Savers
{
    public interface ISaver
    {
        public Task SaveAsync();
        public void Load();
    }
}
