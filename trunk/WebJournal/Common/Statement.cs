﻿using System;
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

        public static Statement Parse(string script)
        {
            throw new NotImplementedException();
        }

        public override string ToCustomScript(string type)
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
