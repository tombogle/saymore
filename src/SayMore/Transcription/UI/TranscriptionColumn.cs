using System.Diagnostics;
using SayMore.Transcription.Model;

namespace SayMore.Transcription.UI
{
	public class TranscriptionColumn : TierColumnBase
	{
		/// ------------------------------------------------------------------------------------
		public TranscriptionColumn(ITier tier) : base(tier)
		{
			Debug.Assert(tier.DataType == TierType.Text);

		}

		/// ------------------------------------------------------------------------------------
		protected override void OnDataGridViewChanged()
		{
			base.OnDataGridViewChanged();

			if (DataGridView == null)
				return;

			DataGridView.CellValueNeeded += (s, e) =>
			{
				if (e.ColumnIndex == Index)
					e.Value = ((ITextSegment)_tier.GetSegment(e.RowIndex)).GetText();
			};

			DataGridView.CellValuePushed += (s, e) =>
			{
				if (e.ColumnIndex == Index)
					((ITextSegment)_tier.GetSegment(e.RowIndex)).SetText(e.Value as string);
			};
		}
	}
}
