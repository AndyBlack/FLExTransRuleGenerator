// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using NUnit.Framework;
using SIL.FLExTransRuleGen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.FLExTransRuleGenModelTests
{
    public class DuplicateTests
    {
        FLExTransRuleGenerator ruleGenerator;

        [SetUp]
        public void Setup()
        {
            ruleGenerator = new FLExTransRuleGenerator();
            FLExTransRule rule = new FLExTransRule();
            rule.Name = "Rule 1";
            ruleGenerator.Rules.Add(rule);
            Phrase source = rule.Source;
            Word sourceWord = new Word();
            sourceWord.Id = "Source 1";
            sourceWord.Category = "Noun";
            sourceWord.Head = HeadValue.yes;
            source.Words.Add(sourceWord);
            Word sourceWord2 = new Word();
            sourceWord2.Id = "Source 2";
            sourceWord2.Category = "Det";
            sourceWord2.Head = HeadValue.no;
            source.Words.Add(sourceWord2);
            Phrase target = rule.Target;
            Word targetWord = new Word();
            targetWord.Id = "Target 1";
            targetWord.Category = "Det";
            targetWord.Head = HeadValue.no;
            target.Words.Add(targetWord);
            Word targetWord2 = new Word();
            targetWord2.Id = "Target 2";
            targetWord2.Category = "Noun";
            targetWord2.Head = HeadValue.yes;
            target.Words.Add(targetWord2);
        }

        [Test]
        public void DuplicateTest()
        {
            Assert.AreEqual(1, ruleGenerator.Rules.Count);
            FLExTransRule rule = ruleGenerator.Rules.ElementAt(0);
            FLExTransRule rule2 = rule.Duplicate();
            Assert.AreEqual(rule.Name, rule2.Name);
            Assert.AreEqual(rule.Source.Words.Count, rule2.Source.Words.Count);
            Assert.AreEqual(rule.Target.Words.Count, rule2.Target.Words.Count);
        }
    }
}
