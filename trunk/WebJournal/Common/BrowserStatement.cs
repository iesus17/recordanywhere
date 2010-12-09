using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebJournal.Common
{
    public class BrowserStatement:Statement
    {
        public override HtmlTag Tag
        {
            get { return HtmlTag.Window; }
        }
    }
}
