﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebJournal.Common
{
    public class Statement: IScript
    {
        public HtmlTag Tag { get; set; }
        public string Id { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public HtmlAction Action { get; set; }
        public string Value { get; set; }

        public static Statement Parse(string script)
        {
            throw new NotImplementedException();
        }

        public string ToCustomScript(string type)
        {
            throw new NotImplementedException();
        }

        public string ToNativeScript()
        {
            throw new NotImplementedException();
        }

        public string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
