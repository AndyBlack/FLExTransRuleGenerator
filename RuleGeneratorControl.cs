// Copyright (c) 2023 SIL International
// This software is licensed under the LGPL, version 2.1 or later
// (http://www.gnu.org/licenses/lgpl-2.1.html)

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
	public partial class RuleGeneratorControl : Form
	{
		public RuleGeneratorControl()
		{
			InitializeComponent();
			wv2RuleEditor.WebMessageReceived += webView2_WebMessageReceived;
			wv2RuleEditor.Source = new Uri(@"C:\Users\Andy Black\Documents\FieldWorks\FLExTrans\RuleGenerator\TreeFlex\Test123.html");
			wv2RuleEditor.Show();
		}

		private void webView2_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs args)
		{
			string message = args.TryGetWebMessageAsString();
			MessageBox.Show(message);
		}
	}
}
