// ---------------------------------------------------------------------------------------------
#region // Copyright (c) 2010, SIL International. All Rights Reserved.
// <copyright from='2010' to='2010' company='SIL International'>
//		Copyright (c) 2010, SIL International. All Rights Reserved.
//
//		Distributable under the terms of either the Common Public License or the
//		GNU Lesser General Public License, as specified in the LICENSING.txt file.
// </copyright>
#endregion
//
// File: NewSessionsFromFileDlgViewModelTests.cs
// Responsibility: D. Olson
//
// <remarks>
// </remarks>
// ---------------------------------------------------------------------------------------------
using System.Linq;
using System.IO;
using NUnit.Framework;
using Palaso.TestUtilities;
using SIL.Sponge.Model;
using SilUtils;

namespace SIL.Sponge.Dialogs
{
	/// ----------------------------------------------------------------------------------------
	/// <summary>
	/// Test for the NewSessionsFromFileDlgViewModel class
	/// </summary>
	/// ----------------------------------------------------------------------------------------
	[TestFixture]
	public class NewSessionsFromFileDlgViewModelTests : TestBase
	{
		private NewSessionsFromFileDlgViewModel _viewModel;

		/// ------------------------------------------------------------------------------------
		/// <summary>
		///
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[SetUp]
		public override void TestSetup()
		{
			base.TestSetup();

			SessionFileBase.PreventGettingMediaFileDurationsUsingDirectX = true;
			_viewModel = new NewSessionsFromFileDlgViewModel();
			_viewModel.WaitForAsyncFileLoadingToFinish = true;
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		///
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[TearDown]
		public override void TestTearDown()
		{
			base.TestTearDown();
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that LoadFilesFromFolder only loads audio or video files.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void LoadFilesFromFolder_VerifyFileTypesLoaded()
		{
			File.CreateText(_mainAppFldr.Combine("hamster.pdf")).Close();
			File.CreateText(_mainAppFldr.Combine("dog.wav")).Close();
			File.CreateText(_mainAppFldr.Combine("cat.mpg")).Close();

			ReflectionHelper.CallMethod(_viewModel, "LoadFilesFromFolder", _mainAppFldr.FolderPath);

			Assert.AreEqual(2, _viewModel.Files.Count);
			Assert.IsNotNull(_viewModel.Files.First(x => x.FileName == "dog.wav"));
			Assert.IsNotNull(_viewModel.Files.First(x => x.FileName == "cat.mpg"));
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that LoadFilesFromFolder clears the Files list when sent a null folder.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void LoadFilesFromFolder_FolderArgumentIsNull()
		{
			var path = _mainAppFldr.Combine("bird.doc");
			File.CreateText(path).Close();
			_viewModel.Files.Add(new NewSessionFile(path));
			Assert.AreEqual(1, _viewModel.Files.Count);
			ReflectionHelper.CallMethod(_viewModel, "LoadFilesFromFolder", new object[] { null });
			Assert.AreEqual(0, _viewModel.Files.Count);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that LoadFilesFromFolder clears the Files list when sent a folder whose
		/// name is empty.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void LoadFilesFromFolder_FolderArgumentIsEmpty()
		{
			var path = _mainAppFldr.Combine("bird.doc");
			File.CreateText(path).Close();
			_viewModel.Files.Add(new NewSessionFile(path));
			Assert.AreEqual(1, _viewModel.Files.Count);
			ReflectionHelper.CallMethod(_viewModel, "LoadFilesFromFolder", string.Empty);
			Assert.AreEqual(0, _viewModel.Files.Count);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that LoadFilesFromFolder finishes with a sorted list of files.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void LoadFilesFromFolder_VerifyFileListSorted()
		{
			File.CreateText(_mainAppFldr.Combine("z.wma")).Close();
			File.CreateText(_mainAppFldr.Combine("y.wav")).Close();
			File.CreateText(_mainAppFldr.Combine("y.mp3")).Close();

			ReflectionHelper.CallMethod(_viewModel, "LoadFilesFromFolder", _mainAppFldr.FolderPath);

			Assert.AreEqual(3, _viewModel.Files.Count);
			Assert.AreEqual("y.mp3", _viewModel.Files[0].FileName);
			Assert.AreEqual("y.wav", _viewModel.Files[1].FileName);
			Assert.AreEqual("z.wma", _viewModel.Files[2].FileName);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that SelectedFolder property loads files.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void SelectedFolder()
		{
			File.CreateText(_mainAppFldr.Combine("dog.wav")).Close();
			File.CreateText(_mainAppFldr.Combine("cat.mpg")).Close();

			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;

			Assert.AreEqual(2, _viewModel.Files.Count);
			Assert.AreEqual("cat.mpg", _viewModel.Files[0].FileName);
			Assert.AreEqual("dog.wav", _viewModel.Files[1].FileName);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the SelectAllFiles method.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void SelectAllFiles_False_FilesAreUnselected()
		{
			File.CreateText(_mainAppFldr.Combine("dog.wav")).Close();
			File.CreateText(_mainAppFldr.Combine("cat.mpg")).Close();
			File.CreateText(_mainAppFldr.Combine("goat.mov")).Close();

			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;

			Assert.IsTrue(_viewModel.Files[0].Selected);
			Assert.IsTrue(_viewModel.Files[1].Selected);
			Assert.IsTrue(_viewModel.Files[2].Selected);

			_viewModel.SelectAllFiles(false);

			Assert.IsFalse(_viewModel.Files[0].Selected);
			Assert.IsFalse(_viewModel.Files[1].Selected);
			Assert.IsFalse(_viewModel.Files[2].Selected);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the SelectAllFiles method.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void SelectAllFiles_True_FilesAreSelected()
		{
			File.CreateText(_mainAppFldr.Combine("dog.wav")).Close();
			File.CreateText(_mainAppFldr.Combine("cat.mpg")).Close();
			File.CreateText(_mainAppFldr.Combine("goat.mov")).Close();

			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;
			_viewModel.SelectAllFiles(false);

			Assert.IsFalse(_viewModel.Files[0].Selected);
			Assert.IsFalse(_viewModel.Files[1].Selected);
			Assert.IsFalse(_viewModel.Files[2].Selected);

			_viewModel.SelectAllFiles(true);

			Assert.IsTrue(_viewModel.Files[0].Selected);
			Assert.IsTrue(_viewModel.Files[1].Selected);
			Assert.IsTrue(_viewModel.Files[2].Selected);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that the NumberOfSelectedFiles property indicates that files are selected
		/// by default when they are added.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void NumberOfSelectedFiles_SelectedByDefault()
		{
			Assert.AreEqual(0, _viewModel.NumberOfSelectedFiles);

			File.CreateText(_mainAppFldr.Combine("dog.wav")).Close();
			File.CreateText(_mainAppFldr.Combine("cat.wav")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;
			Assert.AreEqual(2, _viewModel.NumberOfSelectedFiles);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that the NumberOfSelectedFiles property after adding two files and
		/// unselecting one, then both.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void NumberOfSelectedFiles_ProperCountAfterUnselect()
		{
			File.CreateText(_mainAppFldr.Combine("dog.wav")).Close();
			File.CreateText(_mainAppFldr.Combine("cat.wav")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;
			Assert.AreEqual(2, _viewModel.NumberOfSelectedFiles);
			_viewModel.Files[1].Selected = false;
			Assert.AreEqual(1, _viewModel.NumberOfSelectedFiles);
			_viewModel.Files[0].Selected = false;
			Assert.AreEqual(0, _viewModel.NumberOfSelectedFiles);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that the AnyFilesSelected property after adding two files and
		/// unselecting one, then both.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void AnyFilesSelected()
		{
			Assert.IsFalse(_viewModel.AnyFilesSelected);
			File.CreateText(_mainAppFldr.Combine("dog.wav")).Close();
			File.CreateText(_mainAppFldr.Combine("cat.wav")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;
			Assert.IsTrue(_viewModel.AnyFilesSelected);
			_viewModel.Files[1].Selected = false;
			Assert.IsTrue(_viewModel.AnyFilesSelected);
			_viewModel.Files[0].Selected = false;
			Assert.IsFalse(_viewModel.AnyFilesSelected);
			_viewModel.Files[0].Selected = true;
			Assert.IsTrue(_viewModel.AnyFilesSelected);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that the AllFilesSelected property after adding two files and
		/// unselecting one, then both.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void AllFilesSelected()
		{
			Assert.IsFalse(_viewModel.AnyFilesSelected);
			File.CreateText(_mainAppFldr.Combine("dog.wav")).Close();
			File.CreateText(_mainAppFldr.Combine("cat.wav")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;
			Assert.IsTrue(_viewModel.AllFilesSelected);
			_viewModel.Files[1].Selected = false;
			Assert.IsFalse(_viewModel.AllFilesSelected);
			_viewModel.Files[1].Selected = true;
			Assert.IsTrue(_viewModel.AllFilesSelected);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		///
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void ToggleFilesSelectedState_fileIndex_TooLarge()
		{
			File.CreateText(_mainAppFldr.Combine("pig.wmv")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;
			Assert.IsTrue(_viewModel.Files[0].Selected);
			_viewModel.ToggleFilesSelectedState(1);
			Assert.IsTrue(_viewModel.Files[0].Selected);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		///
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void ToggleFilesSelectedState_fileIndex_TooSmall()
		{
			File.CreateText(_mainAppFldr.Combine("pig.wmv")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;
			Assert.IsTrue(_viewModel.Files[0].Selected);
			_viewModel.ToggleFilesSelectedState(-1);
			Assert.IsTrue(_viewModel.Files[0].Selected);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		///
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void ToggleFilesSelectedState_fileIndex_JustRight()
		{
			File.CreateText(_mainAppFldr.Combine("pig.wmv")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;
			Assert.IsTrue(_viewModel.Files[0].Selected);
			_viewModel.ToggleFilesSelectedState(0);
			Assert.IsFalse(_viewModel.Files[0].Selected);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		///
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void GetFullFilePath_fileIndex_TooLarge()
		{
			File.CreateText(_mainAppFldr.Combine("pig.wmv")).Close();
			File.CreateText(_mainAppFldr.Combine("cow.wma")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;
			Assert.AreEqual(string.Empty, _viewModel.GetFullFilePath(2));
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		///
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void GetFullFilePath_fileIndex_TooSmall()
		{
			File.CreateText(_mainAppFldr.Combine("pig.wmv")).Close();
			File.CreateText(_mainAppFldr.Combine("cow.wma")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;
			Assert.AreEqual(string.Empty, _viewModel.GetFullFilePath(-1));
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		///
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void GetFullFilePath_fileIndex_JustRight()
		{
			File.CreateText(_mainAppFldr.Combine("cow.wma")).Close();
			File.CreateText(_mainAppFldr.Combine("pig.wmv")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;
			Assert.AreEqual(_mainAppFldr.Combine("cow.wma"), _viewModel.GetFullFilePath(0));
			Assert.AreEqual(_mainAppFldr.Combine("pig.wmv"), _viewModel.GetFullFilePath(1));
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that passing a null object to the CreateSingleSession method does nothing.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test, Ignore("appears to violate the Fail Fast principle, unless there's a reason we'd be giving it nulls")]
		public void CreateSingleSession_NullFileIsIgnored()
		{
			_viewModel.CreateSingleSession(null);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that passing an unselected file object to the CreateSingleSession method
		/// does nothing.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void GetSourceAndDestinationPairs_UnselectedFileIsIgnored()
		{
			using (var temp = new TempFile())
			{
				//var path = _mainAppFldr.Combine("dog.wav");
				//File.CreateText(path).Close();
				var sessionFile = new NewSessionFile(temp.Path);
				sessionFile.Selected = false;
				using (var model = new NewSessionsFromFileDlgViewModel())
				{
					model.Files.Add(sessionFile);
					Assert.AreEqual(0,model.GetSourceAndDestinationPairs().Count());
				}
			}
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that passing some selected files to the CreateSingleSession method
		/// sets the FirstNewSessionAdded property correclty.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void CreateSingleSession_SetsFirstNewSessionAddedProperly()
		{
			InitProject();//todo: remove this when CreateSingleSession no longer uses a static

			using (var one = new TempFile())
			using (var two = new TempFile())
			{
				using (var model = new NewSessionsFromFileDlgViewModel())
				{
					Assert.IsNull(model.FirstNewSessionAdded);
					model.CreateSingleSession(one.Path);
					model.CreateSingleSession(two.Path);
					Assert.AreEqual(Path.GetFileNameWithoutExtension(one.Path), model.FirstNewSessionAdded);
				}
			}
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that passing a file object to the CreateSingleSession method
		/// creates a new session with the correct data.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void CreateSingleSession_CreatesSessionFolder()
		{
			InitProject();
			using (var temp = new TempFile())
			{
				_viewModel.CreateSingleSession(temp.Path);
				var id = Path.GetFileNameWithoutExtension(temp.Path);
				var session = Session.Create(_prj, id);
				Assert.AreEqual(id, session.Id);
				Assert.AreEqual(File.GetLastWriteTime(temp.Path).ToShortDateString(), session.Date.ToShortDateString());
			}
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that the RenameFile ignores an invalid file name sent to RenameFile.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void RenameFile_OldName_Invalid()
		{
			File.CreateText(_mainAppFldr.Combine("chicken.wav")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;

			Assert.AreEqual("chicken.wav", _viewModel.Files[0].FileName);

			ReflectionHelper.CallMethod(_viewModel, "RenameFile",
				_mainAppFldr.Combine("chicken.wav"), _mainAppFldr.Combine("nonexistent.wav"));

			Assert.AreEqual("chicken.wav", _viewModel.Files[0].FileName);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that the RenameFile changes the file path of the renamed
		/// file and resorts the files list.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void RenameFile_OldName_Valid()
		{
			File.CreateText(_mainAppFldr.Combine("chicken.wav")).Close();
			File.CreateText(_mainAppFldr.Combine("cow.wmv")).Close();
			File.CreateText(_mainAppFldr.Combine("sheep.mpg")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;

			Assert.AreEqual(3, _viewModel.Files.Count);
			Assert.AreEqual("chicken.wav", _viewModel.Files[0].FileName);
			Assert.AreEqual("cow.wmv", _viewModel.Files[1].FileName);
			Assert.AreEqual("sheep.mpg", _viewModel.Files[2].FileName);

			File.Move(_viewModel.Files[0].FullFilePath, _mainAppFldr.Combine("hen.wav"));

			ReflectionHelper.CallMethod(_viewModel, "RenameFile",
				_mainAppFldr.Combine("hen.wav"), _mainAppFldr.Combine("chicken.wav"));

			Assert.AreEqual(3, _viewModel.Files.Count);
			Assert.AreEqual("cow.wmv", _viewModel.Files[0].FileName);
			Assert.AreEqual("hen.wav", _viewModel.Files[1].FileName);
			Assert.AreEqual("sheep.mpg", _viewModel.Files[2].FileName);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that the RemoveFile ignores an invalid file name sent to RemoveFile.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void RemoveFile_fullPath_Invalid()
		{
			File.CreateText(_mainAppFldr.Combine("chicken.wav")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;

			Assert.AreEqual("chicken.wav", _viewModel.Files[0].FileName);
			ReflectionHelper.CallMethod(_viewModel, "RemoveFile", _mainAppFldr.Combine("nonexistent.wav"));
			Assert.AreEqual("chicken.wav", _viewModel.Files[0].FileName);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that the RemoveFile removes a file from the files list.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void RemoveFile_fullPath_Valid()
		{
			File.CreateText(_mainAppFldr.Combine("chicken.wav")).Close();
			File.CreateText(_mainAppFldr.Combine("cow.wmv")).Close();
			File.CreateText(_mainAppFldr.Combine("sheep.mpg")).Close();

			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;

			Assert.AreEqual(3, _viewModel.Files.Count);
			Assert.AreEqual("chicken.wav", _viewModel.Files[0].FileName);
			Assert.AreEqual("cow.wmv", _viewModel.Files[1].FileName);
			Assert.AreEqual("sheep.mpg", _viewModel.Files[2].FileName);

			ReflectionHelper.CallMethod(_viewModel, "RemoveFile", _mainAppFldr.Combine("chicken.wav"));

			Assert.AreEqual(2, _viewModel.Files.Count);
			Assert.AreEqual("cow.wmv", _viewModel.Files[0].FileName);
			Assert.AreEqual("sheep.mpg", _viewModel.Files[1].FileName);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that the AddFile ignores an invalid file name.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void AddFile_fullPath_Invalid()
		{
			Assert.AreEqual(0, _viewModel.Files.Count);
			ReflectionHelper.CallMethod(_viewModel, "AddFile", _mainAppFldr.Combine("nonexistent.wav"));
			Assert.AreEqual(0, _viewModel.Files.Count);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that the AddFile ignores an attempt to add a file that's already in the list.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void AddFile_fullPath_AlreadyInList()
		{
			File.CreateText(_mainAppFldr.Combine("goat.mpg")).Close();
			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;
			Assert.AreEqual(1, _viewModel.Files.Count);
			Assert.AreEqual("goat.mpg", _viewModel.Files[0].FileName);
			ReflectionHelper.CallMethod(_viewModel, "AddFile", _mainAppFldr.Combine("goat.mpg"));
			Assert.AreEqual(1, _viewModel.Files.Count);
			Assert.AreEqual("goat.mpg", _viewModel.Files[0].FileName);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests that the AddFile adds a file to the files list and that the list is sorted
		/// accordingly.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void AddFile_fullPath_Valid()
		{
			File.CreateText(_mainAppFldr.Combine("sheep.mpg")).Close();
			File.CreateText(_mainAppFldr.Combine("chicken.wav")).Close();

			_viewModel.SelectedFolder = _mainAppFldr.FolderPath;

			Assert.AreEqual(2, _viewModel.Files.Count);
			Assert.AreEqual("chicken.wav", _viewModel.Files[0].FileName);
			Assert.AreEqual("sheep.mpg", _viewModel.Files[1].FileName);

			File.CreateText(_mainAppFldr.Combine("cow.wmv")).Close();
			ReflectionHelper.CallMethod(_viewModel, "AddFile", _mainAppFldr.Combine("cow.wmv"));

			Assert.AreEqual(3, _viewModel.Files.Count);
			Assert.AreEqual("chicken.wav", _viewModel.Files[0].FileName);
			Assert.AreEqual("cow.wmv", _viewModel.Files[1].FileName);
			Assert.AreEqual("sheep.mpg", _viewModel.Files[2].FileName);
		}
	}
}
