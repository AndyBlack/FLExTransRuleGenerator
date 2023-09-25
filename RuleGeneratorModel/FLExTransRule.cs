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
    public class FLExTransRule
    {
        [XmlElement("source")]
        public Phrase Source { get; set; } = new Phrase();

        [XmlElement("target")]
        public Phrase Target { get; set; } = new Phrase();

        [XmlAttribute("name")]
        public string Name { get; set; } = "";

        public FLExTransRule() { }

        public FLExTransRule Duplicate()
        {
            FLExTransRule newRule = new FLExTransRule();
            newRule.Name = Name;
            Phrase newSource = Source.Duplicate();
            newRule.Source = newSource;
            Phrase newTarget = Target.Duplicate();
            newRule.Target = newTarget;
            ;
            return newRule;
        }
    }
}
