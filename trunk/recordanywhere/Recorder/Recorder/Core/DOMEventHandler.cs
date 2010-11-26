using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using System.Runtime.InteropServices;

namespace Recorder.Core
{
    ///

    /// Generic HTML DOM Event method handler.

    ///
    public delegate void DOMEvent(IHTMLEventObj e);

    [ComVisible(true)]
    public class DOMEventHandler
    {
        public object OriginalEventHandlers;

        DispHTMLDocument Document;

        public DOMEvent NewEventHandlers;

        public DOMEventHandler(DispHTMLDocument doc, object existingHandlers)
        {
            this.Document = doc;
            OriginalEventHandlers = existingHandlers;
        }

        [DispId(0)]
        public void Call()
        {
            HTMLWindow2 win = (HTMLWindow2)Document.parentWindow;
            string eventtype = win.@event.type;

            NewEventHandlers(Document.parentWindow.@event);

            //IHTMLElement activeElement;
            //switch (eventtype)
            //{
            //    case "click":
            //        NewEventHandlers(Document.parentWindow.@event);
            
            //        activeElement = Document.activeElement;
                    
            //        activeElement.click();
            //        break;
            //    case "mouseup":
            //        NewEventHandlers(Document.parentWindow.@event);
            //        //OriginalEventHandlers.GetType().InvokeMember("[DispID=0]", System.Reflection.BindingFlags.InvokeMethod, null, OriginalEventHandlers, null);
            //        break;
            //    case "keypress":
            //        NewEventHandlers(Document.parentWindow.@event);
            //        break;

            //    default:
            //        NewEventHandlers(Document.parentWindow.@event);
            //        break;

            //}

            
            
        }
    }
}
