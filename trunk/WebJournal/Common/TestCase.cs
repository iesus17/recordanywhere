using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebJournal.Common
{
    public class TestCase : NativeObject
    {
        public List<Statement> Statements { get; set; }

        public static TestCase Parse(string script)
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
