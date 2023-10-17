// Copyright (c) 2022-2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using NUnit.Framework;
using SIL.FLExTransRuleGen.FLExModel;
using SIL.FLExTransRuleGen.Model;
using SIL.FLExTransRuleGen.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SIL.FLExTransRuleGenServiceTests
{
    public class Check : ServiceTestBase
    {
        XmlBackEndProviderFLExData providerFLExData;

        [SetUp]
        override public void Setup()
        {
            base.Setup();
            providerFLExData = new XmlBackEndProviderFLExData();
        }

        [Test]
        public void LoadFLExDataTest()
        {
            RuleGenExpected = Path.Combine(TestDataDir, "FLExDataSpanFrench.xml");
            providerFLExData.LoadDataFromFile(RuleGenExpected);
            FLExData flexData = providerFLExData.FLExData;
            Assert.NotNull(flexData);
            SourceFLExData source = flexData.SourceData;
            Assert.NotNull(source);
            Assert.AreEqual("Spanish-FLExTrans-Exp4", source.Name);
            Assert.NotNull(source.Categories);
            var cats = source.Categories;
            Assert.AreEqual(14, cats.Count);
            CheckCategory(cats, "adj", 0);
            CheckCategory(cats, "adv", 1);
            CheckCategory(cats, "conj", 2);
            CheckCategory(cats, "coordconj", 3);
            CheckCategory(cats, "cop", 4);
            CheckCategory(cats, "def", 5);
            CheckCategory(cats, "det", 6);
            CheckCategory(cats, "indf", 7);
            CheckCategory(cats, "n", 8);
            CheckCategory(cats, "nprop", 9);
            CheckCategory(cats, "prep", 10);
            CheckCategory(cats, "prepart", 11);
            CheckCategory(cats, "pro", 12);
            CheckCategory(cats, "v", 13);
            ChecFeatures(source.Features);

            TargetFLExData target = flexData.TargetData;
            Assert.NotNull(target);
            Assert.AreEqual("French-FLExTrans-Exp4", target.Name);
            Assert.NotNull(target.Categories);
            cats = target.Categories;
            Assert.AreEqual(15, cats.Count);
            CheckCategory(cats, "adj", 0);
            CheckCategory(cats, "adv", 1);
            CheckCategory(cats, "conj", 2);
            CheckCategory(cats, "coordconj", 3);
            CheckCategory(cats, "cop", 4);
            CheckCategory(cats, "def", 5);
            CheckCategory(cats, "det", 6);
            CheckCategory(cats, "existmrkr", 7);
            CheckCategory(cats, "indf", 8);
            CheckCategory(cats, "n", 9);
            CheckCategory(cats, "nprop", 10);
            CheckCategory(cats, "prep", 11);
            CheckCategory(cats, "prepart", 12);
            CheckCategory(cats, "pro", 13);
            CheckCategory(cats, "v", 14);

            ChecFeatures(target.Features);
        }

        private void ChecFeatures(List<FLExFeature> features)
        {
            Assert.AreEqual(5, features.Count);
            var values = CheckFeatureName(features, "absolute tense", 0, 4);
            CheckFeatureValue(values, "fut", 0);
            CheckFeatureValue(values, "pret", 1);
            CheckFeatureValue(values, "prs", 2);
            CheckFeatureValue(values, "pst", 3);
            values = CheckFeatureName(features, "case", 1, 3);
            CheckFeatureValue(values, "acc", 0);
            CheckFeatureValue(values, "dat", 1);
            CheckFeatureValue(values, "nom", 2);
            values = CheckFeatureName(features, "gender", 2, 3);
            CheckFeatureValue(values, "f", 0);
            CheckFeatureValue(values, "m", 1);
            CheckFeatureValue(values, "?", 2);
            values = CheckFeatureName(features, "number", 3, 2);
            CheckFeatureValue(values, "pl", 0);
            CheckFeatureValue(values, "sg", 1);
            values = CheckFeatureName(features, "person", 4, 3);
            CheckFeatureValue(values, "1", 0);
            CheckFeatureValue(values, "2", 1);
            CheckFeatureValue(values, "3", 2);
        }

        private void CheckFeatureValue(List<FLExFeatureValue> values, string sAbbr, int index)
        {
            FLExFeatureValue value = values[index];
            Assert.AreEqual(sAbbr, value.Abbreviation);
        }

        private List<FLExFeatureValue> CheckFeatureName(
            List<FLExFeature> features,
            string sName,
            int index,
            int iCount
        )
        {
            FLExFeature feat = features[index];
            Assert.AreEqual(sName, feat.Name);
            var values = feat.Values;
            Assert.AreEqual(iCount, values.Count);
            return values;
        }

        private void CheckCategory(List<FLExCategory> cats, string sAbbr, int index)
        {
            FLExCategory cat = cats[index];
            Assert.AreEqual(sAbbr, cat.Abbreviation);
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
    }
}
