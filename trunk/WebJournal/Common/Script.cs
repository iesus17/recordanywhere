using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebJournal.Common
{
    public class TestCase : IScript
    {
        public List<Statement> Statements { get; set; }

        public static TestCase Parse(string script)
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
    }
}
