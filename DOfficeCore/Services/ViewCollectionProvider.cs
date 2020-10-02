using DOfficeCore.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace DOfficeCore.Services
{
    /// <summary>Класс для обеспечения взаимодействия между ViewCollection и коллекцией диагнозов</summary>
    class ViewCollectionProvider : IViewCollectionProvider
    {

        public ViewCollection DiagnosisFromDataToView (ViewCollection viewCollection)
        {
            if (viewCollection == null) return null;

            viewCollection.DiagnosisCode = new ObservableCollection<string>(viewCollection.DataCollection.
                Select(t => t.Code));
            return viewCollection;
        }

        public ViewCollection BlocksFromDataToView (ViewCollection viewCollection)
        {
            if (viewCollection == null ||
                viewCollection.CurrentDiagnosis == null) return null;

            viewCollection.BlocksNames = null;

            var tempDiagnosis = viewCollection.DataCollection.
                Single(t => t.Code.Equals(viewCollection.CurrentDiagnosis));

            var tempBlockNames = tempDiagnosis.Blocks.Select(t => t.Name);

            viewCollection.BlocksNames = new ObservableCollection<string>(tempBlockNames);

            return viewCollection;
        }

        public ViewCollection LinesFromDataToView (ViewCollection viewCollection)
        {
            if (viewCollection == null ||
                viewCollection.BlocksNames == null ||
                viewCollection.CurrentDiagnosis == null ||
                viewCollection.CurrentBlock == null) return null;

            viewCollection.LinesNames = null;

            var tempDiagnosis = viewCollection.DataCollection.
                Single(t => t.Code.Equals(viewCollection.CurrentDiagnosis));

            var tempBlock = tempDiagnosis.Blocks.Single(t => t.Name.Equals(viewCollection.CurrentBlock));

            viewCollection.LinesNames = new ObservableCollection<string>(tempBlock.Lines);

            return viewCollection;
        }

    }
}
