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
    public class RuleIdentifierSetter
    {
        private static readonly RuleIdentifierSetter instance = new RuleIdentifierSetter();

        public static int CurrentIdentifer { get; set; } = 0;

        public static RuleIdentifierSetter Instance
        {
            get { return instance; }
        }

        public void SetIdentifiers(FLExTransRule rule)
        {
            CurrentIdentifer = 0;
            var idSetter = RuleIdentifierSetter.instance;
            SetPhraseIdentifiers(rule.Source.Phrase);
            SetPhraseIdentifiers(rule.Target.Phrase);
        }

        private static void SetPhraseIdentifiers(Phrase phrase)
        {
            phrase.Identifier = ++CurrentIdentifer;
            foreach (Word word in phrase.Words)
            {
                word.Identifier = ++CurrentIdentifer;
                word.CategoryConstituent.Identifier = ++CurrentIdentifer;
                var features = word.Features;
                SetFeatureIdentifiers(word.Features);
                foreach (Affix affix in word.Affixes)
                {
                    affix.Identifier = ++CurrentIdentifer;
                    SetFeatureIdentifiers(affix.Features);
                }
            }
        }

        private static void SetFeatureIdentifiers(List<Feature> features)
        {
            foreach (Feature feature in features)
            {
                feature.Identifier = ++CurrentIdentifer;
            }
        }
    }
}
