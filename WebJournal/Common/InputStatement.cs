using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebJournal.Common
{
    class InputStatement: Statement
    {


        

        protected override string ToWebAiiScript()
        {
            

            StringBuilder script = new StringBuilder();

            script.Append(Environment.NewLine);

            script.Append("\t\t\t");

            string tag = this.Tag.ToString();
            string type = TagMap[tag];


            string controlName = tag.ToLowerInvariant() + (Utility.ControlNameSeed++).ToString();

            script.Append(type).Append(" ").Append(controlName).Append("= ");

            string param = string.Empty;
            if (this.Attributes != null)
            {
                foreach (KeyValuePair<string, string> pair in this.Attributes)
                {
                    if (param.Length > 0)
                        param += ",";
                    param += pair.Key + "=" + pair.Value;
                }
            }

            script.Append(string.Format(ScriptFormatMap["WebaiiFind"], type, param));

            script.Append(";");

            script.Append(Environment.NewLine);

            script.Append("\t\t\t");

            script.Append(string.Format(ScriptFormatMap["WebaiiAction"], ActionMap[this.Action.ToString()], controlName));

            script.Append(";");

            return script.ToString();
        }

    }
}
