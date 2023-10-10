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
    public class RuleConstituent
    {
        [XmlIgnore]
        public int Identifier { get; set; }

        [XmlIgnore]
        public RuleConstituent Parent { get; set; }

        public RuleConstituent() { }

        protected string ProduceSpan(string sClass, string sType)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<span class=\"");
            sb.Append(sClass);
            sb.Append("\" id=\"");
            sb.Append(sType);
            sb.Append(".");
            sb.Append(Identifier);
            sb.Append("\" onclick=\"toApp('");
            sb.Append(sType);
            sb.Append(".");
            sb.Append(Identifier);
            sb.Append("')\">");
            return sb.ToString();
        }
    }
}
