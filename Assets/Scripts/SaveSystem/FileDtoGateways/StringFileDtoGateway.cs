using EasyField.Serializers;
using System.IO;
using System.Threading.Tasks;

namespace EasyField.SaveSystem.FileDtoGateways
{
    public class StringFileDtoGateway : IFileDtoGateway
    {
        private readonly IStringSerializer _serializer;


        public StringFileDtoGateway(IStringSerializer serializer)
        {
            _serializer = serializer;
        }

        public async Task WriteFileAsync<TSaveDto>(string path, TSaveDto saveDto)
        {
            var data = _serializer.Serialize<TSaveDto>(saveDto);

            using var streamWriter = new StreamWriter(path);
            await streamWriter.WriteAsync(data);
        }

        public async Task<TSaveDto> ReadFileAsync<TSaveDto>(string path)
        {
            using var streamReader = new StreamReader(path);
            var data = await streamReader.ReadToEndAsync();

            return _serializer.Deserialize<TSaveDto>(data);
        }
    }
}