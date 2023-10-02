// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.FLExTransRuleGen.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

// Some code taken from https://www.codeproject.com/Articles/483055/XML-Serialization-and-Deserialization-Part-1
// and https://www.codeproject.com/Articles/487571/XML-Serialization-and-Deserialization-Part-2
// on November 1, 2022

namespace SIL.FLExTransRuleGen.Service
{
    public class XmlBackEndProvider
    {
        public FLExTransRuleGenerator RuleGenerator { get; set; }

        public void LoadDataFromFile(string FileName)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(FLExTransRuleGenerator));
            TextReader reader = new StreamReader(FileName);
            object obj = deserializer.Deserialize(reader);
            RuleGenerator = (FLExTransRuleGenerator)obj;
            // Not sure we need this, but otherwise the target phrase type is set to source
            foreach (FLExTransRule rule in RuleGenerator.FLExTransRules)
            {
                rule.Target.Phrase.Type = PhraseType.target;
            }
            reader.Close();
        }

        public void SaveDataToFile(string FileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FLExTransRuleGenerator));
            using (TextWriter writer = new StreamWriter(FileName))
            {
                serializer.Serialize(writer, RuleGenerator);
            }
            // Maybe there's a better way, but this hack inserts the DOCTYPE in the right spot.
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            sb.Append(
                "<!DOCTYPE FLExTransRuleGenerator PUBLIC \" -//XMLmind//DTD FLExTransRuleGenerator//EN\"\n"
            );
            sb.Append("\"FLExTransRuleGenerator.dtd\">\n");
            string result = File.ReadAllText(FileName);
            //int i = result.IndexOf("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\"");
            sb.Append("<FLExTransRuleGenerator>\n");
            sb.Append(result.Substring(165));
            File.WriteAllText(FileName, sb.ToString(), Encoding.UTF8);
        }
    }
}
