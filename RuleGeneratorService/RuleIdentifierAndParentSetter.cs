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
    public class RuleIdentifierAndParentSetter
    {
        private static readonly RuleIdentifierAndParentSetter instance =
            new RuleIdentifierAndParentSetter();

        public static int CurrentIdentifer { get; set; } = 0;

        public static RuleIdentifierAndParentSetter Instance
        {
            get { return instance; }
        }

        public void SetIdentifiersAndParents(FLExTransRule rule)
        {
            CurrentIdentifer = 0;
            var idSetter = RuleIdentifierAndParentSetter.instance;
            SetPhraseIdentifiers(rule.Source.Phrase);
            SetPhraseIdentifiers(rule.Target.Phrase);
            rule.Source.Phrase.Parent = rule;
            rule.Target.Phrase.Parent = rule;
        }

        private static void SetPhraseIdentifiers(Phrase phrase)
        {
            phrase.Identifier = ++CurrentIdentifer;
            foreach (Word word in phrase.Words)
            {
                word.Identifier = ++CurrentIdentifer;
                word.Parent = phrase;
                word.CategoryConstituent.Identifier = ++CurrentIdentifer;
                word.CategoryConstituent.Parent = word;
                var features = word.Features;
                SetFeatureIdentifiers(word.Features, word);
                foreach (Affix affix in word.Affixes)
                {
                    affix.Identifier = ++CurrentIdentifer;
                    affix.Parent = word;
                    SetFeatureIdentifiers(affix.Features, affix);
                }
            }
        }

        private static void SetFeatureIdentifiers(List<Feature> features, RuleConstituent parent)
        {
            foreach (Feature feature in features)
            {
                feature.Identifier = ++CurrentIdentifer;
                feature.Parent = parent;
            }
        }
    }
}
