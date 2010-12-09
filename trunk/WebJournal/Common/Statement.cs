using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebJournal.Common
{
    public class Statement: NativeObject
    {
        public virtual HtmlTag Tag { get; set; }
        public virtual string Id { get; set; }
        public virtual Dictionary<string, string> Attributes { get; set; }
        public virtual HtmlAction Action { get; set; }
        public virtual string Value { get; set; }

        protected Dictionary<string, string> TagMap = CSVUtil.ReadCSVByDic("csv\\tag.csv");
        protected Dictionary<string, string> ActionMap = CSVUtil.ReadCSVByDic("csv\\action.csv");
        protected Dictionary<string,string> ScriptFormatMap = CSVUtil.ReadCSVByDic("csv\\scriptformat.csv");
        
        public static Statement Parse(string script)
        {
            throw new NotImplementedException();
        }

        public override string ToCustomScript(string type)
        {
            switch (type)
            {
                case "webaii":
                    return ToWebAiiScript();
                default:
                    throw new NotImplementedException();

            }
            
        }

        protected virtual string ToWebAiiScript()
        {
            throw new NotImplementedException();
        }

        public override string ToNativeScript()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

    }
}
