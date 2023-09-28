// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.FLExTransRuleGen.Model
{
    public class Phrase
    {
        public List<Word> Words { get; set; } = new List<Word>();

        public Phrase() { }

        public string ProduceHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<li>");
            sb.Append("<span class=\"tf-nc\">");
            sb.Append(Properties.RuleGenModelStrings.phrase);
            sb.Append("<span class=\"language\">");
            // need way to know if it's src or tgt
            sb.Append(Properties.RuleGenModelStrings.src);
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
}
