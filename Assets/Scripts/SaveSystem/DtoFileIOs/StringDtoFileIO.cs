using System.IO;
using System.Threading.Tasks;
using ThisProject.SaveSystem.Serializers;

namespace ThisProject.SaveSystem.DtoFileIOs
{
    public class StringDtoFileIO : IDtoFileIO
    {
        private readonly IStringSerializer _serializer;


        public StringDtoFileIO(IStringSerializer serializer)
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