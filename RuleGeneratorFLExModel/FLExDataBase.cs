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

        public List<FLExCategory> Categories { get; set; } = new List<FLExCategory>();
        public List<FLExFeature> Features { get; set; } = new List<FLExFeature>();

        public FLExDataBase() { }

        public void SetFeatureInFeatureValues()
        {
            foreach (FLExFeature feat in Features)
            {
                foreach (FLExFeatureValue value in feat.Values)
                {
                    value.Feature = feat;
                }
            }
        }
    }
}
