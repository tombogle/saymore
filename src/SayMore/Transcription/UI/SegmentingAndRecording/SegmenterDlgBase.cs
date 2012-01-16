using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Localization;
using Localization.UI;
using Palaso.Progress;
using SayMore.AudioUtils;
using SayMore.Properties;
using SayMore.UI.LowLevelControls;
using SayMore.UI.MediaPlayer;
using SayMore.UI.Utilities;
using SilTools;

namespace SayMore.Transcription.UI
{
	/// ----------------------------------------------------------------------------------------
	public partial class SegmenterDlgBase : MonitorKeyPressDlg
	{
		protected readonly SegmenterDlgBaseViewModel _viewModel;
		protected string _segmentXofYFormat;
		protected string _segmentCountFormat;
		protected Timer _timer;
		protected bool _moreReliableDesignMode;
		private readonly WaveControlBasic _waveControl;

		/// ------------------------------------------------------------------------------------
		public SegmenterDlgBase()
		{
			WaitCursor.Show();

			_moreReliableDesignMode = (DesignMode || GetService(typeof(IDesignerHost)) != null) ||
				(LicenseManager.UsageMode == LicenseUsageMode.Designtime);

			InitializeComponent();
			InitializeZoomComboItems();

			_toolStripStatus.Renderer = new SilTools.NoToolStripBorderRenderer();
			_waveControl = CreateWaveControl();
			_waveControl.Dock = DockStyle.Fill;
			_panelWaveControl.Controls.Add(_waveControl);

			DoubleBuffered = true;
			_comboBoxZoom.Text = _comboBoxZoom.Items[0] as string;
			_comboBoxZoom.Font = SystemFonts.MenuFont;
			_labelZoom.Font = SystemFonts.MenuFont;
			_labelSegmentCount.Font = SystemFonts.MenuFont;
			_labelSegment.Font = SystemFonts.MenuFont;
			_labelTimeDisplay.Font = SystemFonts.MenuFont;
			_labelOriginalRecording.Font = FontHelper.MakeFont(SystemFonts.MenuFont, FontStyle.Bold);

			_buttonCancel.Click += delegate { Close(); };
			_buttonOK.Click += delegate { Close(); };

			_segmentCountFormat = _labelSegmentCount.Text;
			_segmentXofYFormat = _labelSegment.Text;

			LocalizeItemDlg.StringsLocalized += delegate
			{
				_segmentCountFormat = _labelSegmentCount.Text;
				_segmentXofYFormat = _labelSegment.Text;
			};
		}

		/// ------------------------------------------------------------------------------------
		public SegmenterDlgBase(SegmenterDlgBaseViewModel viewModel) : this()
		{
			_viewModel = viewModel;
			_viewModel.UpdateDisplayProvider = UpdateDisplay;

			if (!_moreReliableDesignMode && FormSettings == null)
			{
				StartPosition = FormStartPosition.CenterScreen;
				FormSettings = FormSettings.Create(this);
			}

			InitializeWaveControl();

			_labelTimeDisplay.Text = MediaPlayerViewModel.GetTimeDisplay(0f,
				(float)_viewModel.OrigWaveStream.TotalTime.TotalSeconds);
		}

		/// ------------------------------------------------------------------------------------
		protected virtual WaveControlBasic CreateWaveControl()
		{
			return new WaveControlBasic();
		}

