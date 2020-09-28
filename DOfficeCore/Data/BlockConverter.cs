using DOfficeCore.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace DOfficeCore.Data
{
    class BlockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string BlockNames = String.Empty;
            int index = 0;
            var result = (value as DataGrid);
            var col = result.Items;
            ObservableCollection<Diagnosis> colDiag = new ObservableCollection<Diagnosis>();
            foreach (Diagnosis item in col)
            {
                colDiag.Add(item);
            }
            if (result.SelectedItem != null)
            {
                index = result.SelectedIndex;
                if (index == -1) return BlockNames;
            }
            return BlockNames;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
