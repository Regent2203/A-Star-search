using System.IO;
using System.Threading.Tasks;
using ThisProject.SaveSystem.Serializers;

namespace ThisProject.SaveSystem.DtoReaders
{
    public class BinaryDtoReader : IDtoReader
    {
        private readonly IBinarySerializer _serializer;

        public BinaryDtoReader(IBinarySerializer serializer)
        {
            _serializer = serializer;
        }

        public async Task<TSaveDto> ReadFileAsync<TSaveDto>(string path)
        {
            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
            var buffer = new byte[fileStream.Length];
            await fileStream.ReadAsync(buffer, 0, buffer.Length);

            var saveDto = _serializer.Deserialize<TSaveDto>(buffer);

            return saveDto;
        }
    }
}