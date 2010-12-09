using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebJournal.Common
{
    public class Statement
    {
        public HtmlTag Tag { get; set; }
        public string Id { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public HtmlAction Action { get; set; }
        public string Value { get; set; }

        /// <summary>
        /// Parse the WebJournal statement to Statement object
        /// </summary>
        /// <param name="script">WebJournal statement</param>
        /// <returns>Statement object</returns>
	    public static Statement Parse(string script)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Load mapping information from csv file and convert the object to custom script statements
        /// </summary>
        /// <param name="type">the script type</param>
        /// <returns>Webaii script</returns>
        public string ToCustomScript(string type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Convert the object to a statement in native format
        /// </summary>
        /// <returns>native statement</returns>
        public string ToNativeScript()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Convert the object to a easy-to-understand string to display
        /// </summary>
        /// <returns>the converted string</returns>
        public string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
