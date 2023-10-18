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
    public partial class CategoryChooser : Form
    {
        public List<FLExCategory> Categories { get; set; } = new List<FLExCategory>();
        public FLExCategory SelectedCategory { get; set; } = new FLExCategory();

        public CategoryChooser()
        {
            InitializeComponent();
            FillCategoriesListBox();
        }

        public void FillCategoriesListBox()
        {
            lBoxCategories.BeginUpdate();
            lBoxCategories.Items.Clear();
            foreach (FLExCategory category in Categories)
            {
                lBoxCategories.Items.Add(category);
            }
            lBoxCategories.EndUpdate();
        }

        public void SelectCategory(int index)
        {
            if (index > -1 && index < Categories.Count)
            {
                lBoxCategories.SelectedIndex = index;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedCategory = (FLExCategory)lBoxCategories.SelectedItem;
        }
    }
}
