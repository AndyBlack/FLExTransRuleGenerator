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
        Word sourceWord;
        Word sourceWord2;

        [SetUp]
        public void Setup()
        {
            ruleGenerator = new FLExTransRuleGenerator();
            FLExTransRule rule = new FLExTransRule();
            rule.Name = "Rule 1";
            ruleGenerator.FLExTransRules.Add(rule);
            Source source = rule.Source;
            Phrase sourcePhrase = source.Phrase;
            sourceWord = new Word();
            sourceWord.Id = "Source 1";
            sourceWord.Category = "Noun";
            sourceWord.CategoryConstituent = new Category(sourceWord.Category);
            sourceWord.Head = HeadValue.yes;
            sourcePhrase.Words.Add(sourceWord);
            sourceWord2 = new Word();
            sourceWord2.Id = "Source 2";
            sourceWord2.Category = "Det";
            sourceWord2.CategoryConstituent = new Category(sourceWord2.Category);
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
        public void RuleDuplicateTest()
        {
            Assert.AreEqual(1, ruleGenerator.FLExTransRules.Count);
            FLExTransRule rule = ruleGenerator.FLExTransRules.ElementAt(0);
            FLExTransRule rule2 = rule.Duplicate();
            Assert.AreEqual(rule.Name, rule2.Name);
            Assert.AreEqual(rule.Source.Phrase.Words.Count, rule2.Source.Phrase.Words.Count);
            Assert.AreEqual(rule.Target.Phrase.Words.Count, rule2.Target.Phrase.Words.Count);
        }

        [Test]
        public void WordDuplicateTest()
        {
            Word newWord = sourceWord.Duplicate();
            Assert.AreEqual("Source 1", sourceWord.Id);
            Assert.AreEqual("Source 1", newWord.Id);
            Assert.AreEqual("Noun", sourceWord.Category);
            Assert.AreEqual("Noun", newWord.Category);
            Category sourceCat = sourceWord.CategoryConstituent;
            Category newCat = newWord.CategoryConstituent;
            Assert.AreEqual("Noun", sourceCat.Name);
            Assert.AreEqual("Noun", newCat.Name);
            Assert.AreEqual(0, sourceWord.Affixes.Count);
            Assert.AreEqual(0, newWord.Affixes.Count);
            Assert.AreEqual(0, sourceWord.Features.Count);
            Assert.AreEqual(0, newWord.Features.Count);
        }
    }
}
