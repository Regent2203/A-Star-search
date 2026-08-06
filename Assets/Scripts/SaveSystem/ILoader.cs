using System.Threading.Tasks;

namespace EasyField.SaveSystem
{
    public interface ILoader
    {
        public Task<TSaveDto> LoadAsync<TSaveDto>();
    }
}