using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;
using Palaso.Reporting;
using Palaso.TestUtilities;
using SayMore.Model;
using SayMore.Model.Files;
using SayMore.UI.Archiving;

namespace SayMoreTests.Utilities
{
	[TestFixture]
	public class ArchivingDlgViewModelTests
	{
		private TemporaryFolder _tmpFolder;
		private Mock<Event> _event;
		private Mock<Person> _person;
		private Mock<PersonInformant> _personInformant;
		private ArchivingDlgViewModel _helper;

		/// ------------------------------------------------------------------------------------
		[SetUp]
		public void Setup()
		{
			ErrorReport.IsOkToInteractWithUser = false;

			_tmpFolder = new TemporaryFolder("ArchiveHelperTestFolder");

			CreateEventAndPersonFolders();

			SetupMocks();

			_helper = new ArchivingDlgViewModel(_event.Object, _personInformant.Object);
		}

		/// ------------------------------------------------------------------------------------
		private void CreateEventAndPersonFolders()
		{
			// Create an event
			var folder = Path.Combine(_tmpFolder.Path, "Events");
			Directory.CreateDirectory(folder);
			folder = Path.Combine(folder, "ddo-event");
			Directory.CreateDirectory(folder);
			File.CreateText(Path.Combine(folder, "ddo.event")).Close();
			File.CreateText(Path.Combine(folder, "ddo.mpg")).Close();
			File.CreateText(Path.Combine(folder, "ddo.mp3")).Close();
			File.CreateText(Path.Combine(folder, "ddo.pdf")).Close();

			// Create a person
			folder = Path.Combine(_tmpFolder.Path, "People");
			Directory.CreateDirectory(folder);
			folder = Path.Combine(folder, "ddo-person");
			Directory.CreateDirectory(folder);
			File.CreateText(Path.Combine(folder, "ddoPic.jpg")).Close();
			File.CreateText(Path.Combine(folder, "ddoVoice.wav")).Close();
		}

		/// ------------------------------------------------------------------------------------
		private void SetupMocks()
		{
			var metaFile = new Mock<ProjectElementComponentFile>();
			metaFile.Setup(m => m.GetStringValue("title", null)).Returns("StupidEvent");

			_event = new Mock<Event>();
			_event.Setup(e => e.FolderPath).Returns(Path.Combine(Path.Combine(_tmpFolder.Path, "Events"), "ddo-event"));
			_event.Setup(e => e.GetAllParticipants()).Returns(new[] { "ddo-person" });
			_event.Setup(e => e.Id).Returns("ddo");
			_event.Setup(e => e.MetaDataFile).Returns(metaFile.Object);

			_person = new Mock<Person>();
			_person.Setup(p => p.FolderPath).Returns(Path.Combine(Path.Combine(_tmpFolder.Path, "People"), "ddo-person"));
			_person.Setup(p => p.Id).Returns("ddo-person");

			_personInformant = new Mock<PersonInformant>();
			_personInformant.Setup(i => i.GetPersonByName("ddo-person")).Returns(_person.Object);
		}

		/// ------------------------------------------------------------------------------------
		[TearDown]
		public void TearDown()
		{
			_tmpFolder.Dispose();
			_helper.CleanUp();

			try { Directory.Delete(Path.Combine(Path.GetTempPath(), "ddo-event"), true); }
			catch { }

			try { File.Delete(_helper.RampPackagePath); }
			catch { }
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		[Category("SkipOnTeamCity")]
		public void GetFilesToArchive_GetsCorrectListSize()
		{
			var files = _helper.GetFilesToArchive();
			Assert.AreEqual(2, files.Count);
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		[Category("SkipOnTeamCity")]
		public void GetFilesToArchive_GetsCorrectEventFiles()
		{
			var files = _helper.GetFilesToArchive();
			Assert.AreEqual(4, files[string.Empty].Count());

			var list = files[string.Empty].Select(Path.GetFileName).ToList();
			Assert.Contains("ddo.event", list);
			Assert.Contains("ddo.mpg", list);
			Assert.Contains("ddo.mp3", list);
			Assert.Contains("ddo.pdf", list);
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		[Category("SkipOnTeamCity")]
		public void GetFilesToArchive_GetsCorrectPersonFiles()
		{
			var files = _helper.GetFilesToArchive();
			Assert.AreEqual(2, files["ddo-person"].Count());
			var list = files["ddo-person"].Select(Path.GetFileName).ToList();
			Assert.Contains("ddoPic.jpg", list);
			Assert.Contains("ddoVoice.wav", list);
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		[Category("SkipOnTeamCity")]
		public void CreateEventArchive_CreatesArchive()
		{
			Assert.IsTrue(_helper.CreateEventArchive());
			Assert.IsTrue(File.Exists(Path.Combine(Path.GetTempPath(), "ddo.zip")));
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void CreateMetsFile_CreatesFile()
		{
			Assert.IsTrue(_helper.CreateMetsFile());
			Assert.IsTrue(File.Exists(Path.Combine(Path.GetTempPath(), "mets.xml")));
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		[Category("SkipOnTeamCity")]
		public void CreateRampPackageWithEventArchiveAndMetsFile_CreatesRampPackage()
		{
			_helper.CreateEventArchive();
			_helper.CreateMetsFile();
			Assert.IsTrue(_helper.CreateRampPackageWithEventArchiveAndMetsFile());
			Assert.IsTrue(File.Exists(_helper.RampPackagePath));
		}
	}
}