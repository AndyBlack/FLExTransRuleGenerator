// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.FLExTransRuleGen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.FLExTransRuleGen.Service
{
    public class ConstituentFinder
    {
        private static readonly ConstituentFinder instance = new ConstituentFinder();

        public static ConstituentFinder Instance
        {
            get { return instance; }
        }

        public RuleConstituentBase FindConstituent(FLExTransRule rule, int identifier)
        {
            RuleConstituentBase constituent = null;
            if (identifier < rule.Target.Phrase.Identifier)
            {
                constituent = rule.Source.Phrase.FindConstituent(identifier);
                if (constituent != null)
                    return constituent;
            }
            else
            {
                constituent = rule.Target.Phrase.FindConstituent(identifier);
            }
            return constituent;
        }
    }
}
