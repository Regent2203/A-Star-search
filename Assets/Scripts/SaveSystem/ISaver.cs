using System.Threading.Tasks;

namespace EasyField.SaveSystem
{
    public interface ISaver
    {
        public Task SaveAsync<TSaveDto>(TSaveDto saveDto);
    }
}