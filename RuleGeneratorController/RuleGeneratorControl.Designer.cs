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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleGeneratorControl));
			this.lBoxRules = new System.Windows.Forms.ListBox();
			this.wv2RuleEditor = new Microsoft.Web.WebView2.WinForms.WebView2();
			this.lblName = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.lblRules = new System.Windows.Forms.Label();
			this.lblRightClickToEdit = new System.Windows.Forms.Label();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
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
			this.lBoxRules.SelectedIndexChanged += new System.EventHandler(this.lBoxRules_SelectedIndexChanged);
			this.lBoxRules.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lBoxRules_MouseUp);
			// 
			// wv2RuleEditor
			// 
			this.wv2RuleEditor.AllowExternalDrop = true;
			this.wv2RuleEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.wv2RuleEditor.CreationProperties = null;
			this.wv2RuleEditor.DefaultBackgroundColor = System.Drawing.Color.White;
			this.wv2RuleEditor.Location = new System.Drawing.Point(305, 134);
			this.wv2RuleEditor.Name = "wv2RuleEditor";
			this.wv2RuleEditor.Size = new System.Drawing.Size(1246, 659);
			this.wv2RuleEditor.TabIndex = 2;
			this.wv2RuleEditor.ZoomFactor = 1D;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(305, 89);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(92, 20);
			this.lblName.TabIndex = 3;
			this.lblName.Text = "Rule Name:";
			// 
			// tbName
			// 
			this.tbName.Location = new System.Drawing.Point(417, 89);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(597, 26);
			this.tbName.TabIndex = 4;
			// 
			// lblRules
			// 
			this.lblRules.AutoSize = true;
			this.lblRules.Location = new System.Drawing.Point(8, 60);
			this.lblRules.Name = "lblRules";
			this.lblRules.Size = new System.Drawing.Size(50, 20);
			this.lblRules.TabIndex = 5;
			this.lblRules.Text = "Rules";
			// 
			// lblRightClickToEdit
			// 
			this.lblRightClickToEdit.AutoSize = true;
			this.lblRightClickToEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRightClickToEdit.Location = new System.Drawing.Point(80, 60);
			this.lblRightClickToEdit.Name = "lblRightClickToEdit";
			this.lblRightClickToEdit.Size = new System.Drawing.Size(140, 20);
			this.lblRightClickToEdit.TabIndex = 6;
			this.lblRightClickToEdit.Text = "(Right-click to edit)";
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.Location = new System.Drawing.Point(1475, 84);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(75, 39);
			this.btnHelp.TabIndex = 7;
			this.btnHelp.Text = "Help";
			this.btnHelp.UseVisualStyleBackColor = true;
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(1347, 84);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(98, 39);
			this.btnSave.TabIndex = 8;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// RuleGeneratorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1584, 820);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.lblRightClickToEdit);
			this.Controls.Add(this.lblRules);
			this.Controls.Add(this.tbName);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.wv2RuleEditor);
			this.Controls.Add(this.lBoxRules);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RuleGeneratorControl";
			this.Text = "FLExTrans Rule Generator";
			((System.ComponentModel.ISupportInitialize)(this.wv2RuleEditor)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lBoxRules;
		private Microsoft.Web.WebView2.WinForms.WebView2 wv2RuleEditor;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.Label lblRules;
		private System.Windows.Forms.Label lblRightClickToEdit;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnSave;
	}
}