using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace Recorder.Core.Find
{
    public class Finder
    {
        internal const string altAttribute = "alt";
        internal const string idAttribute = "id";
        internal const string forAttribute = "htmlFor";
        internal const string nameAttribute = "name";
        internal const string srcAttribute = "src";
        internal const string styleBaseAttribute = "style";
        internal const string innerTextAttribute = "innertext";
        internal const string titleAttribute = "title";
        internal const string tagNameAttribute = "tagName";
        internal const string valueAttribute = "value";
        internal const string hrefAttribute = "href";
        internal const string classNameAttribute = "className";

        static IList<string> attris;

        static Finder()
        {
            attris = new List<string>();
            attris.Add(altAttribute);
            attris.Add(forAttribute);
            //attris.Add(nameAttribute);
            attris.Add(srcAttribute);
            //attris.Add(styleBaseAttribute);
            attris.Add(titleAttribute);
            attris.Add(innerTextAttribute);
            attris.Add(tagNameAttribute);
            attris.Add(valueAttribute);
            attris.Add(hrefAttribute);
            attris.Add(classNameAttribute);
        }

        public static string GetID(IHTMLElement ele)
        {
            if (ele.id != null && !(ele.id.Equals(string.Empty)))
                return idAttribute + " : " + ele.id;
            else
                return string.Empty;
        }

        public static string GetAttribute(IHTMLElement ele, string attributeName)
        {
            object attri_obj = ele.getAttribute(attributeName, 0);
            if (attri_obj != null)
            {
                string attri = attri_obj.ToString();
                if (attri != null && !(attri.Equals(string.Empty)))
                    return attributeName + " : " + attri;
                else
                    return string.Empty;
            }
            else
                return string.Empty;
            
        }

        public static string GetAttributes(IHTMLElement ele)
        {

            string attrs = string.Empty;

            try
            {
                foreach (string tmp in attris)
                {
                    string ret = GetAttribute(ele, tmp);

                    if (ret != null && !(ret.Equals(string.Empty)))
                    {
                        attrs += tmp + " : " + ret + ";";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return attrs;

        }
    }
}
