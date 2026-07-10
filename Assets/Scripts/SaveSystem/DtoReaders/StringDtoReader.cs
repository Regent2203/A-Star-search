using System.IO;
using System.Threading.Tasks;
using ThisProject.SaveSystem.Serializers;

namespace ThisProject.SaveSystem.DtoReaders
{
    public class StringDtoReader : IDtoReader
    {
        private readonly IStringSerializer _serializer;

        public StringDtoReader(IStringSerializer serializer)
        {
            _serializer = serializer;
        }

        public async Task<TSaveDto> ReadFileAsync<TSaveDto>(string path)
        {
            var data = await File.ReadAllTextAsync(path);

            var saveDto = _serializer.Deserialize<TSaveDto>(data);

            return saveDto;
        }
    }
}