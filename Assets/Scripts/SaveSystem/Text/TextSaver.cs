using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.SaveSystem.FilePathProviders;
using ThisProject.SaveSystem.Serializers;
using UnityEngine;

namespace ThisProject.SaveSystem
{
    public class TextSaver<T, TId> : ISaver
        where T : INodeData<TId>
    {
        private readonly IObjectsStorage<T, TId> _nodes;
        private readonly IFilePathProvider _filePathProvider;
        private readonly ITextSerializer _serializer;


        public TextSaver(IObjectsStorage<T, TId> nodes, IFilePathProvider filePathProvider, ITextSerializer serializer)
        {
            _nodes = nodes;
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
                var nodesDto = _nodes.AllItems.Select(node => new NodeDataDTO<TId>
                {
                    Id = node.Id,
                    NodePosition = new Vector2DTO(node.NodePosition)
                }).ToList();

                var mainDto = new FieldSaveDTO<TId>
                {
                    Nodes = nodesDto
                };
                string textData = _serializer.Serialize(mainDto);

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