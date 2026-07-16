using System;
using System.IO;
using System.Threading.Tasks;
using ThisProject.SaveSystem.Dto;
using ThisProject.SaveSystem.DtoFileIOs;
using ThisProject.SaveSystem.FilePathProviders;
using UnityEngine;

namespace ThisProject.SaveSystem
{
    public class Saver : ISaver
    {
        private readonly IFilePathProvider _filePathProvider;
        private readonly IDtoFileIO _dtoFile;


        public Saver(IFilePathProvider filePathProvider, IDtoFileIO dtoFile)
        {
            _filePathProvider = filePathProvider;
            _dtoFile = dtoFile;
        }

        public async Task SaveAsync<TSaveDto>(TSaveDto saveDto)
        {
            var path = _filePathProvider.GetSaveFilePath();

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError($"Invalid file path: {path}");
                return;
            }

            try
            {
                await _dtoFile.WriteFileAsync<TSaveDto>(path, saveDto);
                Debug.Log($"Data successfully saved to: {path}");
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