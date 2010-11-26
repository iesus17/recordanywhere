using System;
using System.Collections.Generic;
using System.Text;

namespace Recorder.Core.Actions
{
    class BrowserActionEnum
    {
        public static string Nav = "Navigate : ";
    }

    class DOMActionEnum
    {
        public static string Click = "Click : ";
        public static string Key = "KeyInput : ";
        public static string Value = "Value : ";
    }

    //format : Action + Target + Value
    // Navigate : Browser; ID : XXX;Attri: XXX; Value : XXX
}
