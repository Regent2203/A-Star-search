using EasyField.Serializers;
using System.IO;
using System.Threading.Tasks;

namespace EasyField.SaveSystem.FileDtoGateways
{
    public class BytesFileDtoGateway : IFileDtoGateway
    {
        private readonly IBytesSerializer _serializer;


        public BytesFileDtoGateway(IBytesSerializer serializer)
        {
            _serializer = serializer;
        }

        public async Task WriteFileAsync<TSaveDto>(string path, TSaveDto saveDto)
        {
            var data = _serializer.Serialize(saveDto);

            using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
            await fileStream.WriteAsync(data, 0, data.Length);
        }

        public async Task<TSaveDto> ReadFileAsync<TSaveDto>(string path)
        {
            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
            var buffer = new byte[fileStream.Length];
            await fileStream.ReadAsync(buffer, 0, buffer.Length);

            return _serializer.Deserialize<TSaveDto>(buffer);
        }
    }
}