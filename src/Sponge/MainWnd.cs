using System.Windows.Forms;
using SIL.Localize.LocalizationUtils;
using SIL.Sponge.Model;
using SIL.Sponge.Properties;

namespace SIL.Sponge
{
	/// ----------------------------------------------------------------------------------------
	/// <summary>
	/// Class for the main window of the application.
	/// </summary>
	/// ----------------------------------------------------------------------------------------
	public partial class MainWnd : Form
	{
		private SetupVw m_setupView;
		private OverviewVw m_overviewView;
		private SessionsVw m_sessionsView;
		private PeopleVw m_peopleView;
		private SendReceiveVw m_sendReceiveView;
		private ViewButtonManager m_viewManger;

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Gets the current Sponge project.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		public static SpongeProject CurrentProject { get; private set; }

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="MainWnd"/> class.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		public MainWnd()
		{
			InitializeComponent();

			if (Settings.Default.MainWndBounds.Height < 0)
				StartPosition = FormStartPosition.CenterScreen;
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="MainWnd"/> class.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		public MainWnd(SpongeProject prj) : this()
		{
			CurrentProject = prj;
			CurrentProject.FileWatcherSynchronizingObject = this;
			SetupViews();
			m_viewManger.SetView(tsbSetup);
			SetWindowText();
			LocalizeItemDlg.StringsLocalized += SetWindowText;
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Create and load all the views.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		private void SetupViews()
		{
			m_setupView = new SetupVw();
			m_overviewView = new OverviewVw();
			m_sessionsView = new SessionsVw(CurrentProject);
			m_peopleView = new PeopleVw();
			m_sendReceiveView = new SendReceiveVw();

			Controls.AddRange(new Control[] { m_setupView, m_overviewView,
				m_sessionsView, m_peopleView, m_sendReceiveView });

			m_viewManger = new ViewButtonManager(tsMain,
				new Control[] { m_setupView, m_overviewView, m_sessionsView, m_peopleView, m_sendReceiveView });
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
				m_viewManger.Dispose();
			}

			base.Dispose(disposing);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Sets the localized window title texts.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		private void SetWindowText()
		{
			var fmt = LocalizationManager.GetString(this);
			Text = string.Format(fmt, CurrentProject.ProjectName);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		protected override void OnLoad(System.EventArgs e)
		{
			base.OnLoad(e);

			// Do this here because it doesn't work in the constructor.
			if (Settings.Default.MainWndBounds.Height >= 0)
				Bounds = Settings.Default.MainWndBounds;
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.FormClosing"/> event.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			Settings.Default.MainWndBounds = Bounds;
			Settings.Default.Save();
			LocalizeItemDlg.StringsLocalized -= SetWindowText;
		}
	}
}
