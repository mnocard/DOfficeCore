using DOfficeCore.Infrastructure.Commands.Core;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Controls;

namespace DOfficeCore.Infrastructure.Commands
{
    class ClearDiaryBoxCommand : CommandCore
    {
        protected override void Execute(object p)
        {
            var DiaryBox = p as TextBox;
            if (DiaryBox == null) return;
            DiaryBox.Text = String.Empty;
        }
    }
}
