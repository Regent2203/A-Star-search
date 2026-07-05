using SFB;
using System.IO;
using System;
using UnityEngine;

namespace ThisProject.Savers.FilePathProviders
{
    public class DialogueFilePathProvider : IFilePathProvider
    {
        private readonly ExtensionFilter[] _extensions = new[]
        {
            new ExtensionFilter("JSON Files", "json"),
            new ExtensionFilter("All Files", "*")
        };

        public string GetSaveFilePath()
        {
            string folderPath = Application.dataPath;//AppDomain.CurrentDomain.BaseDirectory;
            //string filePath = Path.Combine(folderPath, "save.json");

            // Открываем окно "Сохранить как..."
            // Параметры: (Заголовок окна, Стартовая папка, Имя файла по умолчанию, Фильтр расширений)
            string path = StandaloneFileBrowser.SaveFilePanel("Сохранить сетку А*", folderPath, "grid_save", _extensions);

            // Если пользователь закрыл окно или нажал "Отмена"
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("Сохранение отменено пользователем.");
                return null;
            }

            return path;
        }

        public string GetLoadFilePath()
        {
            // Открываем окно выбора файлов
            // Параметры: (Заголовок, Стартовая папка, Фильтр, Разрешить выбор нескольких файлов: false)
            string[] paths = StandaloneFileBrowser.OpenFilePanel("Открыть файл сетки А*", Application.dataPath, _extensions, false);

            // Если массив пустой или пользователь нажал "Отмена"
            if (paths == null || paths.Length == 0 || string.IsNullOrEmpty(paths[0]))
            {
                Debug.Log("Загрузка отменена пользователем.");
                return null; // Возвращаем null, чтобы вызывающий метод понял, что загрузка отменена
            }

            return paths[0];
        }
    }
}
