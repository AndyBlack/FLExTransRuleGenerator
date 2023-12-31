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
    public class Category : RuleConstituent
    {
        [XmlIgnore]
        public string Name { get; set; } = "";

        public Category(string name)
        {
            Name = name;
        }

        public RuleConstituent FindConstituent(int identifier)
        {
            RuleConstituent constituent = null;
            if (Identifier == identifier)
            {
                return this;
            }
            return constituent;
        }

        public string ProduceHtml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<li>");
            sb.Append(ProduceSpan("tf-nc category", "c"));
            sb.Append(Properties.RuleGenModelStrings.cat);
            sb.Append(":");
            sb.Append(Name);
            sb.Append("</span></li>\n");
            return sb.ToString();
        }

        public Category Duplicate()
        {
            Category newCat = new Category(Name);
            return newCat;
        }
    }
}
