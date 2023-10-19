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
    public abstract class ConstituentWithFeatures : RuleConstituent
    {
        public List<Feature> Features { get; set; } = new List<Feature>();

        public ConstituentWithFeatures() { }

        public void DeleteFeature(Feature feature)
        {
            Features.Remove(feature);
        }

        public Feature InsertNewFeature(string label, string match)
        {
            Feature feature = new Feature();
            feature.Label = label;
            feature.Match = match;
            Features.Add(feature);
            return feature;
        }

        protected RuleConstituent FindConstituentInFeatures(int identifier)
        {
            RuleConstituent constituent = null;
            foreach (Feature feature in Features)
            {
                constituent = feature.FindConstituent(identifier);
                if (constituent != null)
                {
                    return constituent;
                }
            }
            return constituent;
        }

        protected void ProduceHtmlForFeatures(StringBuilder sb)
        {
            foreach (Feature feature in Features)
            {
                sb.Append(feature.ProduceHtml());
            }
        }

        protected List<Feature> DuplicateFeatures()
        {
            List<Feature> newFeatures = new List<Feature>();
            foreach (Feature feature in Features)
            {
                newFeatures.Add(feature.Duplicate());
            }
            return newFeatures;
        }
    }
}
