namespace SayMore.UI.ComponentEditors
{
	partial class AudioComponentEditor
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this._labelRecordist = new System.Windows.Forms.Label();
			this._tableLayout = new System.Windows.Forms.TableLayoutPanel();
			this._recordist = new System.Windows.Forms.TextBox();
			this._labelDevice = new System.Windows.Forms.Label();
			this._device = new System.Windows.Forms.TextBox();
			this._labelMicrophone = new System.Windows.Forms.Label();
			this._microphone = new System.Windows.Forms.TextBox();
			this._labelSampleRate = new System.Windows.Forms.Label();
			this._labelBitDepth = new System.Windows.Forms.Label();
			this._sampleRate = new System.Windows.Forms.TextBox();
			this._bitDepth = new System.Windows.Forms.TextBox();
			this._labelAnalogGain = new System.Windows.Forms.Label();
			this._labelDigitalGain = new System.Windows.Forms.Label();
			this._analogGain = new System.Windows.Forms.TextBox();
			this._digitalGain = new System.Windows.Forms.TextBox();
			this._labelChannel = new System.Windows.Forms.Label();
			this._channel = new System.Windows.Forms.TextBox();
			this._binder = new SayMore.UI.ComponentEditors.BindingHelper(this.components);
			this._presetMenuButton = new System.Windows.Forms.Button();
			this._presetMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._tableLayout.SuspendLayout();
			this.SuspendLayout();
			// 
			// _labelRecordist
			// 
			this._labelRecordist.AutoSize = true;
			this._labelRecordist.BackColor = System.Drawing.Color.Transparent;
			this._labelRecordist.Location = new System.Drawing.Point(0, 30);
			this._labelRecordist.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
			this._labelRecordist.Name = "_labelRecordist";
			this._labelRecordist.Size = new System.Drawing.Size(52, 13);
			this._labelRecordist.TabIndex = 0;
			this._labelRecordist.Text = "Recordist";
			// 
			// _tableLayout
			// 
			this._tableLayout.AutoSize = true;
			this._tableLayout.ColumnCount = 3;
			this._tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46F));
			this._tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
			this._tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
			this._tableLayout.Controls.Add(this._labelRecordist, 0, 1);
			this._tableLayout.Controls.Add(this._recordist, 0, 2);
			this._tableLayout.Controls.Add(this._labelDevice, 0, 3);
			this._tableLayout.Controls.Add(this._device, 0, 4);
			this._tableLayout.Controls.Add(this._labelMicrophone, 0, 5);
			this._tableLayout.Controls.Add(this._microphone, 0, 6);
			this._tableLayout.Controls.Add(this._labelSampleRate, 1, 1);
			this._tableLayout.Controls.Add(this._labelBitDepth, 2, 1);
			this._tableLayout.Controls.Add(this._sampleRate, 1, 2);
			this._tableLayout.Controls.Add(this._bitDepth, 2, 2);
			this._tableLayout.Controls.Add(this._labelAnalogGain, 1, 3);
			this._tableLayout.Controls.Add(this._labelDigitalGain, 2, 3);
			this._tableLayout.Controls.Add(this._analogGain, 1, 4);
			this._tableLayout.Controls.Add(this._digitalGain, 2, 4);
			this._tableLayout.Controls.Add(this._labelChannel, 1, 5);
			this._tableLayout.Controls.Add(this._channel, 1, 6);
			this._tableLayout.Controls.Add(this._presetMenuButton, 0, 0);
			this._tableLayout.Dock = System.Windows.Forms.DockStyle.Top;
			this._tableLayout.Location = new System.Drawing.Point(7, 7);
			this._tableLayout.Name = "_tableLayout";
			this._tableLayout.RowCount = 7;
			this._tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this._tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this._tableLayout.Size = new System.Drawing.Size(435, 163);
			this._tableLayout.TabIndex = 0;
			// 
			// _recordist
			// 
			this._recordist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._recordist.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._binder.SetIsBound(this._recordist, true);
			this._recordist.Location = new System.Drawing.Point(0, 46);
			this._recordist.Margin = new System.Windows.Forms.Padding(0, 3, 5, 3);
			this._recordist.Name = "_recordist";
			this._recordist.Size = new System.Drawing.Size(195, 22);
			this._recordist.TabIndex = 1;
			// 
			// _labelDevice
			// 
			this._labelDevice.AutoSize = true;
			this._labelDevice.BackColor = System.Drawing.Color.Transparent;
			this._labelDevice.Location = new System.Drawing.Point(0, 76);
			this._labelDevice.Margin = new System.Windows.Forms.Padding(0, 5, 3, 0);
			this._labelDevice.Name = "_labelDevice";
			this._labelDevice.Size = new System.Drawing.Size(41, 13);
			this._labelDevice.TabIndex = 2;
			this._labelDevice.Text = "Device";
			// 
			// _device
			// 
			this._device.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._device.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._binder.SetIsBound(this._device, true);
			this._device.Location = new System.Drawing.Point(0, 92);
			this._device.Margin = new System.Windows.Forms.Padding(0, 3, 5, 3);
			this._device.Name = "_device";
			this._device.Size = new System.Drawing.Size(195, 22);
			this._device.TabIndex = 3;
			// 
			// _labelMicrophone
			// 
			this._labelMicrophone.AutoSize = true;
			this._labelMicrophone.BackColor = System.Drawing.Color.Transparent;
			this._labelMicrophone.Location = new System.Drawing.Point(0, 122);
			this._labelMicrophone.Margin = new System.Windows.Forms.Padding(0, 5, 3, 0);
			this._labelMicrophone.Name = "_labelMicrophone";
			this._labelMicrophone.Size = new System.Drawing.Size(63, 13);
			this._labelMicrophone.TabIndex = 4;
			this._labelMicrophone.Text = "Microphone";
			// 
			// _microphone
			// 
			this._microphone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._microphone.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._binder.SetIsBound(this._microphone, true);
			this._microphone.Location = new System.Drawing.Point(0, 138);
			this._microphone.Margin = new System.Windows.Forms.Padding(0, 3, 5, 3);
			this._microphone.Name = "_microphone";
			this._microphone.Size = new System.Drawing.Size(195, 22);
			this._microphone.TabIndex = 5;
			// 
			// _labelSampleRate
			// 
			this._labelSampleRate.AutoSize = true;
			this._labelSampleRate.BackColor = System.Drawing.Color.Transparent;
			this._labelSampleRate.Location = new System.Drawing.Point(205, 30);
			this._labelSampleRate.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
			this._labelSampleRate.Name = "_labelSampleRate";
			this._labelSampleRate.Size = new System.Drawing.Size(68, 13);
			this._labelSampleRate.TabIndex = 6;
			this._labelSampleRate.Text = "Sample Rate";
			// 
			// _labelBitDepth
			// 
			this._labelBitDepth.AutoSize = true;
			this._labelBitDepth.BackColor = System.Drawing.Color.Transparent;
			this._labelBitDepth.Location = new System.Drawing.Point(322, 30);
			this._labelBitDepth.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
			this._labelBitDepth.Name = "_labelBitDepth";
			this._labelBitDepth.Size = new System.Drawing.Size(51, 13);
			this._labelBitDepth.TabIndex = 12;
			this._labelBitDepth.Text = "Bit Depth";
			// 
			// _sampleRate
			// 
			this._sampleRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._sampleRate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._binder.SetIsBound(this._sampleRate, true);
			this._sampleRate.Location = new System.Drawing.Point(205, 46);
			this._sampleRate.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
			this._sampleRate.Name = "_sampleRate";
			this._sampleRate.Size = new System.Drawing.Size(107, 22);
			this._sampleRate.TabIndex = 7;
			// 
			// _bitDepth
			// 
			this._bitDepth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._bitDepth.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._binder.SetIsBound(this._bitDepth, true);
			this._bitDepth.Location = new System.Drawing.Point(322, 46);
			this._bitDepth.Margin = new System.Windows.Forms.Padding(5, 3, 0, 3);
			this._bitDepth.Name = "_bitDepth";
			this._bitDepth.Size = new System.Drawing.Size(113, 22);
			this._bitDepth.TabIndex = 13;
			// 
			// _labelAnalogGain
			// 
			this._labelAnalogGain.AutoSize = true;
			this._labelAnalogGain.BackColor = System.Drawing.Color.Transparent;
			this._labelAnalogGain.Location = new System.Drawing.Point(205, 76);
			this._labelAnalogGain.Margin = new System.Windows.Forms.Padding(5, 5, 3, 0);
			this._labelAnalogGain.Name = "_labelAnalogGain";
			this._labelAnalogGain.Size = new System.Drawing.Size(65, 13);
			this._labelAnalogGain.TabIndex = 8;
			this._labelAnalogGain.Text = "Analog Gain";
			// 
			// _labelDigitalGain
			// 
			this._labelDigitalGain.AutoSize = true;
			this._labelDigitalGain.BackColor = System.Drawing.Color.Transparent;
			this._labelDigitalGain.Location = new System.Drawing.Point(322, 76);
			this._labelDigitalGain.Margin = new System.Windows.Forms.Padding(5, 5, 3, 0);
			this._labelDigitalGain.Name = "_labelDigitalGain";
			this._labelDigitalGain.Size = new System.Drawing.Size(61, 13);
			this._labelDigitalGain.TabIndex = 14;
			this._labelDigitalGain.Text = "Digital Gain";
			// 
			// _analogGain
			// 
			this._analogGain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._analogGain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._binder.SetIsBound(this._analogGain, true);
			this._analogGain.Location = new System.Drawing.Point(205, 92);
			this._analogGain.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
			this._analogGain.Name = "_analogGain";
			this._analogGain.Size = new System.Drawing.Size(107, 22);
			this._analogGain.TabIndex = 9;
			// 
			// _digitalGain
			// 
			this._digitalGain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._digitalGain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._binder.SetIsBound(this._digitalGain, true);
			this._digitalGain.Location = new System.Drawing.Point(322, 92);
			this._digitalGain.Margin = new System.Windows.Forms.Padding(5, 3, 0, 3);
			this._digitalGain.Name = "_digitalGain";
			this._digitalGain.Size = new System.Drawing.Size(113, 22);
			this._digitalGain.TabIndex = 15;
			// 
			// _labelChannel
			// 
			this._labelChannel.AutoSize = true;
			this._labelChannel.BackColor = System.Drawing.Color.Transparent;
			this._labelChannel.Location = new System.Drawing.Point(205, 122);
			this._labelChannel.Margin = new System.Windows.Forms.Padding(5, 5, 3, 0);
			this._labelChannel.Name = "_labelChannel";
			this._labelChannel.Size = new System.Drawing.Size(46, 13);
			this._labelChannel.TabIndex = 10;
			this._labelChannel.Text = "Channel";
			// 
			// _channel
			// 
			this._channel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._channel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._binder.SetIsBound(this._channel, true);
			this._channel.Location = new System.Drawing.Point(205, 138);
			this._channel.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
			this._channel.Name = "_channel";
			this._channel.Size = new System.Drawing.Size(107, 22);
			this._channel.TabIndex = 11;
			// 
			// _presetMenuButton
			// 
			this._presetMenuButton.Location = new System.Drawing.Point(3, 3);
			this._presetMenuButton.Name = "_presetMenuButton";
			this._presetMenuButton.Size = new System.Drawing.Size(18, 23);
			this._presetMenuButton.TabIndex = 16;
			this._presetMenuButton.Text = ">";
			this._presetMenuButton.UseVisualStyleBackColor = true;
			this._presetMenuButton.Click += new System.EventHandler(this._presetMenuButton_Click);
			// 
			// _presetMenu
			// 
			this._presetMenu.Name = "_presetMenu";
			this._presetMenu.Size = new System.Drawing.Size(61, 4);
			// 
			// AudioComponentEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._tableLayout);
			this.Name = "AudioComponentEditor";
			this.Size = new System.Drawing.Size(449, 188);
			this._tableLayout.ResumeLayout(false);
			this._tableLayout.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label _labelRecordist;
		private System.Windows.Forms.TableLayoutPanel _tableLayout;
		private System.Windows.Forms.Label _labelDevice;
		private System.Windows.Forms.Label _labelMicrophone;
		private BindingHelper _binder;
		private System.Windows.Forms.TextBox _recordist;
		private System.Windows.Forms.TextBox _device;
		private System.Windows.Forms.Label _labelBitDepth;
		private System.Windows.Forms.TextBox _microphone;
		private System.Windows.Forms.TextBox _bitDepth;
		private System.Windows.Forms.Label _labelSampleRate;
		private System.Windows.Forms.TextBox _sampleRate;
		private System.Windows.Forms.Label _labelAnalogGain;
		private System.Windows.Forms.Label _labelDigitalGain;
		private System.Windows.Forms.TextBox _analogGain;
		private System.Windows.Forms.TextBox _digitalGain;
		private System.Windows.Forms.Label _labelChannel;
		private System.Windows.Forms.TextBox _channel;
		private System.Windows.Forms.Button _presetMenuButton;
		private System.Windows.Forms.ContextMenuStrip _presetMenu;
	}
}
