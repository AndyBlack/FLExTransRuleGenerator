﻿// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.FLExTransRuleGenerator.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLExTransRuleGenerator
{
    class FLExTransRuleGenerator
    {
        [STAThread]
        static void Main(string[] args)
        {
            var controller = new RuleGeneratorControl();
            controller.ShowDialog();
        }
    }
}