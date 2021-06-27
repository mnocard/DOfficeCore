using System.Collections.Generic;
using System.Collections.ObjectModel;

using DOfficeCore.Models;

namespace DOfficeCore.Services.Interfaces
{
    /// <summary>Сервис работы с текстовым полем дневника</summary>
    interface IDiaryBoxProvider
    {
        /// <summary>
        /// Перенос строки в дневник с удалением строки, если она уже присутствует в дненвике
        /// </summary>
        /// <param name="DiaryBox">Дневник, в который переносится строка</param>
        /// <param name="Line">Переносимая строка</param>
        /// <returns>Обновлённый дневник</returns>
        string LineToDiaryBox(string DiaryBox, string Line);
        /// <summary>
        /// Создание случайного дневника из списка разделов
        /// </summary>
        /// <param name="Blocks">Список разделов, из которого будет создаваться случайный дневник</param>
        /// <param name="ChanceMidofier">Модификатор шанса попадания одной из строк выбранного раздела 
        /// в дневник, в зависимости от количества строк в этом разделе. По умолчанию равен 20</param>
        /// <returns>Diary - Готовый дневник. Lines - строки, из которых создан дневник</returns>
        (string Diary, ObservableCollection<string> Lines) RandomDiaryWithNewModel(IEnumerable<Block> Blocks, int ChanceMidofier = 20);
    }
}
