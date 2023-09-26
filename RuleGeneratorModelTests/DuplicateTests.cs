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
            ruleGenerator.FLExTransRules.Add(rule);
            Source source = rule.Source;
            Phrase sourcePhrase = source.Phrase;
            Word sourceWord = new Word();
            sourceWord.Id = "Source 1";
            sourceWord.Category = "Noun";
            sourceWord.Head = HeadValue.yes;
            sourcePhrase.Words.Add(sourceWord);
            Word sourceWord2 = new Word();
            sourceWord2.Id = "Source 2";
            sourceWord2.Category = "Det";
            sourceWord2.Head = HeadValue.no;
            sourcePhrase.Words.Add(sourceWord2);
            Target target = rule.Target;
            Phrase targetPhrase = target.Phrase;
            Word targetWord = new Word();
            targetWord.Id = "Target 1";
            targetWord.Category = "Det";
            targetWord.Head = HeadValue.no;
            targetPhrase.Words.Add(targetWord);
            Word targetWord2 = new Word();
            targetWord2.Id = "Target 2";
            targetWord2.Category = "Noun";
            targetWord2.Head = HeadValue.yes;
            targetPhrase.Words.Add(targetWord2);
        }

        [Test]
        public void DuplicateTest()
        {
            Assert.AreEqual(1, ruleGenerator.FLExTransRules.Count);
            FLExTransRule rule = ruleGenerator.FLExTransRules.ElementAt(0);
            FLExTransRule rule2 = rule.Duplicate();
            Assert.AreEqual(rule.Name, rule2.Name);
            Assert.AreEqual(rule.Source.Phrase.Words.Count, rule2.Source.Phrase.Words.Count);
            Assert.AreEqual(rule.Target.Phrase.Words.Count, rule2.Target.Phrase.Words.Count);
        }
    }
}
