// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

using Microsoft.Web.WebView2.Core;
using Microsoft.Win32;
using SIL.FLExTransRuleGen.Model;
using SIL.FLExTransRuleGen.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIL.FLExTransRuleGenerator.Control
{
    public partial class RuleGeneratorControl : Form
    {
        const string kAppNAme = "FLExTransRuleGenerator";
        string flexTransDir = "";
        protected bool ChangesMade { get; set; } = false;
        public FLExTransRuleGen.Model.FLExTransRuleGenerator FLExTransRuleGen { get; set; }
        protected FLExTransRule SelectedRule { get; set; }
        public int LastSelectedRule { get; set; }
        protected WebPageProducer producer;
        protected ConstituentFinder finder;
        protected Affix affix;
        protected Category category;
        protected Feature feature;
        protected Phrase phrase;
        protected Word word;

        protected ContextMenuStrip ruleEditContextMenu;
        protected ContextMenuStrip affixEditContextMenu;
        protected ContextMenuStrip categoryEditContextMenu;
        protected ContextMenuStrip featureEditContextMenu;
        protected ContextMenuStrip wordEditContextMenu;

        protected string cmDelete = "Delete";
        protected string cmDuplicate = "Duplicate";
        protected string cmEdit = "Edit";
        protected string cmInsertAfter = "Insert new after";
        protected string cmInsertBefore = "Insert new before";
        protected string cmInsertCategory = "Insert category";
        protected string cmInsertFeature = "Insert feature";
        protected string cmMoveDown = "Move down";
        protected string cmMoveLeft = "Move left";
        protected string cmMoveRight = "Move right";
        protected string cmMoveUp = "Move up";

        protected RegistryKey regkey;
        const string RegKey = "Software\\SIL\\FLExTransRuleGenerator";
        protected const string m_strLastRule = "LastRule";
        protected const string m_strLocationX = "LocationX";
        protected const string m_strLocationY = "LocationY";
        protected const string m_strSizeHeight = "SizeHeight";
        protected const string m_strSizeWidth = "SizeWidth";
        protected const string m_strWindowState = "WindowState";
        public Rectangle RectNormal { get; set; }

        public RuleGeneratorControl()
        {
            InitializeComponent();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(
                this.OnFormClosing
            );
            BuildRuleContextMenu();
            BuildAffixContextMenu();
            BuildCategoryContextMenu();
            BuildFeatureContextMenu();
            BuildWordContextMenu();
            CheckForPresenceInProgramData();
            SetLocalizationStrings();
            regkey = Registry.CurrentUser.OpenSubKey(RegKey);
            RetrieveRegistryInfo();
            producer = WebPageProducer.Instance;
            finder = ConstituentFinder.Instance;
            wv2RuleEditor.WebMessageReceived += webView2_WebMessageReceived;
            wv2RuleEditor.CoreWebView2InitializationCompleted +=
                webview2_CoreWebView2InitializationCompleted;
            ShowWebPage();
        }

        protected void RememberFormState(string sRegKey)
        {
            regkey = Registry.CurrentUser.OpenSubKey(sRegKey);
            if (regkey != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                RetrieveRegistryInfo();
                regkey.Close();
                DesktopBounds = RectNormal;
                WindowState = WindowState;
                StartPosition = FormStartPosition.Manual;
                Cursor.Current = Cursors.Default;
            }
        }

        protected void RetrieveRegistryInfo()
        {
            if (regkey != null)
            {
                // Window location
                int iX = (int)regkey.GetValue(m_strLocationX, 100);
                int iY = (int)regkey.GetValue(m_strLocationY, 100);
                int iWidth = (int)regkey.GetValue(m_strSizeWidth, 863); // 1228);
                int iHeight = (int)regkey.GetValue(m_strSizeHeight, 670); // 947);
                RectNormal = new Rectangle(iX, iY, iWidth, iHeight);
                // Set form properties
                WindowState = (FormWindowState)regkey.GetValue(m_strWindowState, 0);

                LastSelectedRule = (int)regkey.GetValue(m_strLastRule, 0);
            }
        }

        public void SaveRegistryInfo(string sRegKey)
        {
            regkey = Registry.CurrentUser.OpenSubKey(sRegKey, true);
            if (regkey == null)
            {
                regkey = Registry.CurrentUser.CreateSubKey(sRegKey);
            }

            regkey.SetValue(m_strLastRule, LastSelectedRule);
            // Window position and location
            regkey.SetValue(m_strWindowState, (int)WindowState);
            regkey.SetValue(m_strLocationX, RectNormal.X);
            regkey.SetValue(m_strLocationY, RectNormal.Y);
            regkey.SetValue(m_strSizeWidth, RectNormal.Width);
            regkey.SetValue(m_strSizeHeight, RectNormal.Height);
            regkey.Close();
        }

        protected override void OnMove(EventArgs ea)
        {
            base.OnMove(ea);

            if (WindowState == FormWindowState.Normal)
                RectNormal = DesktopBounds;
        }

        protected override void OnResize(EventArgs ea)
        {
            base.OnResize(ea);

            if (WindowState == FormWindowState.Normal)
                RectNormal = DesktopBounds;
        }

        public void ShowWebPage()
        {
            if (SelectedRule != null)
            {
                string html = producer.ProduceWebPage(SelectedRule);
                wv2RuleEditor.Source = GetUriOfWebPage();
                string path = wv2RuleEditor.Source.AbsolutePath;
                if (File.Exists(wv2RuleEditor.Source.AbsolutePath))
                {
                    File.WriteAllText(path, html, Encoding.UTF8);
                    // Apparently we need to set the Source to something else or it just uses the same as last time
                    wv2RuleEditor.Source = new Uri("local://abc");
                    wv2RuleEditor.Source = GetUriOfWebPage();
                    wv2RuleEditor.Show();
                }
            }
        }

        private void CheckForPresenceInProgramData()
        {
            var programData = Environment.GetFolderPath(
                Environment.SpecialFolder.CommonApplicationData
            );
            flexTransDir = Path.Combine(programData, "SIL", "FLExTransRuleGenerator");
            if (!Directory.Exists(flexTransDir))
            {
                string testDataDir = GetTestDataDirectory();
                Copy(testDataDir, flexTransDir);
            }
        }

        private void SetLocalizationStrings()
        {
            cmDelete = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.cmDelete;
            cmEdit = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.cmEdit;
            cmDuplicate = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.cmDuplicate;
            cmInsertAfter = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.cmInsertAfter;
            cmInsertBefore = SIL.FLExTransRuleGen
                .Controller
                .Properties
                .RuleGenStrings
                .cmInsertBefore;
            cmInsertCategory = SIL.FLExTransRuleGen
                .Controller
                .Properties
                .RuleGenStrings
                .cmInsertCategory;
            cmInsertFeature = SIL.FLExTransRuleGen
                .Controller
                .Properties
                .RuleGenStrings
                .cmInsertFeature;
            cmMoveDown = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.cmMoveDown;
            cmMoveLeft = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.cmMoveLeft;
            cmMoveRight = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.cmMoveRight;
            cmMoveUp = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.cmMoveUp;
            lblName.Text = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.RuleName;
            this.Text = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.FormTitle;
        }

        private async void webview2_CoreWebView2InitializationCompleted(
            object sender,
            CoreWebView2InitializationCompletedEventArgs e
        )
        {
            wv2RuleEditor.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
        }

        private void webView2_WebMessageReceived(
            object sender,
            Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs args
        )
        {
            string message = args.TryGetWebMessageAsString();
            int identifier = Int32.Parse(message.Substring(2));
            RuleConstituent constituent = finder.FindConstituent(SelectedRule, identifier);
            switch (constituent.ToString())
            {
                case "SIL.FLExTransRuleGen.Model.Affix":
                    affix = constituent as Affix;
                    affixEditContextMenu.Show(Cursor.Position);
                    break;
                case "SIL.FLExTransRuleGen.Model.Category":
                    category = constituent as Category;
                    categoryEditContextMenu.Show(Cursor.Position);
                    break;
                case "SIL.FLExTransRuleGen.Model.Feature":
                    feature = constituent as Feature;
                    featureEditContextMenu.Show(Cursor.Position);
                    break;
                case "SIL.FLExTransRuleGen.Model.Phrase":
                    phrase = constituent as Phrase;
                    // do we need anything here?  I think not
                    break;
                case "SIL.FLExTransRuleGen.Model.Word":
                    wordEditContextMenu.Show(Cursor.Position);
                    word = constituent as Word;
                    AdjustWordContextMenuContent();
                    break;
            }
        }

        private Uri GetUriOfWebPage()
        {
            string webfile = Path.Combine(flexTransDir, "FLExTransRule.html");
            Uri uri = new Uri(webfile);
            return uri;
        }

        private static string GetTestDataDirectory()
        {
            Uri uriBase = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var rootdir = Path.GetDirectoryName(Uri.UnescapeDataString(uriBase.AbsolutePath));
            int i = rootdir.LastIndexOf(kAppNAme);
            string basedir = rootdir.Substring(0, i);
            string testDataDir = Path.Combine(basedir, kAppNAme, "TestData");
            return testDataDir;
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        // Following code taken from https://code.4noobz.net/c-copy-a-folder-its-content-and-the-subfolders/
        // on 2023.09.28
        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        // ***************************

        public void FillRulesListBox()
        {
            lBoxRules.Items.Clear();
            foreach (FLExTransRule op in FLExTransRuleGen.FLExTransRules)
            {
                lBoxRules.Items.Add(op);
            }
            if (FLExTransRuleGen.FLExTransRules.Count > 0)
            {
                // select last used operation, if any
                if (
                    LastSelectedRule < 0
                    || LastSelectedRule >= FLExTransRuleGen.FLExTransRules.Count
                )
                    LastSelectedRule = 0;
                lBoxRules.SetSelected(LastSelectedRule, true);
                SelectedRule = FLExTransRuleGen.FLExTransRules[LastSelectedRule];
            }
        }

        protected void BuildRuleContextMenu()
        {
            ruleEditContextMenu = new ContextMenuStrip();
            ruleEditContextMenu.Name = "Rules";
            ToolStripMenuItem ruleInsertBefore = new ToolStripMenuItem(cmInsertBefore);
            ruleInsertBefore.Click += new EventHandler(RuleInsertBeforeContextMenu_Click);
            ruleInsertBefore.Name = cmInsertBefore;
            ToolStripMenuItem ruleInsertAfter = new ToolStripMenuItem(cmInsertAfter);
            ruleInsertAfter.Click += new EventHandler(RuleInsertAfterContextMenu_Click);
            ruleInsertAfter.Name = cmInsertAfter;
            ToolStripMenuItem ruleMoveUp = new ToolStripMenuItem(cmMoveUp);
            ruleMoveUp.Click += new EventHandler(RuleMoveUpContextMenu_Click);
            ruleMoveUp.Name = cmMoveUp;
            ToolStripMenuItem ruleMoveDown = new ToolStripMenuItem(cmMoveDown);
            ruleMoveDown.Click += new EventHandler(RuleMoveDownContextMenu_Click);
            ruleMoveDown.Name = cmMoveDown;
            ToolStripMenuItem ruleDeleteItem = new ToolStripMenuItem(cmDelete);
            ruleDeleteItem.Click += new EventHandler(RuleDeleteContextMenu_Click);
            ruleDeleteItem.Name = cmDelete;
            ToolStripMenuItem ruleDuplicateItem = new ToolStripMenuItem(cmDuplicate);
            ruleDuplicateItem.Click += new EventHandler(RuleDuplicateContextMenu_Click);
            ruleDuplicateItem.Name = cmDuplicate;
            ruleEditContextMenu.Items.Add(ruleDuplicateItem);
            ruleEditContextMenu.Items.Add(ruleInsertBefore);
            ruleEditContextMenu.Items.Add(ruleInsertAfter);
            ruleEditContextMenu.Items.Add("-");
            ruleEditContextMenu.Items.Add(ruleMoveUp);
            ruleEditContextMenu.Items.Add(ruleMoveDown);
            ruleEditContextMenu.Items.Add("-");
            ruleEditContextMenu.Items.Add(ruleDeleteItem);
        }

        protected void BuildAffixContextMenu()
        {
            affixEditContextMenu = new ContextMenuStrip();
            affixEditContextMenu.Name = "Affix";
            ToolStripMenuItem affixInsertBefore = new ToolStripMenuItem(cmInsertBefore);
            affixInsertBefore.Click += new EventHandler(AffixInsertBeforeContextMenu_Click);
            affixInsertBefore.Name = cmInsertBefore;
            ToolStripMenuItem affixInsertAfter = new ToolStripMenuItem(cmInsertAfter);
            affixInsertAfter.Click += new EventHandler(AffixInsertAfterContextMenu_Click);
            affixInsertAfter.Name = cmInsertAfter;
            ToolStripMenuItem affixInsertFeature = new ToolStripMenuItem(cmInsertFeature);
            affixInsertFeature.Click += new EventHandler(AffixInsertFeatureContextMenu_Click);
            affixInsertFeature.Name = cmInsertFeature;
            ToolStripMenuItem affixDeleteItem = new ToolStripMenuItem(cmDelete);
            affixDeleteItem.Click += new EventHandler(AffixDeleteContextMenu_Click);
            affixDeleteItem.Name = cmDelete;
            ToolStripMenuItem affixDuplicateItem = new ToolStripMenuItem(cmDuplicate);
            affixDuplicateItem.Click += new EventHandler(AffixDuplicateContextMenu_Click);
            affixDuplicateItem.Name = cmDuplicate;
            affixEditContextMenu.Items.Add(affixDuplicateItem);
            affixEditContextMenu.Items.Add(affixInsertBefore);
            affixEditContextMenu.Items.Add(affixInsertAfter);
            affixEditContextMenu.Items.Add("-");
            affixEditContextMenu.Items.Add(affixDeleteItem);
            affixEditContextMenu.Items.Add("-");
            affixEditContextMenu.Items.Add(affixInsertFeature);
        }

        protected void BuildCategoryContextMenu()
        {
            categoryEditContextMenu = new ContextMenuStrip();
            categoryEditContextMenu.Name = "Category";
            ToolStripMenuItem categoryDeleteItem = new ToolStripMenuItem(cmDelete);
            categoryDeleteItem.Click += new EventHandler(CategoryDeleteContextMenu_Click);
            categoryDeleteItem.Name = cmDelete;
            ToolStripMenuItem categoryEditItem = new ToolStripMenuItem(cmEdit);
            categoryEditItem.Click += new EventHandler(CategoryEditContextMenu_Click);
            categoryEditItem.Name = cmEdit;
            categoryEditContextMenu.Items.Add(categoryEditItem);
            categoryEditContextMenu.Items.Add("-");
            categoryEditContextMenu.Items.Add(categoryDeleteItem);
        }

        protected void BuildFeatureContextMenu()
        {
            featureEditContextMenu = new ContextMenuStrip();
            featureEditContextMenu.Name = "Feature";
            ToolStripMenuItem featureDeleteItem = new ToolStripMenuItem(cmDelete);
            featureDeleteItem.Click += new EventHandler(FeatureDeleteContextMenu_Click);
            featureDeleteItem.Name = cmDelete;
            ToolStripMenuItem featureEditItem = new ToolStripMenuItem(cmEdit);
            featureEditItem.Click += new EventHandler(FeatureEditContextMenu_Click);
            featureEditItem.Name = cmEdit;
            featureEditContextMenu.Items.Add(featureEditItem);
            featureEditContextMenu.Items.Add("-");
            featureEditContextMenu.Items.Add(featureDeleteItem);
        }

        protected void BuildWordContextMenu()
        {
            wordEditContextMenu = new ContextMenuStrip();
            wordEditContextMenu.Name = "Word";
            ToolStripMenuItem wordInsertBefore = new ToolStripMenuItem(cmInsertBefore);
            wordInsertBefore.Click += new EventHandler(WordInsertBeforeContextMenu_Click);
            wordInsertBefore.Name = cmInsertBefore;
            ToolStripMenuItem wordInsertAfter = new ToolStripMenuItem(cmInsertAfter);
            wordInsertAfter.Click += new EventHandler(WordInsertAfterContextMenu_Click);
            wordInsertAfter.Name = cmInsertAfter;
            ToolStripMenuItem wordInsertCategory = new ToolStripMenuItem(cmInsertCategory);
            wordInsertCategory.Click += new EventHandler(WordInsertCategoryContextMenu_Click);
            wordInsertCategory.Name = cmInsertCategory;
            ToolStripMenuItem wordDeleteItem = new ToolStripMenuItem(cmDelete);
            wordDeleteItem.Click += new EventHandler(WordDeleteContextMenu_Click);
            wordDeleteItem.Name = cmDelete;
            ToolStripMenuItem wordDuplicateItem = new ToolStripMenuItem(cmDuplicate);
            wordDuplicateItem.Click += new EventHandler(WordDuplicateContextMenu_Click);
            wordDuplicateItem.Name = cmDuplicate;
            ToolStripMenuItem wordMoveLeft = new ToolStripMenuItem(cmMoveLeft);
            wordMoveLeft.Click += new EventHandler(WordMoveLeftContextMenu_Click);
            wordMoveLeft.Name = cmMoveLeft;
            ToolStripMenuItem wordMoveRight = new ToolStripMenuItem(cmMoveRight);
            wordMoveRight.Click += new EventHandler(WordMoveRightContextMenu_Click);
            wordMoveRight.Name = cmMoveRight;
            wordEditContextMenu.Items.Add(wordDuplicateItem);
            wordEditContextMenu.Items.Add(wordInsertBefore);
            wordEditContextMenu.Items.Add(wordInsertAfter);
            wordEditContextMenu.Items.Add("-");
            wordEditContextMenu.Items.Add(wordMoveLeft);
            wordEditContextMenu.Items.Add(wordMoveRight);
            wordEditContextMenu.Items.Add("-");
            wordEditContextMenu.Items.Add(wordDeleteItem);
            wordEditContextMenu.Items.Add("-");
            wordEditContextMenu.Items.Add(wordInsertCategory);
        }

        protected void AdjustRuleContextMenuContent(ListBox lBoxSender, int indexAtMouse)
        {
            int indexLast = lBoxSender.Items.Count - 1;
            if (indexAtMouse == 0)
                // move up does not work
                ruleEditContextMenu.Items[4].Enabled = false;
            else
                ruleEditContextMenu.Items[4].Enabled = true;
            if (indexAtMouse == 0 && indexLast == 0)
                // delete does not work
                ruleEditContextMenu.Items[7].Enabled = false;
            else
                ruleEditContextMenu.Items[7].Enabled = true;
            if (indexAtMouse == indexLast)
                // move down does not work
                ruleEditContextMenu.Items[5].Enabled = false;
            else
                ruleEditContextMenu.Items[5].Enabled = true;
        }

        protected void AdjustWordContextMenuContent()
        {
            int index = GetIndexOfWordInPhrase();
            if (index > -1)
            {
                int moveLeftIndex = wordEditContextMenu.Items.IndexOfKey(cmMoveLeft);
                int moveRightIndex = wordEditContextMenu.Items.IndexOfKey(cmMoveRight);
                int deleteIndex = wordEditContextMenu.Items.IndexOfKey(cmDelete);
                int indexLast = phrase.Words.Count - 1;
                if (index == 0)
                    wordEditContextMenu.Items[moveLeftIndex].Enabled = false;
                else
                    wordEditContextMenu.Items[moveLeftIndex].Enabled = true;
                moveLeftIndex = wordEditContextMenu.Items.IndexOfKey(cmDelete);
                if (index == 0 && indexLast == 0)
                    wordEditContextMenu.Items[moveLeftIndex].Enabled = false;
                else
                    wordEditContextMenu.Items[moveLeftIndex].Enabled = true;
                if (index == indexLast)
                    // move down does not work
                    wordEditContextMenu.Items[moveRightIndex].Enabled = false;
                else
                    wordEditContextMenu.Items[moveRightIndex].Enabled = true;
            }
        }

        protected void RuleInsertBeforeContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertBefore)
            {
                DoRuleContextMenuInsert(lBoxRules.SelectedIndex);
            }
        }

        protected void RuleInsertAfterContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertAfter)
            {
                DoRuleContextMenuInsert(lBoxRules.SelectedIndex + 1);
            }
        }

        protected void DoRuleContextMenuInsert(int index)
        {
            FLExTransRule ftRule = new FLExTransRule();
            FLExTransRuleGen.FLExTransRules.Insert(index, ftRule);
            lBoxRules.Items.Insert(index, ftRule);
            lBoxRules.SetSelected(index, true);
            MarkAsChanged(true);
        }

        protected void RuleMoveUpContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmMoveUp)
            {
                int index = lBoxRules.SelectedIndex;
                DoRuleContextMenuMove(index, index - 1);
            }
        }

        protected void RuleMoveDownContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmMoveDown)
            {
                int index = lBoxRules.SelectedIndex;
                DoRuleContextMenuMove(index, index + 1);
            }
        }

        protected void DoRuleContextMenuMove(int index, int otherIndex)
        {
            Object selectedItem = lBoxRules.SelectedItem;
            Object otherItem = lBoxRules.Items[otherIndex];
            FLExTransRuleGen.FLExTransRules[index] = (FLExTransRule)otherItem;
            FLExTransRuleGen.FLExTransRules[otherIndex] = (FLExTransRule)selectedItem;
            lBoxRules.Items[index] = otherItem;
            lBoxRules.Items[otherIndex] = selectedItem;
            lBoxRules.SelectedIndex = otherIndex;
            MarkAsChanged(true);
        }

        protected void RuleDeleteContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmDelete)
            {
                int index = lBoxRules.SelectedIndex;
                FLExTransRule op = FLExTransRuleGen.FLExTransRules.ElementAt(index);
                FLExTransRuleGen.FLExTransRules.RemoveAt(index);
                lBoxRules.Items.RemoveAt(index);
                int newIndex = index < lBoxRules.Items.Count ? index : lBoxRules.Items.Count - 1;
                if (newIndex > -1)
                    lBoxRules.SelectedIndex = newIndex;
            }
            MarkAsChanged(true);
        }

        protected void RuleDuplicateContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmDuplicate)
            {
                int index = lBoxRules.SelectedIndex + 1;
                {
                    FLExTransRule ftRule = SelectedRule.Duplicate();
                    FLExTransRuleGen.FLExTransRules.Insert(index, ftRule);
                    lBoxRules.Items.Insert(index, ftRule);
                }
            }
            MarkAsChanged(true);
        }

        protected void AffixInsertBeforeContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertBefore)
            {
                DoAffixContextMenuInsert( /* lBoxAffixs.SelectedIndex*/
                    0
                );
                MessageBox.Show("affix insert before found");
            }
        }

        protected void AffixInsertAfterContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertAfter)
            {
                DoAffixContextMenuInsert( /*lBoxAffixs.SelectedIndex +*/
                    1
                );
                MessageBox.Show("affix insert after found");
            }
        }

        protected void AffixInsertFeatureContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertFeature)
            {
                DoAffixContextMenuInsert( /*lBoxAffixs.SelectedIndex +*/
                    1
                );
                MessageBox.Show("affix insert feature found");
            }
        }

        protected void DoAffixContextMenuInsert(int index)
        {
            //FLExTransAffix ftAffix = new FLExTransAffix();
            //FLExTransAffixGen.FLExTransAffixs.Insert(index, ftAffix);
            //lBoxAffixs.Items.Insert(index, ftAffix);
            //lBoxAffixs.SetSelected(index, true);
            MarkAsChanged(true);
        }

        protected void AffixDeleteContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmDelete)
            {
                MessageBox.Show("affix delete found");

                //int index = lBoxAffixs.SelectedIndex;
                //FLExTransAffix op = FLExTransAffixGen.FLExTransAffixs.ElementAt(index);
                //FLExTransAffixGen.FLExTransAffixs.RemoveAt(index);
                //lBoxAffixs.Items.RemoveAt(index);
                //int newIndex = index < lBoxAffixs.Items.Count ? index : lBoxAffixs.Items.Count - 1;
                //if (newIndex > -1)
                //	lBoxAffixs.SelectedIndex = newIndex;
            }
            MarkAsChanged(true);
        }

        protected void AffixDuplicateContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmDuplicate)
            {
                MessageBox.Show("affix duplicate found");

                //int index = lBoxAffixs.SelectedIndex + 1;
                //{
                //	FLExTransAffix ftAffix = SelectedAffix.Duplicate();
                //	FLExTransAffixGen.FLExTransAffixs.Insert(index, ftAffix);
                //	lBoxAffixs.Items.Insert(index, ftAffix);
                //}
            }
            MarkAsChanged(true);
        }

        protected void CategoryDeleteContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmDelete)
            {
                word = category.Parent as Word;
                if (word != null)
                {
                    word.DeleteCategory();
                    ShowWebPage();
                    MarkAsChanged(true);
                }
            }
            MarkAsChanged(true);
        }

        protected void CategoryEditContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmEdit)
            {
                MessageBox.Show("category edit found");

                //int index = lBoxCategorys.SelectedIndex + 1;
                //{
                //	FLExTransCategory ftCategory = SelectedCategory.Duplicate();
                //	FLExTransCategoryGen.FLExTransCategorys.Insert(index, ftCategory);
                //	lBoxCategorys.Items.Insert(index, ftCategory);
                //}
            }
            MarkAsChanged(true);
        }

        protected void FeatureDeleteContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmDelete)
            {
                MessageBox.Show("feature delete found");

                //int index = lBoxFeatures.SelectedIndex;
                //FLExTransFeature op = FLExTransFeatureGen.FLExTransFeatures.ElementAt(index);
                //FLExTransFeatureGen.FLExTransFeatures.RemoveAt(index);
                //lBoxFeatures.Items.RemoveAt(index);
                //int newIndex = index < lBoxFeatures.Items.Count ? index : lBoxFeatures.Items.Count - 1;
                //if (newIndex > -1)
                //	lBoxFeatures.SelectedIndex = newIndex;
            }
            MarkAsChanged(true);
        }

        protected void FeatureEditContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmEdit)
            {
                MessageBox.Show("feature edit found");

                //int index = lBoxFeatures.SelectedIndex + 1;
                //{
                //	FLExTransFeature ftFeature = SelectedFeature.Duplicate();
                //	FLExTransFeatureGen.FLExTransFeatures.Insert(index, ftFeature);
                //	lBoxFeatures.Items.Insert(index, ftFeature);
                //}
            }
            MarkAsChanged(true);
        }

        protected void WordInsertBeforeContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertBefore)
            {
                DoWordContextMenuInsert(true);
            }
        }

        protected void WordInsertAfterContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertAfter)
            {
                DoWordContextMenuInsert(false);
            }
        }

        protected void DoWordContextMenuInsert(bool before)
        {
            int index = GetIndexOfWordInPhrase();
            if (index < 0)
                return; // did not find it
            if (before)
            {
                phrase.InsertNewWordAt(index);
            }
            else
            {
                phrase.InsertNewWordAt(index + 1);
            }
            ShowWebPage();
            MarkAsChanged(true);
        }

        protected int GetIndexOfWordInPhrase()
        {
            int index = -1;
            if (word != null)
            {
                phrase = word.Parent as Phrase;
                if (phrase != null)
                {
                    index = phrase.Words.IndexOf(word);
                }
            }
            return index;
        }

        protected void WordMoveLeftContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmMoveLeft)
            {
                int index = GetIndexOfWordInPhrase();
                if (index > -1)
                {
                    phrase.SwapPositionOfWords(index, index - 1);
                    ShowWebPage();
                    MarkAsChanged(true);
                }
            }
        }

        protected void WordMoveRightContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmMoveRight)
            {
                int index = GetIndexOfWordInPhrase();
                if (index > -1)
                {
                    phrase.SwapPositionOfWords(index, index + 1);
                    ShowWebPage();
                    MarkAsChanged(true);
                }
            }
        }

        protected void WordInsertCategoryContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertCategory)
            {
                MessageBox.Show("show categories available dialog");
                //word.CategoryConstituent
            }
        }

        protected void WordDeleteContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmDelete)
            {
                int index = GetIndexOfWordInPhrase();
                if (index > -1)
                {
                    phrase.DeleteWordAt(index);
                    ShowWebPage();
                    MarkAsChanged(true);
                }
            }
        }

        protected void WordDuplicateContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmDuplicate)
            {
                int index = GetIndexOfWordInPhrase();
                if (index > -1)
                {
                    Word newWord = word.Duplicate();
                    newWord.Id = (phrase.Words.Count + 1).ToString();
                    phrase.InsertWordAt(newWord, index + 1);
                    ShowWebPage();
                    MarkAsChanged(true);
                }
            }
        }

        protected void lBoxRules_MouseUp(object sender, MouseEventArgs e)
        {
            HandleContextMenu(sender, e);
        }

        protected void HandleContextMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListBox lBoxSender = (ListBox)sender;
                int indexAtMouse = lBoxSender.IndexFromPoint(e.X, e.Y);
                if (indexAtMouse > -1)
                {
                    AdjustRuleContextMenuContent(lBoxSender, indexAtMouse);
                    lBoxSender.SelectedIndex = indexAtMouse;
                    Point ptClickedAt = e.Location;
                    ptClickedAt = lBoxSender.PointToScreen(ptClickedAt);
                    ruleEditContextMenu.Show(ptClickedAt);
                }
            }
        }

        protected virtual void lBoxRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedRule = lBoxRules.SelectedItem as FLExTransRule;
            if (SelectedRule != null)
            {
                LastSelectedRule = lBoxRules.SelectedIndex;
                tbName.Text = SelectedRule.Name;
                int index = lBoxRules.SelectedIndex + 1;
                ShowWebPage();
                //lbCountOps.Text = index.ToString() + " / " + AlloGens.SelectedRules.Count.ToString();
            }
        }

        protected void MarkAsChanged(bool value)
        {
            ChangesMade = value;
            ShowChangeStatusOnForm();
        }

        protected void ShowChangeStatusOnForm()
        {
            this.Text = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.FormTitle;
            if (ChangesMade)
                this.Text += "*";
        }

        // **************
        protected void OnFormClosing(object sender, EventArgs e)
        {
            //SaveAnyChanges();
            SaveRegistryInfo(RegKey);
        }
    }
}
