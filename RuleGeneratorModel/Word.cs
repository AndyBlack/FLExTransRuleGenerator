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
    public class Word : ConstituentWithFeatures
    {
        public List<Affix> Affixes { get; set; } = new List<Affix>();

        [XmlAttribute("id")]
        public string Id { get; set; } = "";

        [XmlAttribute("category")]
        public string Category { get; set; } = "";

        [XmlAttribute("head")]
        public HeadValue Head { get; set; } = HeadValue.no;

        [XmlIgnore]
        public Category CategoryConstituent { get; set; }

        public Word()
        {
            CategoryConstituent = new Category(Category);
        }

        public void DeleteCategory()
        {
            Category = "";
            CategoryConstituent = new Category("");
        }

        public void InsertCategory(string cat)
        {
            Category = cat;
            if (CategoryConstituent != null)
            {
                CategoryConstituent.Name = cat;
            }
            else
            {
                CategoryConstituent = new Category(cat);
            }
        }

        public void DeleteAffixAt(int index)
        {
            if (index < 0 || index >= Affixes.Count)
                return;
            Affixes.RemoveAt(index);
        }

        public void InsertAffixAt(Affix affix, int index)
        {
            if (index < 0 || (index > Affixes.Count && Affixes.Count > 0))
                return;
            Affixes.Insert(index, affix);
        }

        public void InsertNewAffixAt(AffixType type, int index)
        {
            if (index < 0 || (index > Affixes.Count && Affixes.Count > 0))
                return;
            Affix newAffix = new Affix();
            newAffix.Type = type;
            Affixes.Insert(index, newAffix);
        }

        public void SwapPositionOfAffixes(int index, int otherIndex)
        {
            if (index < 0 | index >= Affixes.Count)
                return;
            if (otherIndex < 0 | otherIndex >= Affixes.Count)
                return;
            Affix affix = Affixes[index];
            Affix otherAffix = Affixes[otherIndex];
            Affixes[index] = otherAffix;
            Affixes[otherIndex] = affix;
        }

        public RuleConstituent FindConstituent(int identifier)
        {
            RuleConstituent constituent = null;
            if (Identifier == identifier)
            {
                return this;
            }
            constituent = CategoryConstituent.FindConstituent(identifier);
            if (constituent != null)
            {
                return constituent;
            }
            constituent = FindConstituentInFeatures(identifier);
            if (constituent != null)
            {
                return constituent;
            }
            foreach (Affix affix in Affixes)
            {
                constituent = affix.FindConstituent(identifier);
                if (constituent != null)
                {
                    return constituent;
                }
            }
            return constituent;
        }

        public string ProduceHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<li>");
            sb.Append(ProduceSpan("tf-nc", "w"));
            sb.Append(Properties.RuleGenModelStrings.word);
            if (Head == HeadValue.yes)
            {
                sb.Append("(");
                sb.Append("<span style=\"font-style:italic; font-size:smaller\">");
                sb.Append(Properties.RuleGenModelStrings.head);
                sb.Append("</span>");
                sb.Append(")");
            }
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
                    sb.Append(CategoryConstituent.ProduceHtml());
                }
                ProduceHtmlForFeatures(sb);
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
            newWord.CategoryConstituent = CategoryConstituent.Duplicate();
            newWord.Head = Head;
            foreach (Affix affix in Affixes)
            {
                newWord.Affixes.Add(affix.Duplicate());
            }
            newWord.Features = DuplicateFeatures();
            return newWord;
        }
    }

    public enum HeadValue
    {
        yes,
        no
    }
}
