using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThisProject.Implementations.Cells;
using ThisProject.Implementations.Vertexes;
using ThisProject.ObjectsStorages;
using ThisProject.Savers.FilePathProviders;
using ThisProject.Savers.Serializers;
using UnityEngine;

namespace ThisProject.Savers
{
    public class JsonSaver : ISaver
    {
        private readonly DictTypeStorage<VertexNode, int> _nodes; //todo iobj
        private readonly IFilePathProvider _filePathProvider;
        private readonly ITextSerializer _serializer;


        public JsonSaver(DictTypeStorage<VertexNode, int> nodes, IFilePathProvider filePathProvider, ITextSerializer serializer)
        {
            _nodes = nodes;
            _filePathProvider = filePathProvider;
            _serializer = serializer;
        }

        public async Task SaveAsync()
        {
            var path = _filePathProvider.GetSaveFilePath();            
            if (string.IsNullOrEmpty(path)) 
                return;

            try
            {
                var dtoList = _nodes.AllItems.Select(node => new NodeDataDTO<int>
                {
                    Id = node.Id,
                    NodePosition = new Vector2DTO(node.NodePosition)
                }).ToList();

                var rootWrapper = new { Nodes = dtoList };
                string rawData = _serializer.Serialize(rootWrapper);

                await File.WriteAllTextAsync(path, rawData);
                Debug.Log($"Data successfully saved to: {path}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Unexpected error while saving to json: {e.Message}");
                throw;
            }

        }

        public void Load() //generic?
        {
            var path = _filePathProvider.GetLoadFilePath();

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    string json = File.ReadAllText(path);

                    // Десериализуем обратно в словарь
                    Dictionary<Vector2Int, CellType> loadedGrid = JsonConvert.DeserializeObject<Dictionary<Vector2Int, CellType>>(json);

                    Debug.Log($"Карта успешно загружена из файла: {path}");
                    return;// loadedGrid;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Ошибка при чтении или парсинге файла: {e.Message}");
                    return;// null;
                }
            }
        }
    }
}
