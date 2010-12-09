using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebJournal.Common
{
    public interface IScript
    {
        /// <summary>
        /// Parse the WebJournal statement to Statement object
        /// </summary>
        /// <param name="script">WebJournal statement</param>
        /// <returns>Statement object</returns>
        public static Statement Parse(string script);

        /// <summary>
        /// Load mapping information from csv file and convert the object to custom script statements
        /// </summary>
        /// <param name="type">the script type</param>
        /// <returns>Webaii script</returns>
        public string ToCustomScript(string type);

        /// <summary>
        /// Convert the object to a statement in native format
        /// </summary>
        /// <returns>native statement</returns>
        public string ToNativeScript();

        /// <summary>
        /// Convert the object to a easy-to-understand string to display
        /// </summary>
        /// <returns>the converted string</returns>
        public string ToString();
    }
}
