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
            reader.Close();
        }

        public void SaveDataToFile(string FileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FLExTransRuleGenerator));
            //serializer.Serialize()
            using (TextWriter writer = new StreamWriter(FileName))
            {
                serializer.Serialize(writer, RuleGenerator);
            }
        }
    }
}
