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
    public class Affix : ConstituentWithFeatures
    {
        [XmlAttribute("type")]
        public AffixType Type { get; set; } = AffixType.suffix;

        public Affix() { }

        public RuleConstituent FindConstituent(int identifier)
        {
            RuleConstituent constituent = null;
            if (Identifier == identifier)
            {
                return this;
            }
            constituent = FindConstituentInFeatures(identifier);
            return constituent;
        }

        public string ProduceHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<li>");
            sb.Append(ProduceSpan("tf-nc affix", "a"));
            sb.Append(
                (Type == AffixType.prefix)
                    ? Properties.RuleGenModelStrings.prefix
                    : Properties.RuleGenModelStrings.suffix
            );
            sb.Append("</span>");
            if (Features.Count > 0)
            {
                sb.Append("<ul>");
                ProduceHtmlForFeatures(sb);
                sb.Append("</ul>");
            }
            sb.Append("</li>\n");
            return sb.ToString();
        }

        public Affix Duplicate()
        {
            Affix newAffix = new Affix();
            newAffix.Type = Type;
            newAffix.Features = DuplicateFeatures();
            return newAffix;
        }
    }

    public enum AffixType
    {
        prefix,
        suffix
    }
}
