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
        
        
        protected override string ToWebAiiScript()        
        {            
            StringBuilder script = new StringBuilder();

            script.Append(Environment.NewLine);

            script.Append("\t\t\t");

            script.Append(TagMap[this.Tag.ToString()]).Append(".");

            script.Append(string.Format(ActionMap[this.Action.ToString()], this.Value));
                
            script.Append(";");

            return script.ToString();       
        }   
        
    }
}
