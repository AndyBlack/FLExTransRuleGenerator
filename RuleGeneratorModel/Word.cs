// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SIL.FLExTransRuleGen.Model
{
    public class Word
    {
        public List<Feature> Features { get; set; } = new List<Feature>();

        public List<Affix> Affixes { get; set; } = new List<Affix>();

        [XmlAttribute("id")]
        public string Id { get; set; } = "";

        [XmlAttribute("category")]
        public string Category { get; set; } = "";

        [XmlAttribute("head")]
        public HeadValue Head { get; set; } = HeadValue.no;

        public Word() { }

        public Word Duplicate()
        {
            Word newWord = new Word();
            newWord.Id = Id;
            newWord.Category = Category;
            newWord.Head = Head;
            foreach (Affix affix in Affixes)
            {
                newWord.Affixes.Add(affix.Duplicate());
            }
            foreach (Feature feature in Features)
            {
                newWord.Features.Add(feature.Duplicate());
            }
            return newWord;
        }
    }

    public enum HeadValue
    {
        yes,
        no
    }
}
