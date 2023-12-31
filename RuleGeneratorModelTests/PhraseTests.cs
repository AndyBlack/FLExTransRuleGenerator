﻿// Copyright (c) 2023 SIL International
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
    public class PhraseTests
    {
        FLExTransRuleGenerator ruleGenerator;
        Phrase sourcePhrase;
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
            sourcePhrase = source.Phrase;
            sourceWord = new Word();
            sourceWord.Id = "Source 1";
            sourceWord.Category = "Noun";
            sourceWord.Head = HeadValue.yes;
            sourcePhrase.Words.Add(sourceWord);
            sourceWord2 = new Word();
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
        public void DeleteWordAtTest()
        {
            Assert.AreEqual(2, sourcePhrase.Words.Count);
            sourcePhrase.DeleteWordAt(-1); // is a no-op
            Assert.AreEqual(2, sourcePhrase.Words.Count);
            sourcePhrase.DeleteWordAt(2); // is a no-op
            Assert.AreEqual(2, sourcePhrase.Words.Count);

            sourcePhrase.DeleteWordAt(1);
            Assert.AreEqual(1, sourcePhrase.Words.Count);
            Assert.AreEqual("Source 1", sourcePhrase.Words[0].Id);
            sourcePhrase.DeleteWordAt(0);
            Assert.AreEqual(0, sourcePhrase.Words.Count);
        }

        [Test]
        public void InsertNewWordAtTest()
        {
            Assert.AreEqual(2, sourcePhrase.Words.Count);
            sourcePhrase.InsertNewWordAt(-1); // is a no-op
            Assert.AreEqual(2, sourcePhrase.Words.Count);
            sourcePhrase.InsertNewWordAt(2); // is a no-op
            Assert.AreEqual(2, sourcePhrase.Words.Count);
            sourcePhrase.InsertNewWordAt(1);
            Assert.AreEqual(3, sourcePhrase.Words.Count);
            Assert.AreEqual("3", sourcePhrase.Words[1].Id);
            sourcePhrase.InsertNewWordAt(0);
            Assert.AreEqual(4, sourcePhrase.Words.Count);
            Assert.AreEqual("4", sourcePhrase.Words[0].Id);
        }

        [Test]
        public void SwapPositionOfWordsTest()
        {
            Assert.AreEqual(2, sourcePhrase.Words.Count);
            sourcePhrase.SwapPositionOfWords(-1, 0); // is a no-op
            Assert.AreEqual(2, sourcePhrase.Words.Count);
            sourcePhrase.SwapPositionOfWords(2, 0); // is a no-op
            Assert.AreEqual(2, sourcePhrase.Words.Count);
            sourcePhrase.SwapPositionOfWords(0, -1); // is a no-op
            Assert.AreEqual(2, sourcePhrase.Words.Count);
            sourcePhrase.SwapPositionOfWords(0, 2); // is a no-op
            Assert.AreEqual(2, sourcePhrase.Words.Count);

            sourcePhrase.SwapPositionOfWords(0, 1);
            Assert.AreEqual("Source 2", sourcePhrase.Words[0].Id);
            Assert.AreEqual("Source 1", sourcePhrase.Words[1].Id);
            sourcePhrase.SwapPositionOfWords(1, 0);
            Assert.AreEqual("Source 1", sourcePhrase.Words[0].Id);
            Assert.AreEqual("Source 2", sourcePhrase.Words[1].Id);
        }

        [Test]
        public void MarkWordAsHeadTest()
        {
            Assert.AreEqual(HeadValue.yes, sourcePhrase.Words[0].Head);
            Assert.AreEqual(HeadValue.no, sourcePhrase.Words[1].Head);
            sourcePhrase.MarkWordAsHead(sourceWord2);
            Assert.AreEqual(HeadValue.no, sourcePhrase.Words[0].Head);
            Assert.AreEqual(HeadValue.yes, sourcePhrase.Words[1].Head);
        }
    }
}
