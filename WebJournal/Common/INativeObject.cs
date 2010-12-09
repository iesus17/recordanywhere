using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebJournal.Common
{
    public abstract class NativeObject
    {
        /// <summary>
        /// Load mapping information from csv file and convert the object to custom script statements
        /// </summary>
        /// <param name="type">the script type</param>
        /// <returns>Webaii script</returns>
        public abstract string ToCustomScript(string type);

        /// <summary>
        /// Convert the object to a statement in native format
        /// </summary>
        /// <returns>native statement</returns>
        public abstract string ToNativeScript();

        /// <summary>
        /// Convert the object to a easy-to-understand string to display
        /// </summary>
        /// <returns>the converted string</returns>
        public abstract override string ToString();
    }
}
