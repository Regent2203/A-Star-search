using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.SaveSystem.Dto;
using ThisProject.SaveSystem.FilePathProviders;
using ThisProject.SaveSystem.Mappers;
using ThisProject.SaveSystem.Serializers;
using UnityEngine;

namespace ThisProject.SaveSystem
{
    public class StringSaver<T, D, TId> : ISaver
        where T : INodeData<TId>
        where D : NodeDataDto<TId>
    {
        private readonly IObjectsStorage<T, TId> _nodes;
        private readonly IMapper<T, D, TId> _mapper;
        private readonly IFilePathProvider _filePathProvider;
        private readonly IStringSerializer _serializer;


        public StringSaver(IObjectsStorage<T, TId> nodes, IMapper<T, D, TId> mapper, IFilePathProvider filePathProvider, IStringSerializer serializer)
        {
            _nodes = nodes;
            _mapper = mapper;
            _filePathProvider = filePathProvider;
            _serializer = serializer;
        }

        public async Task SaveAsync()
        {
            var path = _filePathProvider.GetSaveFilePath();

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError($"Invalid file path: {path}");
                return;
            }

            try
            {
                var FieldSaveDto = new FieldSaveDto<D, TId>
                {
                    Nodes = _nodes.AllItems.Select(node => _mapper.ToDto(node)).ToList(),
                };

                string textData = _serializer.Serialize(FieldSaveDto);

                await File.WriteAllTextAsync(path, textData);
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