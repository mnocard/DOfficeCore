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

        public ViewCollection BlocksFromDataToView (ViewCollection viewCollection, string code)
        {
            if (viewCollection == null ||
                code == null) return null;

            viewCollection.BlocksNames = null;

            var tempDiagnosis = viewCollection.DataCollection.
                Single(t => t.Code.Equals(code));

            var tempBlockNames = tempDiagnosis.Blocks.Select(t => t.Name);

            viewCollection.BlocksNames = new ObservableCollection<string>(tempBlockNames);

            return viewCollection;
        }

        public ViewCollection LinesFromDataToView (ViewCollection viewCollection, string code, string block)
        {
            if (viewCollection == null || 
                viewCollection.BlocksNames == null ||
                code == null ||
                block == null) return null;

            viewCollection.LinesNames = null;

            var tempDiagnosis = viewCollection.DataCollection.
                Single(t => t.Code.Equals(code));

            var tempBlock = tempDiagnosis.Blocks.Single(t => t.Name.Equals(block));

            viewCollection.LinesNames = new ObservableCollection<string>(tempBlock.Lines);

            return viewCollection;
        }
    }
}
