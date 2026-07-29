using System.IO;
using System.IO.Compression;

namespace ThisProject.SaveSystem.Serializers
{
    public class GZipCompressedBytesSerializer : IBytesSerializer
    {
        private readonly IBytesSerializer _originSerializer;


        public GZipCompressedBytesSerializer(IBytesSerializer originSerializer)
        {
            _originSerializer = originSerializer;
        }

        public byte[] Serialize<T>(T obj)
        {
            byte[] rawBytes = _originSerializer.Serialize(obj);

            using var memoryStream = new MemoryStream();
            using (var compressionStream = new GZipStream(memoryStream, CompressionMode.Compress))
            {
                compressionStream.Write(rawBytes, 0, rawBytes.Length);
            }
            return memoryStream.ToArray();
        }

        public T Deserialize<T>(byte[] bytes)
        {
            using var memoryStream = new MemoryStream(bytes);
            using var decompressionStream = new GZipStream(memoryStream, CompressionMode.Decompress);
            using var resultStream = new MemoryStream();

            decompressionStream.CopyTo(resultStream);
            byte[] rawBytes = resultStream.ToArray();

            return _originSerializer.Deserialize<T>(rawBytes);
        }
    }
}