using System.Collections.Generic;

using DOfficeCore.Models;

namespace DOfficeCore.Services.Interfaces
{
    /// <summary>Провайдер поиска и передачи элементов из коллекции данных</summary>
    interface INewViewCollectionProvider
    {
        /// <summary>
        /// Поиск секторов
        /// </summary>
        /// <param name="SectorsList">Коллекция данных, в которой осуществляется поиск</param>
        /// <param name="SectorName">Строка, по которой осуществляется поиск</param>
        /// <returns>Список секторов, содержащих в названии строку для поиска</returns>
        List<Sector> SearchSectors(IEnumerable<Sector> SectorsList, string SectorName);
        /// <summary>
        /// Поиск блоков
        /// </summary>
        /// <param name="SectorsList">Коллекция данных, в которой осуществляется поиск</param>
        /// <param name="BlockName">Строка, по которой осуществляется поиск</param>
        /// <returns>Список блоков, содержащих в названии строку для поиска</returns>
        List<Block> SearchBlocks(IEnumerable<Sector> SectorsList, string BlockName);
        /// <summary>
        /// Поиск строк
        /// </summary>
        /// <param name="SectorsList">Коллекция данных, в которой осуществляется поиск</param>
        /// <param name="Line">Строка, по которой осуществляется поиск</param>
        /// <returns>Список строк, содержащих искомую строку</returns>
        List<string> SearchLines(IEnumerable<Sector> SectorsList, string Line);
        /// <summary>
        /// Получение всех блоков определенного сектора
        /// </summary>
        /// <param name="SectorsList">Коллекция данных, в которой происходит поиск</param>
        /// <param name="Sector">Сектор, из которого будет получен список блоков</param>
        /// <returns>Список полученных блоков</returns>
        List<Block> GetBlocks(IEnumerable<Sector> SectorsList, Sector Sector);
        /// <summary>
        /// Получение всех строк определенного блока
        /// </summary>
        /// <param name="SectorsList">Коллекция данных, в которой происходит поиск</param>
        /// <param name="Block">Блок, из которого будет получен список строк</param>
        /// <returns>Список полученных строк</returns>
        List<string> GetLines(IEnumerable<Sector> SectorsList, Block Block);
    }
}
