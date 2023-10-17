// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SIL.FLExTransRuleGen.FLExModel
{
    public class FLExFeatureValue
    {
        [XmlAttribute("abbr")]
        public string Abbreviation { get; set; } = "";

        public FLExFeatureValue() { }
    }
}
