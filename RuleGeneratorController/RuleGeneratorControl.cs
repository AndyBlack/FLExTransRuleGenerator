// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

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

        protected ContextMenuStrip editContextMenu;
        protected string cmAdd = "Add";
        protected string cmInsertBefore = "Insert new before";
        protected string cmInsertAfter = "Insert new after";
        protected string cmMoveUp = "Move up";
        protected string cmMoveDown = "Move down";
        protected string cmDelete = "Delete";
        protected string cmDuplicate = "Duplicate";

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
            BuildContextMenu();
            CheckForPresenceInProgramData();
            SetLocalizationStrings();
            regkey = Registry.CurrentUser.OpenSubKey(RegKey);
            RetrieveRegistryInfo();
            producer = WebPageProducer.Instance;
            wv2RuleEditor.WebMessageReceived += webView2_WebMessageReceived;
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
            cmDuplicate = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.cmDuplicate;
            cmInsertAfter = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.cmInsertAfter;
            cmInsertBefore = SIL.FLExTransRuleGen
                .Controller
                .Properties
                .RuleGenStrings
                .cmInsertBefore;
            cmMoveDown = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.cmMoveDown;
            cmMoveUp = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.cmMoveUp;
            lblName.Text = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.RuleName;
            this.Text = SIL.FLExTransRuleGen.Controller.Properties.RuleGenStrings.FormTitle;
        }

        private void webView2_WebMessageReceived(
            object sender,
            Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs args
        )
        {
            string message = args.TryGetWebMessageAsString();
            MessageBox.Show(message);
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

        protected void BuildContextMenu()
        {
            editContextMenu = new ContextMenuStrip();
            editContextMenu.Name = "Rules";
            ToolStripMenuItem insertBefore = new ToolStripMenuItem(cmInsertBefore);
            insertBefore.Click += new EventHandler(InsertBeforeContextMenu_Click);
            insertBefore.Name = cmInsertBefore;
            ToolStripMenuItem insertAfter = new ToolStripMenuItem(cmInsertAfter);
            insertAfter.Click += new EventHandler(InsertAfterContextMenu_Click);
            insertAfter.Name = cmInsertAfter;
            ToolStripMenuItem moveUp = new ToolStripMenuItem(cmMoveUp);
            moveUp.Click += new EventHandler(MoveUpContextMenu_Click);
            moveUp.Name = cmMoveUp;
            ToolStripMenuItem moveDown = new ToolStripMenuItem(cmMoveDown);
            moveDown.Click += new EventHandler(MoveDownContextMenu_Click);
            moveDown.Name = cmMoveDown;
            ToolStripMenuItem deleteItem = new ToolStripMenuItem(cmDelete);
            deleteItem.Click += new EventHandler(DeleteContextMenu_Click);
            deleteItem.Name = cmDelete;
            ToolStripMenuItem duplicateItem = new ToolStripMenuItem(cmDuplicate);
            duplicateItem.Click += new EventHandler(DuplicateContextMenu_Click);
            duplicateItem.Name = cmDuplicate;
            editContextMenu.Items.Add(duplicateItem);
            editContextMenu.Items.Add(insertBefore);
            editContextMenu.Items.Add(insertAfter);
            editContextMenu.Items.Add("-");
            editContextMenu.Items.Add(moveUp);
            editContextMenu.Items.Add(moveDown);
            editContextMenu.Items.Add("-");
            editContextMenu.Items.Add(deleteItem);
        }

        protected void AdjustContextMenuContent(ListBox lBoxSender, int indexAtMouse)
        {
            int indexLast = lBoxSender.Items.Count - 1;
            if (indexAtMouse == 0)
                // move up does not work
                editContextMenu.Items[4].Enabled = false;
            else
                editContextMenu.Items[4].Enabled = true;
            if (indexAtMouse == 0 && indexLast == 0)
                // delete does not work
                editContextMenu.Items[7].Enabled = false;
            else
                editContextMenu.Items[7].Enabled = true;
            if (indexAtMouse == indexLast)
                // move down does not work
                editContextMenu.Items[5].Enabled = false;
            else
                editContextMenu.Items[5].Enabled = true;
        }

        protected void InsertBeforeContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertBefore)
            {
                DoContextMenuInsert(lBoxRules.SelectedIndex);
            }
        }

        protected void InsertAfterContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmInsertAfter)
            {
                DoContextMenuInsert(lBoxRules.SelectedIndex + 1);
            }
        }

        protected void DoContextMenuInsert(int index)
        {
            FLExTransRule ftRule = new FLExTransRule();
            FLExTransRuleGen.FLExTransRules.Insert(index, ftRule);
            lBoxRules.Items.Insert(index, ftRule);
            lBoxRules.SetSelected(index, true);
            MarkAsChanged(true);
        }

        protected void MoveUpContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmMoveUp)
            {
                int index = lBoxRules.SelectedIndex;
                DoContextMenuMove(index, index - 1);
            }
        }

        protected void MoveDownContextMenu_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = (ToolStripItem)sender;
            if (menuItem.Name == cmMoveDown)
            {
                int index = lBoxRules.SelectedIndex;
                DoContextMenuMove(index, index + 1);
            }
        }

        protected void DoContextMenuMove(int index, int otherIndex)
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

        protected void DeleteContextMenu_Click(object sender, EventArgs e)
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

        protected void DuplicateContextMenu_Click(object sender, EventArgs e)
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
                    AdjustContextMenuContent(lBoxSender, indexAtMouse);
                    lBoxSender.SelectedIndex = indexAtMouse;
                    Point ptClickedAt = e.Location;
                    ptClickedAt = lBoxSender.PointToScreen(ptClickedAt);
                    editContextMenu.Show(ptClickedAt);
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
