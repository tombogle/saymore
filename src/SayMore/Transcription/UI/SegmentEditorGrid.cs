using System.Drawing;
using System.Windows.Forms;
using SayMore.Properties;
using SilTools;

namespace SayMore.Transcription.UI
{
	public class SegmentEditorGrid : SilGrid
	{
		/// ------------------------------------------------------------------------------------
		public SegmentEditorGrid()
		{
			Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
			Margin = new Padding(0);
			VirtualMode = true;
			AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			EditMode = DataGridViewEditMode.EditOnEnter;
			FullRowFocusRectangleColor = DefaultCellStyle.SelectionBackColor;
			DefaultCellStyle.SelectionForeColor = DefaultCellStyle.ForeColor;
			DefaultCellStyle.SelectionBackColor =
				ColorHelper.CalculateColor(Color.White, DefaultCellStyle.SelectionBackColor, 140);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// When the user is in a transcription cell, this will intercept the tab and shift+tab
		/// keys so they move to the next transcription cell or previous transcription cell
		/// respectively.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (IsCurrentCellInEditMode && msg.WParam.ToInt32() == (int)Keys.Tab)
			{
				int newRowIndex =  CurrentCellAddress.Y + (ModifierKeys == Keys.Shift ? -1 : 1);

				if (newRowIndex >= 0 && newRowIndex < RowCount)
				{
					EndEdit();
					CurrentCell = this[CurrentCell.ColumnIndex, newRowIndex];
				}

				return true;
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		/// ------------------------------------------------------------------------------------
		protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
		{
			base.OnColumnWidthChanged(e);
			Settings.Default.SegmentGrid = GridSettings.Create(this);
		}
	}
}
