using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
            Statement s = new Statement();
            string regexString = "{(?<Tag>.*?)}:{(?<Id>.*?)}:{(?<Attributes>.*?)}:{(?<Action>.*?)}:{(?<Value>.*?)}";
            MatchCollection mc = Regex.Matches(script, regexString);
            if (mc.Count > 0)
            {
                s.Tag = (HtmlTag)Enum.Parse(typeof(HtmlTag), mc[0].Groups["Tag"].Value);
                s.Id = mc[0].Groups["Id"].Value;
                s.Action = (HtmlAction)Enum.Parse(typeof(HtmlAction), mc[0].Groups["Action"].Value);
                s.Value = mc[0].Groups["Value"].Value;

                string attributeRegexString = "(?<Key>\\S*?)=\"(?<Value>.*?)\"";
                MatchCollection mcAttribute = Regex.Matches(mc[0].Groups["Attributes"].Value, attributeRegexString);
                s.Attributes = new Dictionary<string, string>(mcAttribute.Count);
                foreach (Match match in mcAttribute)
                {
                    string key = match.Groups["Key"].Value;
                    string value = match.Groups["Value"].Value;
                    s.Attributes.Add(key, value);
                }
            }
            return s;
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
            List<string> attributeStrings = new List<string>(Attributes.Count);
            foreach (KeyValuePair<string, string> pair in Attributes)
            {
                attributeStrings.Add(string.Format("{0}=\"{1}\"", pair.Key, pair.Value));
            }
            return string.Format("{{{0}}}:{{{1}}}:{{{2}}}:{{{3}}}:{{{4}}}", Tag, Id, string.Join(" ", attributeStrings.ToArray()), Action, Value);
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

    }
}
