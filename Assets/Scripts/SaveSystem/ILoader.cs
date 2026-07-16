using System.Threading.Tasks;

namespace ThisProject.SaveSystem
{
    public interface ILoader
    {
        public Task<TSaveDto> LoadAsync<TSaveDto>();
    }
}