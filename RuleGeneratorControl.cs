// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

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

        public RuleGeneratorControl()
        {
            InitializeComponent();
            wv2RuleEditor.WebMessageReceived += webView2_WebMessageReceived;
            wv2RuleEditor.Source = GetUriOfWebPage();
            //new Uri(@"C:\Users\Andy Black\Documents\FieldWorks\FLExTrans\RuleGenerator\TreeFlex\Test123.html");
            wv2RuleEditor.Show();
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
            Uri uriBase = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var rootdir = Path.GetDirectoryName(Uri.UnescapeDataString(uriBase.AbsolutePath));
            int i = rootdir.LastIndexOf(kAppNAme);
            string basedir = rootdir.Substring(0, i);
            string testDataDir = Path.Combine(basedir, kAppNAme, "TestData");
            string webfile = Path.Combine(testDataDir, "Test123.html");
            Uri uri = new Uri(webfile);
            return uri;
        }
    }
}
