using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Recorder.Core.Convert
{
    public interface IScriptConvert
    {
        void ConvertTo(string ScriptType, TreeView view);
    }
}
