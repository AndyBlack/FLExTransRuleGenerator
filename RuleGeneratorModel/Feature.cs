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
    public class Feature
    {
        [XmlAttribute("match")]
        public string Match { get; set; } = "";

        [XmlAttribute("label")]
        public string Label { get; set; } = "";

        public Feature() { }

        public Feature Duplicate()
        {
            Feature newFeature = new Feature();
            newFeature.Match = Match;
            newFeature.Label = Label;
            return newFeature;
        }
    }
}
