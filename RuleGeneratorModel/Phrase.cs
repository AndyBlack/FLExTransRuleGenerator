﻿// Copyright (c) 2023 SIL International
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
    public class Phrase : RuleConstituent
    {
        public List<Word> Words { get; set; } = new List<Word>();

        [XmlIgnore]
        public PhraseType Type { get; set; } = PhraseType.source;

        public Phrase() { }

        public void DeleteWordAt(int index)
        {
            if (index < 0 || index >= Words.Count)
                return;
            Words.RemoveAt(index);
        }

        public void InsertNewWordAt(int index)
        {
            if (index < 0 || index >= Words.Count)
                return;
            var newWord = new Word();
            newWord.Id = (Words.Count + 1).ToString();
            Words.Insert(index, newWord);
        }

        public void InsertWordAt(Word word, int index)
        {
            if (index < 0 || index >= Words.Count)
                return;
            Words.Insert(index, word);
        }

        public void SwapPositionOfWords(int index, int otherIndex)
        {
            if (index < 0 | index >= Words.Count)
                return;
            if (otherIndex < 0 | otherIndex >= Words.Count)
                return;
            Word word = Words[index];
            Word otherWord = Words[otherIndex];
            Words[index] = otherWord;
            Words[otherIndex] = word;
        }

        public void MarkWordAsHead(Word word)
        {
            int index = Words.IndexOf(word);
            if (index < 0 || index >= Words.Count)
                return;
            foreach (Word w in Words)
            {
                if (w == word)
                {
                    w.Head = HeadValue.yes;
                }
                else
                {
                    w.Head = HeadValue.no;
                }
            }
        }

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

        public RuleConstituent FindConstituent(int identifier)
        {
            RuleConstituent constituent = null;
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
