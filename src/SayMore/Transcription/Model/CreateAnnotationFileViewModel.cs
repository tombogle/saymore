using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Localization;
using SayMore.UI.LowLevelControls;

namespace SayMore.Transcription.Model
{
	public class CreateAnnotationFileViewModel : IProgressViewModel
	{
		private readonly AnnotationFileHelper _helper;
		private readonly AudacityLabelInfo[] _audacityLabels;

		public event EventHandler OnFinished;
		public event EventHandler OnUpdateProgress;
		public event EventHandler OnUpdateStatus;

		public int CurrentProgressValue { get; private set; }
		public int MaximumProgressValue { get; private set; }
		public string StatusString { get; private set; }

		/// ------------------------------------------------------------------------------------
		public CreateAnnotationFileViewModel(AnnotationFileHelper helper,
			IEnumerable<AudacityLabelInfo> audacityLabels)
		{
			_helper = helper;
			_audacityLabels = audacityLabels.ToArray();
			MaximumProgressValue = _audacityLabels.Length;

			StatusString = LocalizationManager.GetString(
				"EventsView.Transcription.AnnotationFileHelper.CreateAnnotationFileFromAudacity.ProgressMsg",
				"Creating Annotation File from Audacity Label File...");
		}

		/// ------------------------------------------------------------------------------------
		public void Start()
		{
			var worker = new BackgroundWorker();
			worker.WorkerReportsProgress = true;
			worker.ProgressChanged += HandleWorkerProgressChanged;
			worker.DoWork += BuildAnnotationFile;
			worker.RunWorkerAsync();
			while (worker.IsBusy) { Application.DoEvents(); }

			_helper.Save();

			if (OnFinished != null)
			{
				StatusString = LocalizationManager.GetString(
					"EventsView.Transcription.AnnotationFileHelper.CreateAnnotationFileFromAudacity.FinishedMsg",
					"Finished Creating Annotation File");

				OnFinished.Invoke(null, null);
			}
		}

		/// ------------------------------------------------------------------------------------
		private void BuildAnnotationFile(object sender, DoWorkEventArgs e)
		{
			var worker = sender as BackgroundWorker;
			int i = 0;

			foreach (var label in _audacityLabels)
			{
				worker.ReportProgress(++i);
				_helper.AddNewTranscriptionAnnotationElement(label);
			}
		}

		/// ------------------------------------------------------------------------------------
		void HandleWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (OnUpdateProgress != null)
			{
				CurrentProgressValue = e.ProgressPercentage;
				OnUpdateProgress(this, EventArgs.Empty);
			}
		}
	}
}