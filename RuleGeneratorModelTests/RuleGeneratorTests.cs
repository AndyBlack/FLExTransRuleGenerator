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
    public class RuleGeneratorTests
    {
        FLExTransRuleGenerator ruleGenerator;

        [SetUp]
        public void Setup()
        {
            ruleGenerator = new FLExTransRuleGenerator();
        }

        [Test]
        public void NewOperationTest()
        {
            Assert.AreEqual(0, ruleGenerator.Rules.Count);

            FLExTransRule rule = new FLExTransRule();
            ruleGenerator.Rules.Add(rule);
            Assert.AreEqual(1, ruleGenerator.Rules.Count);
            Phrase source = rule.Source;
            Assert.IsNotNull(source);
            Phrase target = rule.Target;
            Assert.IsNotNull(target);
            Assert.AreEqual(0, source.Words.Count);

            Word sourceWord = new Word();
            source.Words.Add(sourceWord);
            Assert.AreEqual(1, source.Words.Count);
            Assert.AreEqual("", sourceWord.Id);
            Assert.AreEqual("", sourceWord.Category);
            Assert.AreEqual(HeadValue.no, sourceWord.Head);
            Assert.AreEqual(0, sourceWord.Affixes.Count);
            Assert.AreEqual(0, sourceWord.Features.Count);
            Assert.AreEqual(0, target.Words.Count);

            Word targetWord = new Word();
            target.Words.Add(targetWord);
            Assert.AreEqual(1, target.Words.Count);
            Assert.AreEqual("", targetWord.Id);
            Assert.AreEqual("", targetWord.Category);
            Assert.AreEqual(HeadValue.no, targetWord.Head);
            Assert.AreEqual(0, targetWord.Affixes.Count);
            Assert.AreEqual(0, targetWord.Features.Count);

            Affix affix = new Affix();
            sourceWord.Affixes.Add(affix);
            Assert.AreEqual(1, sourceWord.Affixes.Count);
            Assert.AreEqual(AffixType.suffix, affix.Type);
            Assert.AreEqual(0, affix.Features.Count);
            Feature affixFeature = new Feature();
            affix.Features.Add(affixFeature);
            Assert.AreEqual(1, affix.Features.Count);
            Assert.AreEqual("", affixFeature.Match);
            Assert.AreEqual("", affixFeature.Label);
        }
    }
}
