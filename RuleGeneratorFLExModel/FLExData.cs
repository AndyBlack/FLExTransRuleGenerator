// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SIL.FLExTransRuleGen.FLExModel
{
    [XmlRoot("FLExData")]
    public class FLExData
    {
        public SourceFLExData SourceData { get; set; } = new SourceFLExData();
        public TargetFLExData TargetData { get; set; } = new TargetFLExData();

        public FLExData() { }
    }
}
