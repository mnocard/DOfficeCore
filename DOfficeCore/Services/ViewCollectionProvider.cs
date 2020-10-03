using DOfficeCore.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace DOfficeCore.Services
{
    /// <summary>Класс для обеспечения взаимодействия между ViewCollection и коллекцией диагнозов</summary>
    class ViewCollectionProvider : IViewCollectionProvider
    {
        public ViewCollectionProvider(IViewCollection ViewCollection)
        {
            _ViewCollection = ViewCollection;
        }

        #region Коллекция данных
        private readonly IViewCollection _ViewCollection;
        #endregion

        public void DiagnosisFromDataToView ()
        {
            _ViewCollection.DiagnosisCode = new ObservableCollection<string>(_ViewCollection.DataCollection.
                Select(t => t.Code));
        }

        public void BlocksFromDataToView ()
        {
            if (_ViewCollection.CurrentDiagnosis == null) return;

            _ViewCollection.BlocksNames = null;

            var tempDiagnosis = _ViewCollection.DataCollection.
                Single(t => t.Code.Equals(_ViewCollection.CurrentDiagnosis));

            var tempBlockNames = tempDiagnosis.Blocks.Select(t => t.Name);

            _ViewCollection.BlocksNames = new ObservableCollection<string>(tempBlockNames);
        }

        public void LinesFromDataToView ()
        {
            if (_ViewCollection.BlocksNames == null ||
                _ViewCollection.CurrentDiagnosis == null ||
                _ViewCollection.CurrentBlock == null) return;

            _ViewCollection.LinesNames = null;

            var tempDiagnosis = _ViewCollection.DataCollection.
                Single(t => t.Code.Equals(_ViewCollection.CurrentDiagnosis));

            var tempBlock = tempDiagnosis.Blocks.Single(t => t.Name.Equals(_ViewCollection.CurrentBlock));

            _ViewCollection.LinesNames = new ObservableCollection<string>(tempBlock.Lines);
        }


    }
}
