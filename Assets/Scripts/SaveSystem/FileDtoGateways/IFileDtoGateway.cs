using System.Threading.Tasks;

namespace EasyField.SaveSystem.FileDtoGateways
{
    public interface IFileDtoGateway
    {
        public Task WriteFileAsync<TSaveDto>(string path, TSaveDto saveDto);
        public Task<TSaveDto> ReadFileAsync<TSaveDto>(string path);
    }
}