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
    public class ConstituentFinderTests : ServiceTestBase
    {
        RuleIdentifierAndParentSetter idSetter;
        FLExTransRuleGenerator ruleGenerator;
        ConstituentFinder finder;
        FLExTransRule rule;
        Phrase phrase;
        Word word;
        Category category;
        Feature feature;
        Affix affix;
        RuleConstituent constituent;

        [SetUp]
        override public void Setup()
        {
            base.Setup();
            idSetter = RuleIdentifierAndParentSetter.Instance;
            finder = ConstituentFinder.Instance;
        }

        [Test]
        public void SetForEx1aDefNounTest()
        {
            const string kFileName = "Ex1a_Def-Noun";
            RuleGenExpected = Path.Combine(TestDataDir, kFileName + ".xml");
            provider.LoadDataFromFile(RuleGenExpected);
            ruleGenerator = provider.RuleGenerator;
            rule = ruleGenerator.FLExTransRules[0];
            idSetter.SetIdentifiersAndParents(rule);

            constituent = finder.FindConstituent(rule, 0);
            Assert.IsNull(constituent);
            constituent = finder.FindConstituent(rule, 1);
            phrase = constituent as Phrase;
            Assert.IsNotNull(phrase);
            constituent = finder.FindConstituent(rule, 2);
            word = constituent as Word;
            Assert.IsNotNull(word);
            constituent = finder.FindConstituent(rule, 3);
            category = constituent as Category;
            Assert.IsNotNull(category);
            Assert.AreEqual("def", category.Name);

            constituent = finder.FindConstituent(rule, 4);
            word = constituent as Word;
            Assert.IsNotNull(word);
            constituent = finder.FindConstituent(rule, 5);
            category = constituent as Category;
            Assert.IsNotNull(category);
            Assert.AreEqual("n", category.Name);

            constituent = finder.FindConstituent(rule, 6);
            phrase = constituent as Phrase;
            Assert.IsNotNull(phrase);
            constituent = finder.FindConstituent(rule, 7);
            word = constituent as Word;
            Assert.IsNotNull(word);
            constituent = finder.FindConstituent(rule, 8);
            category = constituent as Category;
            Assert.IsNotNull(category);
            Assert.AreEqual("", category.Name);
            constituent = finder.FindConstituent(rule, 9);
            feature = constituent as Feature;
            Assert.IsNotNull(feature);
            Assert.AreEqual("gender", feature.Label);
            Assert.AreEqual("α", feature.Match);

            constituent = finder.FindConstituent(rule, 10);
            word = constituent as Word;
            Assert.IsNotNull(word);
            constituent = finder.FindConstituent(rule, 11);
            category = constituent as Category;
            Assert.IsNotNull(category);
            Assert.AreEqual("", category.Name);
            constituent = finder.FindConstituent(rule, 12);
            feature = constituent as Feature;
            Assert.IsNotNull(feature);
            Assert.AreEqual("gender", feature.Label);
            Assert.AreEqual("α", feature.Match);

            constituent = finder.FindConstituent(rule, 13);
            Assert.IsNull(constituent);
        }

        [Test]
        public void SetForEx4bIndefAdjNounTest()
        {
            const string kFileName = "Ex4b_Indef-Adj-Noun";
            RuleGenExpected = Path.Combine(TestDataDir, kFileName + ".xml");
            provider.LoadDataFromFile(RuleGenExpected);
            ruleGenerator = provider.RuleGenerator;
            rule = ruleGenerator.FLExTransRules[0];
            idSetter.SetIdentifiersAndParents(rule);

            idSetter.SetIdentifiersAndParents(rule);

            constituent = finder.FindConstituent(rule, 0);
            Assert.IsNull(constituent);
            constituent = finder.FindConstituent(rule, 1);
            phrase = constituent as Phrase;
            Assert.IsNotNull(phrase);
            constituent = finder.FindConstituent(rule, 2);
            word = constituent as Word;
            Assert.IsNotNull(word);
            constituent = finder.FindConstituent(rule, 3);
            category = constituent as Category;
            Assert.IsNotNull(category);
            Assert.AreEqual("indef", category.Name);

            constituent = finder.FindConstituent(rule, 4);
            word = constituent as Word;
            Assert.IsNotNull(word);
            constituent = finder.FindConstituent(rule, 5);
            category = constituent as Category;
            Assert.IsNotNull(category);
            Assert.AreEqual("adj", category.Name);

            constituent = finder.FindConstituent(rule, 6);
            word = constituent as Word;
            Assert.IsNotNull(word);
            constituent = finder.FindConstituent(rule, 7);
            category = constituent as Category;
            Assert.IsNotNull(category);
            //Assert.AreEqual("n", category.Name);

            constituent = finder.FindConstituent(rule, 8);
            phrase = constituent as Phrase;
            Assert.IsNotNull(phrase);
            constituent = finder.FindConstituent(rule, 9);
            word = constituent as Word;
            Assert.IsNotNull(word);
            constituent = finder.FindConstituent(rule, 10);
            category = constituent as Category;
            Assert.IsNotNull(category);
            Assert.AreEqual("", category.Name);
            constituent = finder.FindConstituent(rule, 11);
            affix = constituent as Affix;
            Assert.IsNotNull(affix);
            constituent = finder.FindConstituent(rule, 12);
            feature = constituent as Feature;
            Assert.IsNotNull(feature);
            Assert.AreEqual("gender", feature.Label);
            Assert.AreEqual("α", feature.Match);
            constituent = finder.FindConstituent(rule, 13);
            affix = constituent as Affix;
            Assert.IsNotNull(affix);
            constituent = finder.FindConstituent(rule, 14);
            feature = constituent as Feature;
            Assert.IsNotNull(feature);
            Assert.AreEqual("number", feature.Label);
            Assert.AreEqual("β", feature.Match);

            constituent = finder.FindConstituent(rule, 15);
            word = constituent as Word;
            Assert.IsNotNull(word);
            constituent = finder.FindConstituent(rule, 16);
            category = constituent as Category;
            Assert.IsNotNull(category);
            Assert.AreEqual("", category.Name);
            constituent = finder.FindConstituent(rule, 17);
            feature = constituent as Feature;
            Assert.IsNotNull(feature);
            Assert.AreEqual("gender", feature.Label);
            Assert.AreEqual("α", feature.Match);
            constituent = finder.FindConstituent(rule, 18);
            affix = constituent as Affix;
            Assert.IsNotNull(affix);
            constituent = finder.FindConstituent(rule, 19);
            feature = constituent as Feature;
            Assert.IsNotNull(feature);
            Assert.AreEqual("number", feature.Label);
            Assert.AreEqual("β", feature.Match);

            constituent = finder.FindConstituent(rule, 20);
            word = constituent as Word;
            Assert.IsNotNull(word);
            constituent = finder.FindConstituent(rule, 21);
            category = constituent as Category;
            Assert.IsNotNull(category);
            Assert.AreEqual("", category.Name);
            constituent = finder.FindConstituent(rule, 22);
            affix = constituent as Affix;
            Assert.IsNotNull(affix);
            constituent = finder.FindConstituent(rule, 23);
            feature = constituent as Feature;
            Assert.IsNotNull(feature);
            Assert.AreEqual("gender", feature.Label);
            Assert.AreEqual("α", feature.Match);
            constituent = finder.FindConstituent(rule, 24);
            affix = constituent as Affix;
            Assert.IsNotNull(affix);
            constituent = finder.FindConstituent(rule, 25);
            feature = constituent as Feature;
            Assert.IsNotNull(feature);
            Assert.AreEqual("number", feature.Label);
            Assert.AreEqual("β", feature.Match);
        }
    }
}