		/// ------------------------------------------------------------------------------------
		private void InitializeZoomComboItems()
		{
			_comboBoxZoom.Items.Add(LocalizationManager.GetString(
				"DialogBoxes.Transcription.SegmenterDlgBase.ZoomPercentages.100Pct", "100%"));
			_comboBoxZoom.Items.Add(LocalizationManager.GetString(
				"DialogBoxes.Transcription.SegmenterDlgBase.ZoomPercentages.125Pct", "125%"));
			_comboBoxZoom.Items.Add(LocalizationManager.GetString(
				"DialogBoxes.Transcription.SegmenterDlgBase.ZoomPercentages.150Pct", "150%"));
			_comboBoxZoom.Items.Add(LocalizationManager.GetString(
				"DialogBoxes.Transcription.SegmenterDlgBase.ZoomPercentages.175Pct", "175%"));
			_comboBoxZoom.Items.Add(LocalizationManager.GetString(
				"DialogBoxes.Transcription.SegmenterDlgBase.ZoomPercentages.200Pct", "200%"));
			_comboBoxZoom.Items.Add(LocalizationManager.GetString(
				"DialogBoxes.Transcription.SegmenterDlgBase.ZoomPercentages.250Pct", "250%"));
			_comboBoxZoom.Items.Add(LocalizationManager.GetString(
				"DialogBoxes.Transcription.SegmenterDlgBase.ZoomPercentages.300Pct", "300%"));
			_comboBoxZoom.Items.Add(LocalizationManager.GetString(
				"DialogBoxes.Transcription.SegmenterDlgBase.ZoomPercentages.500Pct", "500%"));
			_comboBoxZoom.Items.Add(LocalizationManager.GetString(
				"DialogBoxes.Transcription.SegmenterDlgBase.ZoomPercentages.750Pct", "750%"));
			_comboBoxZoom.Items.Add(LocalizationManager.GetString(
				"DialogBoxes.Transcription.SegmenterDlgBase.ZoomPercentages.1000Pct", "1000%"));
		}

		/// ------------------------------------------------------------------------------------
		protected string GetSegmentTooShortText()
		{
			return LocalizationManager.GetString(
				"DialogBoxes.Transcription.SegmenterDlgBase.ButtonTextWhenSegmentTooShort",
				"Whoops! The segment will be too short. Continue playing.");
		}

		/// ------------------------------------------------------------------------------------
		public void InitializeWaveControl()
		{
			_waveControl.Initialize(_viewModel.OrigWaveStream);
			_waveControl.SegmentBoundaries = _viewModel.GetSegmentBoundaries();
			_waveControl.PlaybackStarted += OnPlaybackStarted;
			_waveControl.PlaybackUpdate += OnPlayingback;
			_waveControl.PlaybackStopped += OnPlaybackStopped;
		}

		/// ------------------------------------------------------------------------------------
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (_moreReliableDesignMode)
				return;

			_tableLayoutOuter.RowStyles.Clear();
			_tableLayoutOuter.RowCount = 4;
			_tableLayoutOuter.RowStyles.Add(new RowStyle());
			_tableLayoutOuter.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			_tableLayoutOuter.RowStyles.Add(new RowStyle(SizeType.Absolute, GetHeightOfTableLayoutButtonRow()));
			_tableLayoutOuter.RowStyles.Add(new RowStyle());

			_waveControl.ZoomPercentage = ZoomPercentage;
			_comboBoxZoom.Text = string.Format("{0}%", ZoomPercentage);
		}

		/// ------------------------------------------------------------------------------------
		protected virtual int GetHeightOfTableLayoutButtonRow()
		{
			throw new NotImplementedException();
		}

		/// ------------------------------------------------------------------------------------
		protected virtual FormSettings FormSettings
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		/// ------------------------------------------------------------------------------------
		protected virtual float ZoomPercentage
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		/// ------------------------------------------------------------------------------------
		protected virtual bool ShouldShadePlaybackAreaDuringPlayback
		{
			get { return true; }
		}

		/// ------------------------------------------------------------------------------------
		protected override void OnShown(EventArgs e)
		{
			if (!_moreReliableDesignMode)
				FormSettings.InitializeForm(this);

			base.OnShown(e);
			Opacity = 1f;
			WaitCursor.Hide();
		}

		/// ------------------------------------------------------------------------------------
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			e.Cancel = true;
			StopAllMedia();

			if (!_moreReliableDesignMode)
				ZoomPercentage = _waveControl.ZoomPercentage;

