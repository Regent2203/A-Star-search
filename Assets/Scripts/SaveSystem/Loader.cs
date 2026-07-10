using System;
using System.IO;
using System.Threading.Tasks;
using ThisProject.SaveSystem.DtoReaders;
using ThisProject.SaveSystem.FilePathProviders;
using UnityEngine;

namespace ThisProject.SaveSystem
{
    public class Loader<TSaveDto> : ILoader<TSaveDto>
    {
        private readonly IFilePathProvider _filePathProvider;
        private readonly IDtoReader _dtoReader;

        public Loader(IFilePathProvider filePathProvider, IDtoReader dtoReader)
        {
            _filePathProvider = filePathProvider;
            _dtoReader = dtoReader;
        }

        public async Task<TSaveDto> LoadAsync()
        {
            var path = _filePathProvider.GetLoadFilePath();

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                Debug.LogError($"File does not exist at path: {path}");
                return default;
            }

            try
            {
                var saveDto = await _dtoReader.ReadFileAsync<TSaveDto>(path);

                if (saveDto == null)
                {
                    Debug.LogError($"Deserialization error: file is corrupt at {path}");
                    return default;
                }

                Debug.Log($"Data successfully loaded from: {path}");
                return saveDto;
            }
            catch (IOException ioEx)
            {
                Debug.LogError($"Disk I/O error while loading: {ioEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Unexpected error while loading: {ex.Message}");
                throw;
            }
        }
    }
}