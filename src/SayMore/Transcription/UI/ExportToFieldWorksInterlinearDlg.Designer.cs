namespace SayMore.Transcription.UI
{
	partial class ExportToFieldWorksInterlinearDlg
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
			this._tableLayout = new System.Windows.Forms.TableLayoutPanel();
			this._buttonCancel = new System.Windows.Forms.Button();
			this._buttonExport = new System.Windows.Forms.Button();
			this._labelTranscriptionWs = new System.Windows.Forms.Label();
			this._comboTranscriptionWs = new System.Windows.Forms.ComboBox();
			this._labelTranslationWs = new System.Windows.Forms.Label();
			this._comboTranslationWs = new System.Windows.Forms.ComboBox();
			this._labelOverview = new System.Windows.Forms.Label();
			this._tableLayout.SuspendLayout();
			this.SuspendLayout();
			// 
			// _tableLayout
			// 
			this._tableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._tableLayout.AutoSize = true;
			this._tableLayout.ColumnCount = 4;
			this._tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this._tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this._tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this._tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this._tableLayout.Controls.Add(this._buttonCancel, 3, 3);
			this._tableLayout.Controls.Add(this._buttonExport, 2, 3);
			this._tableLayout.Controls.Add(this._labelTranscriptionWs, 0, 1);
			this._tableLayout.Controls.Add(this._comboTranscriptionWs, 1, 1);
			this._tableLayout.Controls.Add(this._labelTranslationWs, 0, 2);
			this._tableLayout.Controls.Add(this._comboTranslationWs, 1, 2);
			this._tableLayout.Controls.Add(this._labelOverview, 0, 0);
			this._tableLayout.Location = new System.Drawing.Point(18, 18);
			this._tableLayout.Name = "_tableLayout";
			this._tableLayout.RowCount = 4;
			this._tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tableLayout.Size = new System.Drawing.Size(323, 132);
			this._tableLayout.TabIndex = 0;
			// 
			// _buttonCancel
			// 
			this._buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this._buttonCancel.AutoSize = true;
			this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._buttonCancel.Location = new System.Drawing.Point(248, 106);
			this._buttonCancel.Margin = new System.Windows.Forms.Padding(4, 8, 0, 0);
			this._buttonCancel.Name = "_buttonCancel";
			this._buttonCancel.Size = new System.Drawing.Size(75, 26);
			this._buttonCancel.TabIndex = 1;
			this._buttonCancel.Text = "Cancel";
			this._buttonCancel.UseVisualStyleBackColor = true;
			// 
			// _buttonExport
			// 
			this._buttonExport.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this._buttonExport.AutoSize = true;
			this._buttonExport.Location = new System.Drawing.Point(165, 106);
			this._buttonExport.Margin = new System.Windows.Forms.Padding(0, 8, 4, 0);
			this._buttonExport.Name = "_buttonExport";
			this._buttonExport.Size = new System.Drawing.Size(75, 26);
			this._buttonExport.TabIndex = 2;
			this._buttonExport.Text = "Export...";
			this._buttonExport.UseVisualStyleBackColor = true;
			this._buttonExport.Click += new System.EventHandler(this.HandleExportButtonClick);
			// 
			// _labelTranscriptionWs
			// 
			this._labelTranscriptionWs.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this._labelTranscriptionWs.AutoSize = true;
			this._labelTranscriptionWs.Location = new System.Drawing.Point(0, 45);
			this._labelTranscriptionWs.Margin = new System.Windows.Forms.Padding(0);
			this._labelTranscriptionWs.Name = "_labelTranscriptionWs";
			this._labelTranscriptionWs.Size = new System.Drawing.Size(71, 13);
			this._labelTranscriptionWs.TabIndex = 3;
			this._labelTranscriptionWs.Text = "Transcription:";
			// 
			// _comboTranscriptionWs
			// 
			this._tableLayout.SetColumnSpan(this._comboTranscriptionWs, 3);
			this._comboTranscriptionWs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._comboTranscriptionWs.FormattingEnabled = true;
			this._comboTranscriptionWs.Location = new System.Drawing.Point(125, 41);
			this._comboTranscriptionWs.Margin = new System.Windows.Forms.Padding(3, 5, 0, 5);
			this._comboTranscriptionWs.Name = "_comboTranscriptionWs";
			this._comboTranscriptionWs.Size = new System.Drawing.Size(198, 21);
			this._comboTranscriptionWs.TabIndex = 4;
			this._comboTranscriptionWs.SelectionChangeCommitted += new System.EventHandler(this.HandleWritingSystemChanged);
			// 
			// _labelTranslationWs
			// 
			this._labelTranslationWs.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this._labelTranslationWs.AutoSize = true;
			this._labelTranslationWs.Location = new System.Drawing.Point(0, 76);
			this._labelTranslationWs.Margin = new System.Windows.Forms.Padding(0);
			this._labelTranslationWs.Name = "_labelTranslationWs";
			this._labelTranslationWs.Size = new System.Drawing.Size(122, 13);
			this._labelTranslationWs.TabIndex = 5;
			this._labelTranslationWs.Text = "Phrase Free Translation:";
			// 
			// _comboTranslationWs
			// 
			this._tableLayout.SetColumnSpan(this._comboTranslationWs, 3);
			this._comboTranslationWs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._comboTranslationWs.FormattingEnabled = true;
			this._comboTranslationWs.Location = new System.Drawing.Point(125, 72);
			this._comboTranslationWs.Margin = new System.Windows.Forms.Padding(3, 5, 0, 5);
			this._comboTranslationWs.Name = "_comboTranslationWs";
			this._comboTranslationWs.Size = new System.Drawing.Size(198, 21);
			this._comboTranslationWs.TabIndex = 6;
			this._comboTranslationWs.SelectionChangeCommitted += new System.EventHandler(this.HandleWritingSystemChanged);
			// 
			// _labelOverview
			// 
			this._labelOverview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._labelOverview.AutoSize = true;
			this._tableLayout.SetColumnSpan(this._labelOverview, 4);
			this._labelOverview.Location = new System.Drawing.Point(0, 0);
			this._labelOverview.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this._labelOverview.Name = "_labelOverview";
			this._labelOverview.Size = new System.Drawing.Size(323, 26);
			this._labelOverview.TabIndex = 7;
			this._labelOverview.Text = "Specify the writing systems for the transcriptions and phrase free translation.";
			// 
			// ExportToFieldWorksInterlinearDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.CancelButton = this._buttonCancel;
			this.ClientSize = new System.Drawing.Size(359, 146);
			this.Controls.Add(this._tableLayout);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExportToFieldWorksInterlinearDlg";
			this.Padding = new System.Windows.Forms.Padding(15);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Export Annotations For FieldWorks Interlinear";
			this._tableLayout.ResumeLayout(false);
			this._tableLayout.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel _tableLayout;
		private System.Windows.Forms.Button _buttonCancel;
		private System.Windows.Forms.Button _buttonExport;
		private System.Windows.Forms.Label _labelTranscriptionWs;
		private System.Windows.Forms.ComboBox _comboTranscriptionWs;
		private System.Windows.Forms.Label _labelTranslationWs;
		private System.Windows.Forms.ComboBox _comboTranslationWs;
		private System.Windows.Forms.Label _labelOverview;
	}
}