			// Cancel means the user closed the form using the X or Alt+F4. In that
			// case whether they want to save changes is ambiguous. So ask them.
			if (DialogResult == DialogResult.Cancel && _viewModel.WereChangesMade)
			{
				DialogResult = DialogResult.OK;

				var msg = LocalizationManager.GetString(
					"DialogBoxes.Transcription.SegmenterDlgBase.SaveChangesQuestion",
					"Would you like to save your changes?");

				DialogResult = Utils.MsgBox(msg, MessageBoxButtons.YesNoCancel);
				if (DialogResult == DialogResult.Cancel)
					return;
			}

			e.Cancel = false;

			if (DialogResult == DialogResult.OK || DialogResult == DialogResult.Yes)
				SaveChanges();

			base.OnFormClosing(e);
		}

		/// ------------------------------------------------------------------------------------
		protected virtual void SaveChanges()
		{
		}

		/// ------------------------------------------------------------------------------------
		private void HandleTableLayoutButtonsPaint(object sender, PaintEventArgs e)
		{
			using (var br = new LinearGradientBrush(_tableLayoutButtons.ClientRectangle,
				AppColors.BarEnd, AppColors.BarBegin, 0f))
			{
					e.Graphics.FillRectangle(br, _tableLayoutButtons.ClientRectangle);
			}

			using (var pen = new Pen(AppColors.BarBorder))
				e.Graphics.DrawLine(pen, 0, 0, _tableLayoutButtons.Width, 0);
		}

		/// ------------------------------------------------------------------------------------
		protected virtual void OnPlaybackStarted(WaveControlBasic ctrl, TimeSpan start, TimeSpan end)
		{
			UpdateDisplay();
		}

		/// ------------------------------------------------------------------------------------
		protected virtual void OnPlayingback(WaveControlBasic ctrl, TimeSpan current, TimeSpan total)
		{
			_labelTimeDisplay.Text = MediaPlayerViewModel.GetTimeDisplay(
				(float)current.TotalSeconds, (float)total.TotalSeconds);
		}

		/// ------------------------------------------------------------------------------------
		protected virtual void OnPlaybackStopped(WaveControlBasic ctrl, TimeSpan start, TimeSpan end)
		{
			UpdateDisplay();
		}

		#region Methods for adjusting/saving/playing within segment boundaries
		/// ------------------------------------------------------------------------------------
		protected virtual bool OnAdjustSegmentBoundaryOnArrowKey(int milliseconds)
		{
			StopAllMedia();

			var boundary = GetBoundaryToAdjustOnArrowKeys();
			if (boundary == TimeSpan.Zero || !_viewModel.MoveExistingSegmentBoundary(boundary, milliseconds))
				return false;

			_waveControl.SegmentBoundaries = _viewModel.GetSegmentBoundaries();
			PlaybackShortPortionUpToBoundary(boundary);
			return true;
		}

		/// ------------------------------------------------------------------------------------
		protected virtual TimeSpan GetBoundaryToAdjustOnArrowKeys()
		{
			return TimeSpan.Zero;
		}

		/// ------------------------------------------------------------------------------------
		public void HandleSegmentBoundaryMoved(WaveControlWithMovableBoundaries waveCtrl,
			TimeSpan oldEndTime, TimeSpan newEndTime)
		{
			StopAllMedia();
			_viewModel.SegmentBoundaryMoved(oldEndTime, newEndTime);
			UpdateDisplay();
			PlaybackShortPortionUpToBoundary(newEndTime);
		}

		/// ------------------------------------------------------------------------------------
		protected virtual void PlaybackShortPortionUpToBoundary(TimeSpan boundary)
		{
			if (boundary == TimeSpan.Zero)
				return;

			var playbackStartTime = boundary.Subtract(TimeSpan.FromMilliseconds(
				Settings.Default.MillisecondsToRePlayAfterAdjustingSegmentBoundary));

			if (playbackStartTime < TimeSpan.Zero)
				playbackStartTime = TimeSpan.Zero;

			_timer = new Timer();
			_timer.Interval = Settings.Default.MillisecondsToDelayPlaybackAfterAdjustingSegmentBoundary;
			_timer.Tick += delegate
			{
				if (!_waveControl.IsPlaying)
				{
					_waveControl.Play(playbackStartTime, boundary);
					_waveControl.PlaybackStopped += PlaybackShortPortionUpToBoundary;
				}

				_timer.Stop();
				_timer.Dispose();
				_timer = null;
			};

			_timer.Start();
		}

		/// ------------------------------------------------------------------------------------
		protected virtual void PlaybackShortPortionUpToBoundary(WaveControlBasic ctrl,
			TimeSpan time1, TimeSpan time2)
		{
			_waveControl.PlaybackStopped -= PlaybackShortPortionUpToBoundary;
		}

		/// ------------------------------------------------------------------------------------
		protected virtual void StopAllMedia()
		{
			if (_timer != null)
			{
				_timer.Stop();
				_timer.Dispose();
				_timer = null;
			}

			_waveControl.Stop();
		}

		/// ------------------------------------------------------------------------------------
		protected virtual TimeSpan GetSubSegmentReplayEndTime()
		{
			return TimeSpan.Zero;
		}

		#endregion

		/// ------------------------------------------------------------------------------------
		protected virtual void UpdateDisplay()
		{
			UpdateStatusLabelsDisplay();
		}

		/// ------------------------------------------------------------------------------------
		protected virtual void UpdateStatusLabelsDisplay()
		{
			_labelSegment.Visible = false;
			_labelSegmentCount.Visible = true;
			_labelSegmentCount.Text = string.Format(_segmentCountFormat, _viewModel.GetSegmentCount());

			_labelTimeDisplay.Text = MediaPlayerViewModel.GetTimeDisplay(
				(float)GetCurrentTimeForTimeDisplay().TotalSeconds,
				(float)_viewModel.OrigWaveStream.TotalTime.TotalSeconds);
		}

		/// ------------------------------------------------------------------------------------
		protected virtual TimeSpan GetCurrentTimeForTimeDisplay()
		{
			return _waveControl.GetCursorTime();
		}

		#region Low level keyboard handling
		/// ------------------------------------------------------------------------------------
		protected override bool OnLowLevelKeyDown(Keys key)
		{
			switch (key)
			{
				case Keys.Right:
					OnAdjustSegmentBoundaryOnArrowKey(Settings.Default.MillisecondsToAdvanceSegmentBoundaryOnRightArrow);
					return true;

				case Keys.Left:
					OnAdjustSegmentBoundaryOnArrowKey(-Settings.Default.MillisecondsToBackupSegmentBoundaryOnLeftArrow);
					return true;
			}

			return false;
		}

		/// ------------------------------------------------------------------------------------
		protected override bool OnLowLevelKeyUp(Keys key)
		{
			if (key == Keys.ControlKey)
			{
				_waveControl.Stop();
				return true;
			}

			return false;
		}

		#endregion

		#region Methods for handling zoom
		/// ------------------------------------------------------------------------------------
		private void HandleZoomComboValidating(object sender, CancelEventArgs e)
		{
			SetZoom();
		}

		/// ------------------------------------------------------------------------------------
		private void HandleZoomSelectedIndexChanged(object sender, EventArgs e)
		{
			SetZoom();
		}

		/// ------------------------------------------------------------------------------------
		private void HandleZoomKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.Handled = true;
				e.SuppressKeyPress = true;
				SetZoom();
			}
		}

		/// ------------------------------------------------------------------------------------
		private void SetZoom()
		{
			var text = _comboBoxZoom.Text.Replace("%", string.Empty).Trim();
			float newValue;
			if (float.TryParse(text, out newValue))
				_waveControl.ZoomPercentage = newValue;

			_comboBoxZoom.Text = string.Format("{0}%", _waveControl.ZoomPercentage);
		}

		#endregion
	}
}