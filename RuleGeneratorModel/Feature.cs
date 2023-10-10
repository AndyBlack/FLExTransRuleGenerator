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
    public class Feature : RuleConstituent
    {
        [XmlAttribute("match")]
        public string Match { get; set; } = "";

        [XmlAttribute("label")]
        public string Label { get; set; } = "";

        public Feature() { }

        public RuleConstituent FindConstituent(int identifier)
        {
            RuleConstituent constituent = null;
            if (Identifier == identifier)
            {
                return this;
            }
            return constituent;
        }

        public string ProduceHtml()
        {
            StringBuilder sb = new StringBuilder();
            if (Label.Length > 0 || Match.Length > 0)
            {
                sb.Append("<li>");
                sb.Append(ProduceSpan("tf-nc feature", "f"));
                sb.Append((Label.Length > 0) ? Label : Properties.RuleGenModelStrings.FeatureX);
                sb.Append(":");
                sb.Append((Match.Length > 0) ? Match : Properties.RuleGenModelStrings.MatchX);
                sb.Append("</span></li>\n");
            }
            return sb.ToString();
        }

        public Feature Duplicate()
        {
            Feature newFeature = new Feature();
            newFeature.Match = Match;
            newFeature.Label = Label;
            return newFeature;
        }
    }
}
