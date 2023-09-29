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
        public Source Source { get; set; } = new Source();

        public Target Target { get; set; } = new Target();

        [XmlAttribute("name")]
        public string Name { get; set; } = "";

        public FLExTransRule() { }

        public FLExTransRule Duplicate()
        {
            FLExTransRule newRule = new FLExTransRule();
            newRule.Name = Name;
            Source newSource = Source.Duplicate();
            newRule.Source = newSource;
            Target newTarget = Target.Duplicate();
            newRule.Target = newTarget;
            return newRule;
        }

        public override string ToString()
        {
            string result = Name;
            if (Name.Length == 0)
            {
                result = Properties.RuleGenModelStrings.NameMissing;
            }
            return result;
        }
    }
}
