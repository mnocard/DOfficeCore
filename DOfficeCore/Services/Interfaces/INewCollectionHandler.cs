using System.Collections.Generic;

using DOfficeCore.Models;

namespace DOfficeCore.Services.Interfaces
{
    /// <summary>Сервис работы с коллекцией данных</summary>
    interface INewCollectionHandler
    {
        /// <summary>
        /// Добавление нового сектора в коллекцию данных
        /// </summary>
        /// <param name="SectorsCollection">Коллекция данных, в которой происходит операция</param>
        /// <param name="SectorName">Название сектора</param>
        /// <returns>True если операция прошла успешно</returns>
        bool AddSector(List<Sector> SectorsCollection, string SectorName);
        /// <summary>
        /// Добавление нового блока в коллекцию данных
        /// </summary>
        /// <param name="SectorsCollection">Коллекция данных, в которой происходит операция</param>
        /// <param name="Sector">Сектор, в который добавляется блок</param>
        /// <param name="BlockName">Название добавляемого блока</param>
        /// <returns>True если операция прошла успешно</returns>
        bool AddBlock(List<Sector> SectorsCollection, Sector Sector, string BlockName);
        /// <summary>
        /// Добавление новой строки в коллекцию данных
        /// </summary>
        /// <param name="SectorsCollection">Коллекция данных, в которой происходит операция</param>
        /// <param name="Block">Блок, в который добавляется строка</param>
        /// <param name="LineName">Название добавляемой строки</param>
        /// <returns>True если операция прошла успешно</returns>
        bool AddLine(List<Sector> SectorsCollection, Block Block, string LineName);

        /// <summary>
        /// Удаление сектора из коллекции данных
        /// </summary>
        /// <param name="SectorsCollection">Коллекция данных, в которой происходит операция</param>
        /// <param name="Sector">Удаляемый сектор</param>
        /// <returns>True если операция прошла успешно</returns>
        bool RemoveSector(List<Sector> SectorsCollection, Sector Sector);
        /// <summary>
        /// Удаление блока из коллекции данных
        /// </summary>
        /// <param name="SectorsCollection">Коллекция данных, в которой происходит операция</param>
        /// <param name="Sector">Сектор, из которого происходит удаление</param>
        /// <param name="Block">Удаляемый блок</param>
        /// <returns>True если операция прошла успешно</returns>
        bool RemoveBlock(List<Sector> SectorsCollection, Sector Sector, Block Block);
        /// <summary>
        /// Удаление строки из коллекции данных
        /// </summary>
        /// <param name="SectorsCollection">Коллекция данных, в которой происходит операция</param>
        /// <param name="Block">Блок, из которого происходит удаление</param>
        /// <param name="LineName">Удаляемая строка</param>
        /// <returns>True если операция прошла успешно</returns>
        bool RemoveLine(List<Sector> SectorsCollection, Block Block, string LineName);

        /// <summary>
        /// Переименование сектора
        /// </summary>
        /// <param name="SectorsCollection">Коллекция данных, в которой происходит операция</param>
        /// <param name="Sector">Целевой сектор</param>
        /// <param name="SectorName">Новое название сектора</param>
        /// <returns>True если операция прошла успешно</returns>
        bool EditSector(List<Sector> SectorsCollection, Sector Sector, string SectorName);
        /// <summary>
        /// Переименование блока
        /// </summary>
        /// <param name="SectorsCollection">Коллекция данных, в которой происходит операция</param>
        /// <param name="Block">Целевой блок</param>
        /// <param name="BlockName">Новое название блока</param>
        /// <returns></returns>
        bool EditBlock(List<Sector> SectorsCollection, Block Block, string BlockName);
        /// <summary>
        /// Изменение строки
        /// </summary>
        /// <param name="SectorsCollection">Коллекция данных, в которой происходит операция</param>
        /// <param name="Block">Блок, содержащий целевую строку</param>
        /// <param name="OldLine">Старая строка</param>
        /// <param name="NewLine">Новая строка</param>
        /// <returns></returns>
        bool EditLine(List<Sector> SectorsCollection, Block Block, string OldLine, string NewLine);
    }
}
