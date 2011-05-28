using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SayMore.Transcription.Model;

namespace SayMore.Transcription.UI
{
	public class AudioWaveFormColumn : TierColumnBase
	{
		private readonly TinyMediaPlayer _player;
		private DateTime _lastShiftKeyPress;
		private Control _gridEditControl;

		/// ------------------------------------------------------------------------------------
		public AudioWaveFormColumn(ITier tier) : base(tier)
		{
			Debug.Assert(tier.DataType == TierType.Audio);
			_player = new TinyMediaPlayer();
		}

		/// ------------------------------------------------------------------------------------
		protected override void OnDataGridViewChanged()
		{
			base.OnDataGridViewChanged();

			if (DataGridView == null)
				return;

			DataGridView.Controls.Add(_player);
			DataGridView.CellPainting += HandleCellPainting;
			DataGridView.KeyDown += HandleKeyDown;
			DataGridView.RowEnter += (s, e) => LocatePlayer(e.RowIndex, true);
			DataGridView.ColumnWidthChanged += (s, e) =>
			{
				if (e.Column.Index == Index)
					LocatePlayer(DataGridView.CurrentCellAddress.Y, false);
			};

			DataGridView.EditingControlShowing += (s, e) =>
			{
				_gridEditControl = e.Control;
				_gridEditControl.KeyDown += HandleKeyDown;
			};

			DataGridView.CellEndEdit += (s, e) =>
			{
				_gridEditControl.KeyDown -= HandleKeyDown;
				_gridEditControl = null;
			};
		}

		/// ------------------------------------------------------------------------------------
		void HandleKeyDown(object sender, KeyEventArgs e)
		{
			if (!e.Shift)
				return;

			if (DateTime.Now.Subtract(_lastShiftKeyPress).Milliseconds > 250)
				_lastShiftKeyPress = DateTime.Now;
			else
			{
				_player.Play();
			}
		}

		/// ------------------------------------------------------------------------------------
		void HandleCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex != Index || DataGridView.CurrentCellAddress.Y == e.RowIndex || e.RowIndex < 0)
				return;

			e.Handled = true;
			var rc = e.CellBounds;
			e.Paint(rc, DataGridViewPaintParts.Border);

			rc.Width--;
			rc.Height--;

			var segment = _tier.GetSegment(e.RowIndex) as IMediaSegment;

			_player.Draw(e.Graphics, segment.MediaStart, segment.MediaLength,
				rc, SystemColors.GrayText, e.CellStyle.BackColor);
		}

		/// ------------------------------------------------------------------------------------
		private void LocatePlayer(int rowIndex, bool stopPlayingFirst)
		{
			if (stopPlayingFirst)
				_player.Stop();

			var segment = _tier.GetSegment(rowIndex) as IMediaSegment;
			if (segment != _player.Segment)
				_player.LoadSegment(segment);

			var rc = DataGridView.GetCellDisplayRectangle(Index, rowIndex, false);
			rc.Width--;
			rc.Height--;

			if (_player.Bounds != rc)
				_player.Bounds = rc;

			_player.ForeColor = DataGridView.DefaultCellStyle.SelectionForeColor;
			_player.BackColor = DataGridView.DefaultCellStyle.SelectionBackColor;
		}
	}
}
