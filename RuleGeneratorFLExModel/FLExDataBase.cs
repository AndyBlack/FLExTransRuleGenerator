using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SIL.FLExTransRuleGen.FLExModel
{
    public abstract class FLExDataBase
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = "";

        public FLExDataBase() { }
    }
}
