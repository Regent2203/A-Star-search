using System.Threading.Tasks;

namespace EasyField.SaveSystem.DtoFileIOs
{
    public interface IDtoFileIO
    {
        public Task WriteFileAsync<TSaveDto>(string path, TSaveDto saveDto);
        public Task<TSaveDto> ReadFileAsync<TSaveDto>(string path);
    }
}