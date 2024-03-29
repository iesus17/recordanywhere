﻿using System;
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
            TestCase testCase = new TestCase { Statements = new List<Statement>() };
            string[] statementStrings = script.Split('\n');
            foreach (string s in statementStrings)
            {
                testCase.Statements.Add(Statement.Parse(s));
            }
            return testCase;
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
            StringBuilder script = new StringBuilder();

            script.Append(
@"using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestTemplates;

namespace WebJournal.AutoGeneratedTest
{
    [TestFixture]
    public class WebJournalTestClass
    {
        [TestFixtureSetUp]
        public void TestInitialize()
        {
            Initialize();
            Manager.Settings.AnnotateExecution = true;
            Manager.Settings.ExecutionDelay = 2;

            Manager.LaunchNewBrowser();
            Manager.ActiveBrowser.AutoDomRefresh = true;
            Manager.ActiveBrowser.AutoWaitUntilReady = true;
            //ActiveBrowser.Window.Maximize();
        }

        [TestFixtureTearDown]
        public void TestCleanUp()
        {
            // Shuts down WebAii manager and closes all browsers currently running
            this.CleanUp();
        }

        [Test]
        public void TestMethod1()
        {");

            foreach (Statement state in this.Statements)
            {
                script.Append(state.ToCustomScript("webaii"));
            }

            script.Append(
@"
        }
    }
}");

            return script.ToString();
        }

        public override string ToNativeScript()
        {
            return string.Join("\n", Statements.Select(s => s.ToNativeScript()).ToArray());
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
