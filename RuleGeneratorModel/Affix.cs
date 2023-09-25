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
    public class Affix
    {
        [XmlElement("features")]
        public List<Feature> Features { get; set; } = new List<Feature>();

        [XmlAttribute("type")]
        public AffixType Type { get; set; } = AffixType.suffix;

        public Affix() { }

        public Affix Duplicate()
        {
            Affix newAffix = new Affix();
            newAffix.Type = Type;
            foreach (Feature feature in Features)
            {
                newAffix.Features.Add(feature.Duplicate());
            }
            return newAffix;
        }
    }

    public enum AffixType
    {
        prefix,
        suffix
    }
}
