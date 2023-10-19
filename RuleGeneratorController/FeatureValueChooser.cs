// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using SIL.FLExTransRuleGen.FLExModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIL.FLExTransRuleGenerator.Control
{
    public partial class FeatureValueChooser : Form
    {
        public List<FLExFeatureValue> FeatureValues { get; set; } = new List<FLExFeatureValue>();
        public List<FLExFeatureValue> VariableFeatureValues { get; set; } =
            new List<FLExFeatureValue>();
        public FLExFeatureValue SelectedFeatureValue { get; set; } = new FLExFeatureValue();
        public string Match { get; set; } = "";
        public int MaxVariables { get; set; } = 4;
        string[] variables = { "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "ι", "κ", "μ", "ν" };

        public FeatureValueChooser()
        {
            InitializeComponent();
            this.Text = SIL.FLExTransRuleGen
                .Controller
                .Properties
                .RuleGenStrings
                .FeatureValueChooserTitle;
            FillFeatureValuesListBox();
        }

        public void CreateVariableValues(FLExFeature feat)
        {
            for (int i = 0; i < MaxVariables && i < variables.Length; i++)
            {
                CreateVariableValue(feat, variables[i]);
            }
        }

        private void CreateVariableValue(FLExFeature feat, string abbr)
        {
            var variable = new FLExFeatureValue();
            variable.Abbreviation = abbr;
            variable.Feature = feat;
            VariableFeatureValues.Add(variable);
        }

        public void FillFeatureValuesListBox()
        {
            lBoxFeatureValues.BeginUpdate();
            lBoxFeatureValues.Items.Clear();
            foreach (FLExFeatureValue value in FeatureValues)
            {
                lBoxFeatureValues.Items.Add(value);
            }
            foreach (FLExFeatureValue varValue in VariableFeatureValues)
            {
                lBoxFeatureValues.Items.Add(varValue);
            }
            lBoxFeatureValues.EndUpdate();
        }

        public void FindAndSelectFeatureValuePair(string label, string match)
        {
            SelectFeatureValue(0);
            for (int i = 0; i < lBoxFeatureValues.Items.Count; i++)
            {
                FLExFeatureValue val = lBoxFeatureValues.Items[i] as FLExFeatureValue;
                if (val == null)
                    continue;
                if (val.Feature.Name == label && val.Abbreviation == match)
                {
                    SelectFeatureValue(i);
                    break;
                }
            }
        }

        public void SelectFeatureValue(int index)
        {
            if (index > -1 && index < lBoxFeatureValues.Items.Count)
            {
                lBoxFeatureValues.SelectedIndex = index;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedFeatureValue = (FLExFeatureValue)lBoxFeatureValues.SelectedItem;
            Match = SelectedFeatureValue.Abbreviation;
        }
    }
}
