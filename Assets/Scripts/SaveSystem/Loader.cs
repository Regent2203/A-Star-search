using System;
using System.IO;
using System.Threading.Tasks;
using ThisProject.SaveSystem.DtoFileIOs;
using ThisProject.SaveSystem.FilePathProviders;
using UnityEngine;

namespace ThisProject.SaveSystem
{
    public class Loader: ILoader
    {
        private readonly IFilePathProvider _filePathProvider;
        private readonly IDtoFileIO _dtoFile;

        public Loader(IFilePathProvider filePathProvider, IDtoFileIO dtoFile)
        {
            _filePathProvider = filePathProvider;
            _dtoFile = dtoFile;
        }

        public async Task<TSaveDto> LoadAsync<TSaveDto>()
        {
            var path = _filePathProvider.GetLoadFilePath();

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                Debug.LogError($"File does not exist at path: {path}");
                return default;
            }

            try
            {
                var saveDto = await _dtoFile.ReadFileAsync<TSaveDto>(path);

                if (saveDto == null)
                {
                    Debug.LogError($"Deserialization error: file is corrupt at path: {path}");
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