﻿// Copyright (c) 2023 SIL International
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

        public string ProduceHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<li>");
            sb.Append("<span class=\"tf-nc\">word");
            if (Id.Length > 0)
            {
                sb.Append("<span class=\"index\">");
                sb.Append(Id);
                sb.Append("</span></span>\n");
            }
            if (Category.Length > 0 || Features.Count > 0 || Affixes.Count > 0)
            {
                sb.Append("<ul>\n");
                if (Category.Length > 0)
                {
                    sb.Append("<li><span class=\"tf-nc category\">cat:");
                    sb.Append(Category);
                    sb.Append("</span></li>\n");
                }
                foreach (Feature feature in Features)
                {
                    sb.Append(feature.ProduceHtml());
                }
                foreach (Affix affix in Affixes)
                {
                    sb.Append(affix.ProduceHtml());
                }
                sb.Append("</ul>\n");
            }
            sb.Append("</li>");
            return sb.ToString();
        }

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
