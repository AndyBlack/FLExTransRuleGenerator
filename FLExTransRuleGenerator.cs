// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.FLExTransRuleGenerator.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIL.FLExTransRuleGenerator
{
    public class FLExTransRuleGenerator
    {
        [STAThread]
        public static int Main(string[] args)
        {
            if (args.Length < 3)
            {
                WriteHelp();
                return 0;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine(Properties.GeneratorStrings.RuleFileNotFound);
                return 1;
            }

            if (!File.Exists(args[1]))
            {
                Console.WriteLine(Properties.GeneratorStrings.SourceFLExProjectNotFound);
                return 1;
            }

            if (!File.Exists(args[2]))
            {
                Console.WriteLine(Properties.GeneratorStrings.TargetFLExProjectNotFound);
                return 1;
            }

            var controller = new RuleGeneratorControl();
            controller.ShowDialog();
            return 0;
        }

        private static void WriteHelp()
        {
            Console.WriteLine(Properties.GeneratorStrings.HelpTitle);
            Console.WriteLine();
            Console.WriteLine(Properties.GeneratorStrings.CommandLineTemplate);
            Console.WriteLine();
            Console.WriteLine(Properties.GeneratorStrings.RuleFile);
            Console.WriteLine(Properties.GeneratorStrings.SourceProject);
            Console.WriteLine(Properties.GeneratorStrings.TargetProject);
        }
    }
}
