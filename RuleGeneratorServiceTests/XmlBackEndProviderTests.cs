// Copyright (c) 2022-2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using NUnit.Framework;
using SIL.FLExTransRuleGen.Model;
using SIL.FLExTransRuleGen.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SIL.FLExTransRuleGenServiceTests
{
    public class XmlBackEndProviderTests
    {
        string RuleGenProduced { get; set; }

        protected XmlBackEndProvider provider = new XmlBackEndProvider();
        protected string TestDataDir { get; set; }
        protected string RuleGenFile { get; set; }
        protected string RuleGenExpected { get; set; }
        protected string ExpectedFileName = "RuleGenExpected.xml";
        protected const string kTestDir = "RuleGeneratorServiceTests";

        [SetUp]
        public void Setup()
        {
            Uri uriBase = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var rootdir = Path.GetDirectoryName(Uri.UnescapeDataString(uriBase.AbsolutePath));
            int i = rootdir.LastIndexOf(kTestDir);
            string basedir = rootdir.Substring(0, i);
            TestDataDir = Path.Combine(basedir, kTestDir, "TestData");
            RuleGenExpected = Path.Combine(TestDataDir, ExpectedFileName);
        }

        [Test]
        public void LoadTestEx1aDefNoun()
        {
            RuleGenExpected = Path.Combine(TestDataDir, "Ex1a_Def-Noun.xml");
            provider.LoadDataFromFile(RuleGenExpected);
            FLExTransRuleGenerator ruleGenerator = provider.RuleGenerator;
            Assert.NotNull(ruleGenerator);
            Assert.AreEqual(1, ruleGenerator.FLExTransRules.Count);
            FLExTransRule ftRule = ruleGenerator.FLExTransRules[0];
            Assert.AreEqual("Definite - Noun", ftRule.Name);

            Source source = ftRule.Source;
            Assert.NotNull(source);
            Phrase sourcePhrase = source.Phrase;
            List<Word> words = sourcePhrase.Words;
            Assert.AreEqual(2, words.Count);
            Word word = words[0];
            CheckWordAttributes(word, "1", "def", HeadValue.no);
            Assert.AreEqual(0, word.Affixes.Count);
            Assert.AreEqual(0, word.Features.Count);
            word = words[1];
            CheckWordAttributes(word, "2", "n", HeadValue.no);
            Assert.AreEqual(0, word.Affixes.Count);
            Assert.AreEqual(0, word.Features.Count);

            Target target = ftRule.Target;
            Assert.NotNull(target);
            Phrase targetPhrase = target.Phrase;
            words = targetPhrase.Words;
            Assert.AreEqual(2, words.Count);
            word = words[0];
            CheckWordAttributes(word, "1", "", HeadValue.no);
            Assert.AreEqual(0, word.Affixes.Count);
            Assert.AreEqual(1, word.Features.Count);
            Feature feature = word.Features[0];
            CheckFeatureAttributes(feature, "gender", "α");
            word = words[1];
            CheckWordAttributes(word, "2", "", HeadValue.yes);
            Assert.AreEqual(0, word.Affixes.Count);
            Assert.AreEqual(1, word.Features.Count);
            feature = word.Features[0];
            CheckFeatureAttributes(feature, "gender", "α");
        }

        [Test]
        public void LoadTestEx4bIndefAdjNoun()
        {
            RuleGenExpected = Path.Combine(TestDataDir, "Ex4b_Indef-Adj-Noun.xml");
            provider.LoadDataFromFile(RuleGenExpected);
            FLExTransRuleGenerator ruleGenerator = provider.RuleGenerator;
            Assert.NotNull(ruleGenerator);
            Assert.AreEqual(1, ruleGenerator.FLExTransRules.Count);
            FLExTransRule ftRule = ruleGenerator.FLExTransRules[0];
            Assert.AreEqual("Indefinite - Adjective - Noun", ftRule.Name);

            Source source = ftRule.Source;
            Assert.NotNull(source);
            Phrase sourcePhrase = source.Phrase;
            List<Word> words = sourcePhrase.Words;
            Assert.AreEqual(3, words.Count);
            Word word = words[0];
            CheckWordAttributes(word, "1", "indef", HeadValue.no);
            Assert.AreEqual(0, word.Affixes.Count);
            Assert.AreEqual(0, word.Features.Count);
            word = words[1];
            CheckWordAttributes(word, "2", "adj", HeadValue.no);
            Assert.AreEqual(0, word.Affixes.Count);
            Assert.AreEqual(0, word.Features.Count);
            word = words[2];
            CheckWordAttributes(word, "3", "n", HeadValue.no);
            Assert.AreEqual(0, word.Affixes.Count);
            Assert.AreEqual(0, word.Features.Count);

            Target target = ftRule.Target;
            Assert.NotNull(target);
            Phrase targetPhrase = target.Phrase;
            words = targetPhrase.Words;
            Assert.AreEqual(3, words.Count);
            word = words[0];
            CheckWordAttributes(word, "1", "", HeadValue.no);
            Assert.AreEqual(2, word.Affixes.Count);
            Assert.AreEqual(0, word.Features.Count);
            Affix affix = word.Affixes[0];
            Assert.AreEqual(AffixType.suffix, affix.Type);
            Assert.AreEqual(1, affix.Features.Count);
            Feature feature = affix.Features[0];
            CheckFeatureAttributes(feature, "gender", "α");
            affix = word.Affixes[1];
            Assert.AreEqual(AffixType.suffix, affix.Type);
            Assert.AreEqual(1, affix.Features.Count);
            feature = affix.Features[0];
            CheckFeatureAttributes(feature, "number", "β");

            word = words[1];
            CheckWordAttributes(word, "3", "", HeadValue.yes);
            Assert.AreEqual(1, word.Affixes.Count);
            Assert.AreEqual(1, word.Features.Count);
            feature = word.Features[0];
            CheckFeatureAttributes(feature, "gender", "α");
            affix = word.Affixes[0];
            Assert.AreEqual(AffixType.suffix, affix.Type);
            Assert.AreEqual(1, affix.Features.Count);
            feature = affix.Features[0];
            CheckFeatureAttributes(feature, "number", "β");

            word = words[2];
            CheckWordAttributes(word, "2", "", HeadValue.no);
            Assert.AreEqual(2, word.Affixes.Count);
            Assert.AreEqual(0, word.Features.Count);
            affix = word.Affixes[0];
            Assert.AreEqual(AffixType.suffix, affix.Type);
            Assert.AreEqual(1, affix.Features.Count);
            feature = affix.Features[0];
            CheckFeatureAttributes(feature, "gender", "α");
            affix = word.Affixes[1];
            Assert.AreEqual(AffixType.prefix, affix.Type);
            Assert.AreEqual(1, affix.Features.Count);
            feature = affix.Features[0];
            CheckFeatureAttributes(feature, "number", "β");
        }

        protected void CheckWordAttributes(Word word, string id, string category, HeadValue head)
        {
            Assert.NotNull(word);
            Assert.AreEqual(id, word.Id);
            Assert.AreEqual(category, word.Category);
            Assert.AreEqual(head, word.Head);
        }

        protected void CheckFeatureAttributes(Feature feature, string label, string match)
        {
            Assert.NotNull(feature);
            Assert.AreEqual(label, feature.Label);
            Assert.AreEqual(match, feature.Match);
        }

        [Test]
        public void SaveTest()
        {
            FLExTransRuleGenerator ruleGenerator = new FLExTransRuleGenerator();
            FLExTransRule ftRule = new FLExTransRule();
            ftRule.Name = "Indefinite - Adjective - Noun";
            Source source = new Source();
            Phrase sourcePhrase = new Phrase();
            Word word1 = MakeWordAttritbutes("1", "indef", HeadValue.no);
            sourcePhrase.Words.Add(word1);
            Word word2 = MakeWordAttritbutes("2", "adj", HeadValue.no);
            sourcePhrase.Words.Add(word2);
            Word word3 = MakeWordAttritbutes("3", "n", HeadValue.no);
            sourcePhrase.Words.Add(word3);
            source.Phrase = sourcePhrase;
            ftRule.Source = source;

            Target target = new Target();
            Phrase targetPhrase = new Phrase();
            word1 = MakeWordAttritbutes("1", "", HeadValue.no);
            List<Feature> features1 = new List<Feature>();
            features1.Add(MakeFeatureAttributes("gender", "α"));
            Affix affix1 = MakeAffix(AffixType.suffix, features1);
            word1.Affixes.Add(affix1);
            List<Feature> features2 = new List<Feature>();
            features2.Add(MakeFeatureAttributes("number", "β"));
            Affix affix2 = MakeAffix(AffixType.suffix, features2);
            word1.Affixes.Add(affix2);
            targetPhrase.Words.Add(word1);

            word2 = MakeWordAttritbutes("3", "", HeadValue.yes);
            features2 = new List<Feature>();
            features2.Add(MakeFeatureAttributes("number", "β"));
            affix2 = MakeAffix(AffixType.suffix, features2);
            word2.Affixes.Add(affix2);
            List<Feature> wordFeatures = new List<Feature>();
            wordFeatures.Add(MakeFeatureAttributes("gender", "α"));
            word2.Features = wordFeatures;
            targetPhrase.Words.Add(word2);

            word3 = MakeWordAttritbutes("2", "", HeadValue.no);
            List<Feature> features31 = new List<Feature>();
            features31.Add(MakeFeatureAttributes("gender", "α"));
            Affix affix3 = MakeAffix(AffixType.suffix, features31);
            word3.Affixes.Add(affix3);
            List<Feature> features32 = new List<Feature>();
            features32.Add(MakeFeatureAttributes("number", "β"));
            affix2 = MakeAffix(AffixType.suffix, features32);
            word3.Affixes.Add(affix2);
            targetPhrase.Words.Add(word3);
            target.Phrase = targetPhrase;
            ftRule.Target = target;
            ruleGenerator.FLExTransRules.Add(ftRule);

            provider.RuleGenerator = ruleGenerator;

            string xmlFile = Path.Combine(Path.GetTempPath(), "RuleGen.xml");
            provider.SaveDataToFile(xmlFile);
            using (var streamReader = new StreamReader(RuleGenExpected, Encoding.UTF8))
            {
                RuleGenExpected = streamReader.ReadToEnd().Replace("\r", "");
            }
            using (var streamReader = new StreamReader(xmlFile, Encoding.UTF8))
            {
                RuleGenProduced = streamReader.ReadToEnd().Replace("\r", "");
            }
            Assert.AreEqual(RuleGenExpected, RuleGenProduced);
        }

        protected Word MakeWordAttritbutes(string id, string category, HeadValue head)
        {
            Word word = new Word();
            word.Id = id;
            word.Category = category;
            word.Head = head;
            return word;
        }

        protected Affix MakeAffix(AffixType type, List<Feature> features)
        {
            Affix affix = new Affix();
            affix.Type = type;
            affix.Features = features;
            return affix;
        }

        protected Feature MakeFeatureAttributes(string label, string match)
        {
            Feature feature = new Feature();
            feature.Label = label;
            feature.Match = match;
            return feature;
        }
    }
}
