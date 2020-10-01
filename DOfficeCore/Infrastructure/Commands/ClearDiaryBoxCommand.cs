using DOfficeCore.Infrastructure.Commands.Core;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Controls;

namespace DOfficeCore.Infrastructure.Commands
{
    /// <summary>Команда очистки поля дневника</summary>
    class ClearDiaryBoxCommand : CommandCore
    {
        protected override void Execute(object p)
        {
            if (!(p is TextBox DiaryBox)) return;
            DiaryBox.Text = String.Empty;
            DiaryBox.IsReadOnly = true;
        }
    }
}
