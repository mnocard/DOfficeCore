using DOfficeCore.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace DOfficeCore.Services
{
    /// <summary>Класс для обеспечения взаимодействия между ViewCollection и коллекцией диагнозов</summary>
    class ViewCollectionProvider : IViewCollectionProvider
    {

        public void DiagnosisFromDataToView (ViewCollection viewCollection)
        {
            if (viewCollection == null) return;

            viewCollection.DiagnosisCode = new ObservableCollection<string>(viewCollection.DataCollection.
                Select(t => t.Code));
        }

        public void BlocksFromDataToView (ViewCollection viewCollection)
        {
            if (viewCollection == null ||
                viewCollection.CurrentDiagnosis == null) return;

            viewCollection.BlocksNames = null;

            var tempDiagnosis = viewCollection.DataCollection.
                Single(t => t.Code.Equals(viewCollection.CurrentDiagnosis));

            var tempBlockNames = tempDiagnosis.Blocks.Select(t => t.Name);

            viewCollection.BlocksNames = new ObservableCollection<string>(tempBlockNames);
        }

        public void LinesFromDataToView (ViewCollection viewCollection)
        {
            if (viewCollection == null ||
                viewCollection.BlocksNames == null ||
                viewCollection.CurrentDiagnosis == null ||
                viewCollection.CurrentBlock == null) return;

            viewCollection.LinesNames = null;

            var tempDiagnosis = viewCollection.DataCollection.
                Single(t => t.Code.Equals(viewCollection.CurrentDiagnosis));

            var tempBlock = tempDiagnosis.Blocks.Single(t => t.Name.Equals(viewCollection.CurrentBlock));

            viewCollection.LinesNames = new ObservableCollection<string>(tempBlock.Lines);
        }


    }
}
