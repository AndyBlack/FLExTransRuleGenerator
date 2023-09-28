// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using NUnit.Framework;
using SIL.FLExTransRuleGen.Model;
using SIL.FLExTransRuleGen.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.FLExTransRuleGenServiceTests
{
    public class WebPageProducerTests : ServiceTestBase
    {
        WebPageProducer producer;
        string expectedResultFile = "";
        string actualWebPageOutput = "";
        FLExTransRuleGenerator ruleGenerator;
        FLExTransRule rule;

        [SetUp]
        override public void Setup()
        {
            base.Setup();
            producer = WebPageProducer.Instance;
        }

        [Test]
        public void ProduceFromEx1aDefNounTest()
        {
            const string kFileName = "Ex1a_Def-Noun";
            RuleGenExpected = Path.Combine(TestDataDir, kFileName + ".xml");
            provider.LoadDataFromFile(RuleGenExpected);
            ruleGenerator = provider.RuleGenerator;
            rule = ruleGenerator.FLExTransRules[0];
            expectedResultFile = Path.Combine(TestDataDir, kFileName + ".htm");
            CheckResult();
        }

        [Test]
        public void ProduceFromEx4bIndefAdjNounTest()
        {
            const string kFileName = "Ex4b_Indef-Adj-Noun";
            RuleGenExpected = Path.Combine(TestDataDir, kFileName + ".xml");
            provider.LoadDataFromFile(RuleGenExpected);
            ruleGenerator = provider.RuleGenerator;
            rule = ruleGenerator.FLExTransRules[0];
            expectedResultFile = Path.Combine(TestDataDir, kFileName + ".htm");
            CheckResult();
        }

        private void CheckResult()
        {
            actualWebPageOutput = producer.ProduceWebPage(rule);
            Console.WriteLine("actual=\n" + actualWebPageOutput);
            using (var streamReader = new StreamReader(expectedResultFile, Encoding.UTF8))
            {
                string expectedWebPageOutput = streamReader.ReadToEnd().Replace("\r", "");
                Assert.AreEqual(expectedWebPageOutput, actualWebPageOutput);
            }
        }
    }
}
