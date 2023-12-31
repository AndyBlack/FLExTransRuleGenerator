﻿// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.FLExTransRuleGen.Model
{
    public class Source
    {
        public Phrase Phrase { get; set; } = new Phrase();

        public Source()
        {
            Phrase.Type = PhraseType.source;
        }

        public Source Duplicate()
        {
            Source newSource = new Source();
            newSource.Phrase = Phrase.Duplicate();
            return newSource;
        }
    }
}
