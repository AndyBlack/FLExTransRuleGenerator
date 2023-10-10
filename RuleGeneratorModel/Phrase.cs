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
    public class Phrase : RuleConstituentBase
    {
        public List<Word> Words { get; set; } = new List<Word>();

        [XmlIgnore]
        public PhraseType Type { get; set; } = PhraseType.source;

        public Phrase() { }

        public string ProduceHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<li>");
            sb.Append(ProduceSpan("tf-nc", "p"));
            sb.Append(Properties.RuleGenModelStrings.phrase);
            sb.Append("<span class=\"language\">");
            if (Type == PhraseType.source)
            {
                sb.Append(Properties.RuleGenModelStrings.src);
            }
            else
            {
                sb.Append(Properties.RuleGenModelStrings.tgt);
            }
            sb.Append("</span></span>\n");
            sb.Append("<ul>");
            foreach (Word word in Words)
            {
                sb.Append(word.ProduceHtml());
            }
            sb.Append("</ul>");
            sb.Append("</li>");
            return sb.ToString();
        }

        public RuleConstituentBase FindConstituent(int identifier)
        {
            RuleConstituentBase constituent = null;
            if (Identifier == identifier)
            {
                return this;
            }
            foreach (Word word in Words)
            {
                constituent = word.FindConstituent(identifier);
                if (constituent != null)
                {
                    return constituent;
                }
            }
            return constituent;
        }

        public Phrase Duplicate()
        {
            Phrase newPhrase = new Phrase();
            foreach (Word word in Words)
            {
                newPhrase.Words.Add(word.Duplicate());
            }
            return newPhrase;
        }
    }

    public enum PhraseType
    {
        source,
        target
    }
}
