namespace SIL.FLExTransRuleGenerator.Control
{
	partial class RuleGeneratorControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lBoxRules = new System.Windows.Forms.ListBox();
			this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
			this.wv2RuleEditor = new Microsoft.Web.WebView2.WinForms.WebView2();
			((System.ComponentModel.ISupportInitialize)(this.wv2RuleEditor)).BeginInit();
			this.SuspendLayout();
			// 
			// lBoxRules
			// 
			this.lBoxRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lBoxRules.FormattingEnabled = true;
			this.lBoxRules.ItemHeight = 20;
			this.lBoxRules.Location = new System.Drawing.Point(4, 89);
			this.lBoxRules.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.lBoxRules.Name = "lBoxRules";
			this.lBoxRules.Size = new System.Drawing.Size(266, 704);
			this.lBoxRules.TabIndex = 0;
			// 
			// checkedListBox1
			// 
			this.checkedListBox1.FormattingEnabled = true;
			this.checkedListBox1.Location = new System.Drawing.Point(129, 74);
			this.checkedListBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.Size = new System.Drawing.Size(10, 4);
			this.checkedListBox1.TabIndex = 1;
			// 
			// wv2RuleEditor
			// 
			this.wv2RuleEditor.AllowExternalDrop = true;
			this.wv2RuleEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.wv2RuleEditor.CreationProperties = null;
			this.wv2RuleEditor.DefaultBackgroundColor = System.Drawing.Color.White;
			this.wv2RuleEditor.Location = new System.Drawing.Point(305, 89);
			this.wv2RuleEditor.Name = "wv2RuleEditor";
			this.wv2RuleEditor.Size = new System.Drawing.Size(1246, 704);
			this.wv2RuleEditor.TabIndex = 2;
			this.wv2RuleEditor.ZoomFactor = 1D;
			// 
			// RuleGeneratorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1584, 820);
			this.Controls.Add(this.wv2RuleEditor);
			this.Controls.Add(this.checkedListBox1);
			this.Controls.Add(this.lBoxRules);
			this.Name = "RuleGeneratorControl";
			this.Text = "FLExTrans Rule Generator";
			((System.ComponentModel.ISupportInitialize)(this.wv2RuleEditor)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lBoxRules;
		private System.Windows.Forms.CheckedListBox checkedListBox1;
		private Microsoft.Web.WebView2.WinForms.WebView2 wv2RuleEditor;
	}
}