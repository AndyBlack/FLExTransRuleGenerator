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
    public partial class FeatureChooser : Form
    {
        public List<FLExFeature> Features { get; set; } = new List<FLExFeature>();
        public FLExFeature SelectedFeature { get; set; } = new FLExFeature();
        public string Match { get; set; } = "";

        public FeatureChooser()
        {
            InitializeComponent();
            this.Text = SIL.FLExTransRuleGen
                .Controller
                .Properties
                .RuleGenStrings
                .FeatureChooserTitle;
            FillFeaturesListBox();
        }

        public void FillFeaturesListBox()
        {
            lBoxFeatures.BeginUpdate();
            lBoxFeatures.Items.Clear();
            foreach (FLExFeature feat in Features)
            {
                lBoxFeatures.Items.Add(feat);
            }
            lBoxFeatures.EndUpdate();
        }

        public void SelectFeature(int index)
        {
            if (index > -1 && index < Features.Count)
            {
                lBoxFeatures.SelectedIndex = index;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedFeature = (FLExFeature)lBoxFeatures.SelectedItem;
        }

        void lBoxFeatures_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lBoxFeatures.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                SelectedFeature = lBoxFeatures.SelectedItem as FLExFeature;
                if (SelectedFeature != null)
                {
                    using (FeatureValueChooser chooser = new FeatureValueChooser())
                    {
                        foreach (FLExFeatureValue feat in SelectedFeature.Values)
                        {
                            chooser.FeatureValues.Add(feat);
                        }
                        chooser.FillFeatureValuesListBox();
                        chooser.SelectFeatureValue(0);

                        var catFound = chooser.FeatureValues.FirstOrDefault(
                            cat => cat.Abbreviation == Match
                        );
                        index = chooser.FeatureValues.IndexOf(catFound);
                        if (index > -1)
                            chooser.SelectFeatureValue(index);
                        else
                            chooser.SelectFeatureValue(chooser.FeatureValues.Count);

                        chooser.ShowDialog();
                        if (chooser.DialogResult == DialogResult.OK)
                        {
                            FLExFeatureValue feat = chooser.SelectedFeatureValue;
                            Match = feat.Abbreviation;
                        }
                    }
                }
            }
        }
    }
}
