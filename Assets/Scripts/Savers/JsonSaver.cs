using System;
using System.Collections.Generic;
using System.IO;
using ThisProject.Fields;
using ThisProject.Implementations.Cells;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using UnityEngine;
using Newtonsoft.Json;
using SFB;
using ThisProject.Savers.FilePathProviders;
using ThisProject.Implementations.Vertexes;

namespace ThisProject.Savers
{
    public class JsonSaver : ISaver
    {
        private readonly IFilePathProvider _filePathProvider;
        private readonly DictTypeStorage<VertexNode, int> _nodes;


        public JsonSaver(DictTypeStorage<VertexNode, int> nodes, IFilePathProvider filePathProvider)
        {
            _nodes = nodes;
            _filePathProvider = filePathProvider;
            //todo enumerator
        }

        public void Save()
        {
            var path = _filePathProvider.GetSaveFilePath();

            try
            {
                // Сериализуем словарь напрямую с помощью Newtonsoft.Json
                string json = JsonConvert.SerializeObject(_nodes, Formatting.Indented); //todo nodes
                
                Debug.Log(_nodes.GetItemById(1).NodePosition);

                // Записываем данные по выбранному пользователем пути
                File.WriteAllText(path, json);
                Debug.Log($"Карта успешно сохранена в файл: {path}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Ошибка при сохранении файла: {e.Message}");
            }
        }

        public void Load() //generic?
        {
            var path = _filePathProvider.GetLoadFilePath();            

            try
            {
                // Читаем весь текст из выбранного файла
                string json = File.ReadAllText(path);

                // Десериализуем обратно в словарь
                Dictionary<Vector2Int, CellType> loadedGrid = JsonConvert.DeserializeObject<Dictionary<Vector2Int, CellType>>(json);

                Debug.Log($"Карта успешно загружена из файла: {path}");
                return;// loadedGrid;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Ошибка при чтении или парсинге файла: {e.Message}");
                return;// null;
            }
        }
    }
}
