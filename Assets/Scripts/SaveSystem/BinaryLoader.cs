using System;
using System.IO;
using System.Threading.Tasks;
using ThisProject.SaveSystem.FilePathProviders;
using ThisProject.SaveSystem.Serializers;
using UnityEngine;

namespace ThisProject.SaveSystem
{
    public class BinaryLoader<T> : ILoader<T>
    {
        private readonly IFilePathProvider _filePathProvider;
        private readonly IBinarySerializer _serializer;

        public BinaryLoader(IFilePathProvider filePathProvider, IBinarySerializer serializer)
        {
            _filePathProvider = filePathProvider;
            _serializer = serializer;
        }

        public async Task<T> LoadAsync()
        {
            var path = _filePathProvider.GetLoadFilePath();

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                Debug.LogError($"File does not exist at path: {path}");
                return default;
            }

            try
            {
                byte[] rawDataBytes = await File.ReadAllBytesAsync(path);

                var mainDto = _serializer.Deserialize<T>(rawDataBytes);

                if (mainDto == null)
                {
                    Debug.LogError($"Deserialization error: file is corrupt at {path}");
                    return default;
                }

                Debug.Log($"Data successfully loaded from: {path}");
                return mainDto;
            }
            catch (IOException ioEx)
            {
                Debug.LogError($"Disk I/O error while saving: {ioEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Unexpected error while saving: {ex.Message}");
                throw;
            }
        }
    }
}