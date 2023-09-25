// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.FLExTransRuleGenerator.Model
{
	public class Affix
	{
		public List<Feature> Features { get; set; } = new List<Feature>();
		public AffixType Type { get; set; } = AffixType.Suffix;

	}

	public enum AffixType
	{
		Prefix,
		Suffix
	}
}
