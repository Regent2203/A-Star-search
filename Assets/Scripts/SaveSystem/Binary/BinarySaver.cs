using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThisProject.Implementations.Vertexes;
using ThisProject.ObjectsStorages;
using ThisProject.SaveSystem.FilePathProviders;
using ThisProject.SaveSystem.Serializers;
using UnityEngine;

namespace ThisProject.SaveSystem
{
    public class BinarySaver : ISaver
    {
        private readonly IFilePathProvider _filePathProvider;
        private readonly IBinarySerializer _serializer;
        private readonly DictTypeStorage<VertexData, int> _nodes;

        public BinarySaver(DictTypeStorage<VertexData, int> nodes, IFilePathProvider filePathProvider, IBinarySerializer serializer)
        {
            _nodes = nodes;
            _filePathProvider = filePathProvider;
            _serializer = serializer;
        }

        public async Task SaveAsync()
        {
            string path = _filePathProvider.GetSaveFilePath();
            if (string.IsNullOrEmpty(path)) 
                return;

            try
            {
                // 1. Превращаем элементы коллекции в плоский список DTO
                var dtoList = _nodes.AllItems.Select(node => new NodeDataDTO<int>
                {
                    Id = node.Id,
                    NodePosition = new Vector2DTO(node.NodePosition)
                }).ToList();

                // 2. Упаковываем в объект-обертку, чтобы сохранить структуру с именем "Nodes"
                var rootWrapper = new FieldSaveDTO<int> { Nodes = dtoList };

                // 3. Делегируем быструю бинарную сериализацию в byte[]
                byte[] rawDataBytes = _serializer.Serialize(rootWrapper);

                // 4. Асинхронно пишем байты на диск (Zero-freeze для кадров Unity)
                await File.WriteAllBytesAsync(path, rawDataBytes);

                Debug.Log($"[SaveSystem] Binary data successfully saved to: {path} ({rawDataBytes.Length} bytes)");
            }
            catch (IOException ioEx)
            {
                Debug.LogError($"[SaveSystem] Disk I/O Error: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[SaveSystem] Unexpected error during binary save: {ex.Message}");
                throw; // Пробрасываем критические системные исключения дальше
            }
        }

        public Task LoadAsync()
        {
            //todo
            return Task.CompletedTask;            
        }
    }
}