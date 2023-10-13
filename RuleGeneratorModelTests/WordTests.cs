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
    public class WordTests
    {
        Word word;

        [SetUp]
        public void Setup()
        {
            word = new Word();
            word.Id = "Source 1";
            word.Category = "Noun";
            word.CategoryConstituent = new Category(word.Category);
            word.Head = HeadValue.yes;
        }

        [Test]
        public void DeleteCategoryTest()
        {
            Assert.AreEqual("Noun", word.Category);
            Assert.NotNull(word.CategoryConstituent);
            Assert.AreEqual("Noun", word.CategoryConstituent.Name);
            word.DeleteCategory();
            Assert.AreEqual("", word.Category);
            Assert.NotNull(word.CategoryConstituent);
            Assert.AreEqual("", word.CategoryConstituent.Name);
        }

        [Test]
        public void InsertCategoryTest()
        {
            word.DeleteCategory();
            Assert.AreEqual("", word.Category);
            Assert.NotNull(word.CategoryConstituent);
            Assert.AreEqual("", word.CategoryConstituent.Name);
            word.InsertCategory("verb");
            Assert.AreEqual("verb", word.Category);
            Assert.NotNull(word.CategoryConstituent);
            Assert.AreEqual("verb", word.CategoryConstituent.Name);
        }

        [Test]
        public void DeleteAffixAtTest()
        {
            word.InsertNewAffixAt(AffixType.prefix, 0);
            word.InsertNewAffixAt(AffixType.suffix, 1);
            Assert.AreEqual(2, word.Affixes.Count);
            Assert.AreEqual(AffixType.prefix, word.Affixes[0].Type);
            Assert.AreEqual(AffixType.suffix, word.Affixes[1].Type);

            word.DeleteAffixAt(0);
            Assert.AreEqual(1, word.Affixes.Count);
            Assert.AreEqual(AffixType.suffix, word.Affixes[0].Type);

            word.InsertNewAffixAt(AffixType.prefix, 1);
            Assert.AreEqual(2, word.Affixes.Count);
            Assert.AreEqual(AffixType.suffix, word.Affixes[0].Type);
            Assert.AreEqual(AffixType.prefix, word.Affixes[1].Type);

            word.DeleteAffixAt(1);
            Assert.AreEqual(1, word.Affixes.Count);
            Assert.AreEqual(AffixType.suffix, word.Affixes[0].Type);

            word.DeleteAffixAt(0);
            Assert.AreEqual(0, word.Affixes.Count);
        }

        [Test]
        public void InsertNewAffixAtTest()
        {
            Assert.AreEqual(0, word.Affixes.Count);
            word.InsertNewAffixAt(AffixType.prefix, 0);
            Assert.AreEqual(1, word.Affixes.Count);
            Assert.AreEqual(AffixType.prefix, word.Affixes[0].Type);
            word.InsertNewAffixAt(AffixType.suffix, 0);
            Assert.AreEqual(2, word.Affixes.Count);
            Assert.AreEqual(AffixType.suffix, word.Affixes[0].Type);
            word.InsertNewAffixAt(AffixType.prefix, 2);
            Assert.AreEqual(3, word.Affixes.Count);
            Assert.AreEqual(AffixType.prefix, word.Affixes[2].Type);
        }
    }
}
