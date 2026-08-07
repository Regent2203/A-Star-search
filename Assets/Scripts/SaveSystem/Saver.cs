using EasyField.SaveSystem.FileDtoGateways;
using EasyField.SaveSystem.FilePathProviders;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace EasyField.SaveSystem
{
    public class Saver : ISaver
    {
        private readonly IFilePathProvider _filePathProvider;
        private readonly IFileDtoGateway _fileDtoGateway;


        public Saver(IFilePathProvider filePathProvider, IFileDtoGateway fileDtoGateway)
        {
            _filePathProvider = filePathProvider;
            _fileDtoGateway = fileDtoGateway;
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
                await _fileDtoGateway.WriteFileAsync<TSaveDto>(path, saveDto);
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