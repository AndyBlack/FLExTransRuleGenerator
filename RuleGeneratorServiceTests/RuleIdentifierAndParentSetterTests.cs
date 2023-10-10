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
    public class RuleIdentifierAndParentSetterTests : ServiceTestBase
    {
        RuleIdentifierAndParentSetter idSetter;
        FLExTransRuleGenerator ruleGenerator;
        FLExTransRule rule;
        Phrase phrase;
        Word word;
        Category category;
        Feature feature;
        Affix affix;

        [SetUp]
        override public void Setup()
        {
            base.Setup();
            idSetter = RuleIdentifierAndParentSetter.Instance;
        }

        [Test]
        public void IdentifiersForEx1aDefNounTest()
        {
            const string kFileName = "Ex1a_Def-Noun";
            RuleGenExpected = Path.Combine(TestDataDir, kFileName + ".xml");
            provider.LoadDataFromFile(RuleGenExpected);
            ruleGenerator = provider.RuleGenerator;
            rule = ruleGenerator.FLExTransRules[0];
            idSetter.SetIdentifiersAndParents(rule);
            phrase = rule.Source.Phrase;
            Assert.AreEqual(1, phrase.Identifier);
            Assert.AreEqual(2, phrase.Words[0].Identifier);
            Assert.AreEqual(3, phrase.Words[0].CategoryConstituent.Identifier);
            Assert.AreEqual(4, phrase.Words[1].Identifier);
            Assert.AreEqual(5, phrase.Words[1].CategoryConstituent.Identifier);
            phrase = rule.Target.Phrase;
            Assert.AreEqual(6, phrase.Identifier);
            Assert.AreEqual(7, phrase.Words[0].Identifier);
            Assert.AreEqual(8, phrase.Words[0].CategoryConstituent.Identifier);
            Assert.AreEqual(9, phrase.Words[0].Features[0].Identifier);
            Assert.AreEqual(10, phrase.Words[1].Identifier);
            Assert.AreEqual(11, phrase.Words[1].CategoryConstituent.Identifier);
            Assert.AreEqual(12, phrase.Words[1].Features[0].Identifier);
        }

        [Test]
        public void IdentifiersForEx4bIndefAdjNounTest()
        {
            const string kFileName = "Ex4b_Indef-Adj-Noun";
            RuleGenExpected = Path.Combine(TestDataDir, kFileName + ".xml");
            provider.LoadDataFromFile(RuleGenExpected);
            ruleGenerator = provider.RuleGenerator;
            rule = ruleGenerator.FLExTransRules[0];
            idSetter.SetIdentifiersAndParents(rule);
            phrase = rule.Source.Phrase;
            Assert.AreEqual(1, phrase.Identifier);
            Assert.AreEqual(2, phrase.Words[0].Identifier);
            Assert.AreEqual(3, phrase.Words[0].CategoryConstituent.Identifier);
            Assert.AreEqual(4, phrase.Words[1].Identifier);
            Assert.AreEqual(5, phrase.Words[1].CategoryConstituent.Identifier);
            Assert.AreEqual(6, phrase.Words[2].Identifier);
            Assert.AreEqual(7, phrase.Words[2].CategoryConstituent.Identifier);
            phrase = rule.Target.Phrase;
            Assert.AreEqual(8, phrase.Identifier);
            Assert.AreEqual(9, phrase.Words[0].Identifier);
            Assert.AreEqual(10, phrase.Words[0].CategoryConstituent.Identifier);
            Assert.AreEqual(11, phrase.Words[0].Affixes[0].Identifier);
            Assert.AreEqual(12, phrase.Words[0].Affixes[0].Features[0].Identifier);
            Assert.AreEqual(13, phrase.Words[0].Affixes[1].Identifier);
            Assert.AreEqual(14, phrase.Words[0].Affixes[1].Features[0].Identifier);
            Assert.AreEqual(15, phrase.Words[1].Identifier);
            Assert.AreEqual(16, phrase.Words[1].CategoryConstituent.Identifier);
            Assert.AreEqual(17, phrase.Words[1].Features[0].Identifier);
            Assert.AreEqual(18, phrase.Words[1].Affixes[0].Identifier);
            Assert.AreEqual(19, phrase.Words[1].Affixes[0].Features[0].Identifier);
            Assert.AreEqual(20, phrase.Words[2].Identifier);
            Assert.AreEqual(21, phrase.Words[2].CategoryConstituent.Identifier);
            Assert.AreEqual(22, phrase.Words[2].Affixes[0].Identifier);
            Assert.AreEqual(23, phrase.Words[2].Affixes[0].Features[0].Identifier);
            Assert.AreEqual(24, phrase.Words[2].Affixes[1].Identifier);
            Assert.AreEqual(25, phrase.Words[2].Affixes[1].Features[0].Identifier);
        }

        [Test]
        public void ParentsForEx1aDefNounTest()
        {
            const string kFileName = "Ex1a_Def-Noun";
            RuleGenExpected = Path.Combine(TestDataDir, kFileName + ".xml");
            provider.LoadDataFromFile(RuleGenExpected);
            ruleGenerator = provider.RuleGenerator;
            rule = ruleGenerator.FLExTransRules[0];
            idSetter.SetIdentifiersAndParents(rule);
            phrase = rule.Source.Phrase;
            Assert.AreEqual(rule, phrase.Parent);
            word = phrase.Words[0];
            Assert.AreEqual(phrase, word.Parent);
            category = word.CategoryConstituent;
            Assert.AreEqual(word, category.Parent);
            word = phrase.Words[1];
            Assert.AreEqual(phrase, word.Parent);
            category = word.CategoryConstituent;
            Assert.AreEqual(word, category.Parent);

            phrase = rule.Target.Phrase;
            word = phrase.Words[0];
            Assert.AreEqual(phrase, word.Parent);
            category = word.CategoryConstituent;
            Assert.AreEqual(word, category.Parent);
            feature = word.Features[0];
            Assert.AreEqual(word, feature.Parent);
            word = phrase.Words[1];
            Assert.AreEqual(phrase, word.Parent);
            category = word.CategoryConstituent;
            Assert.AreEqual(word, category.Parent);
            feature = word.Features[0];
            Assert.AreEqual(word, feature.Parent);
        }

        [Test]
        public void ParentsForEx4bIndefAdjNounTest()
        {
            const string kFileName = "Ex4b_Indef-Adj-Noun";
            RuleGenExpected = Path.Combine(TestDataDir, kFileName + ".xml");
            provider.LoadDataFromFile(RuleGenExpected);
            ruleGenerator = provider.RuleGenerator;
            rule = ruleGenerator.FLExTransRules[0];
            idSetter.SetIdentifiersAndParents(rule);

            phrase = rule.Source.Phrase;
            Assert.AreEqual(rule, phrase.Parent);
            word = phrase.Words[0];
            Assert.AreEqual(phrase, word.Parent);
            category = word.CategoryConstituent;
            Assert.AreEqual(word, category.Parent);

            word = phrase.Words[1];
            Assert.AreEqual(phrase, word.Parent);
            category = word.CategoryConstituent;
            Assert.AreEqual(word, category.Parent);

            word = phrase.Words[2];
            Assert.AreEqual(phrase, word.Parent);
            category = word.CategoryConstituent;
            Assert.AreEqual(word, category.Parent);

            phrase = rule.Target.Phrase;
            Assert.AreEqual(rule, phrase.Parent);
            word = phrase.Words[0];
            Assert.AreEqual(phrase, word.Parent);
            category = word.CategoryConstituent;
            Assert.AreEqual(word, category.Parent);
            affix = word.Affixes[0];
            Assert.AreEqual(word, affix.Parent);
            feature = affix.Features[0];
            Assert.AreEqual(affix, feature.Parent);
            affix = word.Affixes[1];
            Assert.AreEqual(word, affix.Parent);
            feature = affix.Features[0];
            Assert.AreEqual(affix, feature.Parent);

            word = phrase.Words[1];
            Assert.AreEqual(phrase, word.Parent);
            category = word.CategoryConstituent;
            Assert.AreEqual(word, category.Parent);
            feature = word.Features[0];
            Assert.AreEqual(word, feature.Parent);
            affix = word.Affixes[0];
            Assert.AreEqual(word, affix.Parent);
            feature = affix.Features[0];
            Assert.AreEqual(affix, feature.Parent);

            word = phrase.Words[2];
            Assert.AreEqual(phrase, word.Parent);
            category = word.CategoryConstituent;
            Assert.AreEqual(word, category.Parent);
            affix = word.Affixes[0];
            Assert.AreEqual(word, affix.Parent);
            feature = affix.Features[0];
            Assert.AreEqual(affix, feature.Parent);
            affix = word.Affixes[1];
            Assert.AreEqual(word, affix.Parent);
            feature = affix.Features[0];
            Assert.AreEqual(affix, feature.Parent);
        }
    }
}
