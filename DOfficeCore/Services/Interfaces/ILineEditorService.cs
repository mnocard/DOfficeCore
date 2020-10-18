using System.Collections.Generic;

namespace DOfficeCore.Services
{
    interface ILineEditorService
    {
        string OpenDocument(string filepath);
        List<string> TextToLines(string lines);
    }
}