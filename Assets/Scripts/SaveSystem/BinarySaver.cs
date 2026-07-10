using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThisProject.Implementations.Vertexes;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.SaveSystem.Dto;
using ThisProject.SaveSystem.FilePathProviders;
using ThisProject.SaveSystem.Mappers;
using ThisProject.SaveSystem.Serializers;
using UnityEngine;

namespace ThisProject.SaveSystem
{
    public class BinarySaver<T, Dto, TId> : ISaver
        where T : INodeData<TId>
        where Dto : NodeDataDto<TId>
    {
        private readonly IObjectsStorage<T, TId> _nodes;
        private readonly IMapper<T, Dto, TId> _mapper;
        private readonly IFilePathProvider _filePathProvider;
        private readonly IBinarySerializer _serializer;


        public BinarySaver(IObjectsStorage<T, TId> nodes, IMapper<T, Dto, TId> mapper, IFilePathProvider filePathProvider, IBinarySerializer serializer)
        {
            _nodes = nodes;
            _mapper = mapper;
            _filePathProvider = filePathProvider;
            _serializer = serializer;
        }

        public async Task SaveAsync()
        {
            string path = _filePathProvider.GetSaveFilePath();

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError($"Invalid file path: {path}");
                return;
            }
            
            try
            {
                var FieldSaveDto = new FieldSaveDto<Dto, TId>
                {
                    Nodes = _nodes.AllItems.Select(node => _mapper.ToDto(node)).ToList(),
                };

                byte[] rawDataBytes = _serializer.Serialize(FieldSaveDto);

                await File.WriteAllBytesAsync(path, rawDataBytes);
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