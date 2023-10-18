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

        public FeatureValueChooser()
        {
            InitializeComponent();
            this.Text = SIL.FLExTransRuleGen
                .Controller
                .Properties
                .RuleGenStrings
                .FeatureValueChooserTitle;
            CreateVariableValues();
            FillFeatureValuesListBox();
        }

        private void CreateVariableValues()
        {
            CreateVariableValue("α");
            CreateVariableValue("β");
            CreateVariableValue("ɣ");
            CreateVariableValue("ẟ");
            CreateVariableValue("ε");
            CreateVariableValue("ζ");
            CreateVariableValue("η");
            CreateVariableValue("θ");
            CreateVariableValue("ι");
        }

        private void CreateVariableValue(string abbr)
        {
            var variable = new FLExFeatureValue();
            variable.Abbreviation = abbr;
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
            foreach (FLExFeatureValue value in VariableFeatureValues)
            {
                lBoxFeatureValues.Items.Add(value);
            }
            lBoxFeatureValues.EndUpdate();
        }

        public void SelectFeatureValue(int index)
        {
            if (index > -1 && index < FeatureValues.Count)
            {
                lBoxFeatureValues.SelectedIndex = index;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedFeatureValue = (FLExFeatureValue)lBoxFeatureValues.SelectedItem;
        }
    }
}
