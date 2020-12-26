using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DOfficeCore.Services
{
    interface ILineEditorService
    {
        /// <summary>
        /// Получение из файла формата docx текста
        /// </summary>
        /// <param name="filepath">Путь к файлу</param>
        /// <returns>Содержимое файла в виде текста</returns>
        string OpenDocument(string filepath);
        /// <summary> Асинхронное получение текста из файла формата docx</summary>
        /// <param name="filepath">Путь к файлу</param>
        /// <param name="token">Токен отмены операции</param>
        /// <returns>Содержимое файла в виде текста</returns>
        Task<string> OpenDocumentAsync(string filepath, CancellationToken token = default);
        /// <summary>
        /// Преобразование текста в список предложений с удалением всех дат
        /// </summary>
        /// <param name="lines">Текст для преобразования</param>
        /// <returns>Список предложений, полученных из текста</returns>
        List<string> TextToLines(string lines);
        /// <summary>
        /// Асинхронное преобразование текста в список предложений с удалением всех дат
        /// </summary>
        /// <param name="lines">Текст для преобразования</param>
        /// <param name="token">Токен отмены</param>
        /// <returns>Список предложений, полученных из текста</returns>
        Task<List<string>> TextToLinesAsync(string lines, CancellationToken token = default);
    }
}