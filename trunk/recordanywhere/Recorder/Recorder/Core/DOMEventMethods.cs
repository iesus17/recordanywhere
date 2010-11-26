using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using mshtml;
using Recorder.Core.Find;
using System.IO;

namespace Recorder.Core
{
    public class DOMEventMethods
    {
        public static StringBuilder sb = new StringBuilder();

        public static IHTMLElement CloneInputElement;

        ///Define the original source format
        ///Finder:Way:Value;Action:ActionType:Value

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public static void _click(IHTMLEventObj obj)
        {

            string identify = GetIdentify(obj.srcElement);
            identify += "action : click" + System.Environment.NewLine;
            sb.Append(identify);
            obj.srcElement.click();

        }

        public static void _link_click(IHTMLEventObj obj)
        {
            string identify = GetIdentify(obj.srcElement);
            identify += "action : click" + System.Environment.NewLine;
            obj.srcElement.click();
        }

        public static void _keypress(IHTMLEventObj obj)
        {
            Keys currentKey = (Keys)Enum.Parse(typeof(Keys),obj.keyCode.ToString());

            IHTMLElement current = ((DispHTMLDocument)obj.srcElement.document).activeElement;

            if (CloneInputElement != null)
            {
                if (current == CloneInputElement) //same input
                {
                    sb.Append(current.getAttribute(Finder.valueAttribute, 0));
                }
                else
                {
                    CloneInputElement = (IHTMLElement)((IHTMLDOMNode)current).cloneNode(true);
                    sb.Length = 0;
                    sb.Append(current.getAttribute(Finder.valueAttribute, 0));
                }
            }
            else
            {
                CloneInputElement = (IHTMLElement)((IHTMLDOMNode)current).cloneNode(true);
                sb.Length = 0;
                sb.Append(current.getAttribute(Finder.valueAttribute, 0));
            }

            //switch (currentKey)
            //{
            //    case Keys.Enter:
            //        break;
            //    default:
            //        break;

            //}
            //string identify = GetIdentify(obj.srcElement);

            //identify += "action : keyinput ; value :"+obj.keyCode ;
            
        }


        public static void WriteKeys(char key)
        {
            
        }

        public static string GetIdentify(IHTMLElement obj)
        {
            string identify = Finder.GetID(obj);
            if (identify == null || identify.Equals(string.Empty))
            {
                identify = Finder.GetAttributes(obj);
            }
            return identify;
        }

        public static void AddToGrid()
        {
           

        }
    }
}
