// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using NUnit.Framework;
using SIL.FLExTransRuleGen.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIL.FLExTransRuleGenServiceTests
{
    abstract public class ServiceTestBase
    {
        protected XmlBackEndProvider provider = new XmlBackEndProvider();
        protected string TestDataDir { get; set; }
        protected string RuleGenFile { get; set; }
        protected string RuleGenExpected { get; set; }
        protected string ExpectedFileName = "RuleGenExpected.xml";
        protected const string kTestDir = "RuleGeneratorServiceTests";

        [SetUp]
        virtual public void Setup()
        {
            Uri uriBase = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var rootdir = Path.GetDirectoryName(Uri.UnescapeDataString(uriBase.AbsolutePath));
            int i = rootdir.LastIndexOf(kTestDir);
            string basedir = rootdir.Substring(0, i);
            TestDataDir = Path.Combine(basedir, kTestDir, "TestData");
            RuleGenExpected = Path.Combine(TestDataDir, ExpectedFileName);
        }
    }
}
