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
// File: PersonTests.cs
// Responsibility: D. Olson
//
// <remarks>
// </remarks>
// ---------------------------------------------------------------------------------------------
using System;
using System.IO;
using NUnit.Framework;
using Palaso.TestUtilities;

namespace SIL.Sponge.Model
{
	/// ----------------------------------------------------------------------------------------
	/// <summary>
	/// Tests for the Person class.
	/// </summary>
	/// ----------------------------------------------------------------------------------------
	[TestFixture]
	public class PersonTests : TestBase
	{
		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Runs before each test.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		public override void TestSetup()
		{
			base.TestSetup();
			InitProject();
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the CreateFromName method.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void CreateFromName()
		{
			var person = Person.CreateFromName(_prj, "Dudley Doright");
			Assert.AreEqual("Dudley Doright", person.FullName);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the FileName property.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void FileName()
		{
			var person = Person.CreateFromName(_prj, "Mozart");
			Assert.AreEqual("Mozart.person", person.FileName);

			person = Person.CreateFromName(_prj, "J:S/Bach");
			Assert.AreEqual("J_S_Bach.person", person.FileName);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the Folder property.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void Folder()
		{
			var person = Person.CreateFromName(_prj, "Brahms");
			Assert.AreEqual(Path.Combine(_prj.PeopleFolder, "Brahms"), person.Folder);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the PermissionsFolder property.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void PermissionsFolder()
		{
			var person = Person.CreateFromName(_prj, "Bach");
			var expectedPath = Path.Combine(_prj.PeopleFolder, "Bach");
			expectedPath = Path.Combine(expectedPath, "Permissions");
			Assert.AreEqual(expectedPath, person.PermissionsFolder);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the PermissionsFiles property.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void PermissionsFiles()
		{
			var person = Person.CreateFromName(_prj, "Chopin");
			person.Save();
			Assert.AreEqual(0, person.PermissionFiles.Length);

			var path = Path.Combine(_prj.PeopleFolder, "Chopin");
			path = Path.Combine(path, "Permissions");
			Directory.CreateDirectory(path);
			File.CreateText(Path.Combine(path, "concerto1.pdf")).Close();
			File.CreateText(Path.Combine(path, "concerto2.doc")).Close();

			Assert.AreEqual(2, person.PermissionFiles.Length);
			Assert.AreEqual("concerto1.pdf", Path.GetFileName(person.PermissionFiles[0]));
			Assert.AreEqual("concerto2.doc", Path.GetFileName(person.PermissionFiles[1]));
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the HasPermissions property.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void HasPermissions()
		{
			var person = Person.CreateFromName(_prj, "E. Power Biggs");
			person.Save();

			Assert.IsFalse(person.HasPermissions);
			Directory.CreateDirectory(person.PermissionsFolder);
			File.CreateText(Path.Combine(person.PermissionsFolder, "Fugue.tmp")).Close();
			Assert.IsTrue(person.HasPermissions);
			Directory.Delete(person.PermissionsFolder, true);
			Assert.IsFalse(person.HasPermissions);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the FullPath property.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void FullFilePath()
		{
			var person = Person.CreateFromName(_prj, "Mozart");
			var expectedPath = Path.Combine(_prj.PeopleFolder, "Mozart");
			expectedPath = Path.Combine(expectedPath, "Mozart.person");
			Assert.AreEqual(expectedPath, person.FullFilePath);

			person = Person.CreateFromName(_prj, "J:S/Bach");
			expectedPath = Path.Combine(_prj.PeopleFolder, "J_S_Bach");
			expectedPath = Path.Combine(expectedPath, "J_S_Bach.person");
			Assert.AreEqual(expectedPath, person.FullFilePath);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the CanSave property.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void CanSave()
		{
			var person = Person.CreateFromName(_prj, null);
			Assert.IsFalse(person.CanSave);
			person = Person.CreateFromName(_prj, string.Empty);
			Assert.IsFalse(person.CanSave);
			person = Person.CreateFromName(_prj, "Fogerty");
			Assert.IsTrue(person.CanSave);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the PictureFile property.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void PictureFile_PictureFileExists_GivesCorrectPath()
		{
			var person = Person.CreateFromName(_prj, "D.Fogerty");
			person.Save();

			foreach (var ext in new[] { "jpg", "gif", "tif", "png", "bmp", "dib" })
			{
				var path = Path.ChangeExtension(person.FullFilePath, ext);
				File.CreateText(path).Close();
				Assert.AreEqual(path, person.PictureFile);
				File.Delete(path);
			}
		}

		[Test]
		public void PictureFile_PictureFileDoesNotExists_GivesNull()
		{
			var person = Person.CreateFromName(_prj, "D.Fogerty");
			person.Save();
			Assert.IsNull(person.PictureFile);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the CopyPictureFile method when the picture file does not exist.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		[Category("SkipOnTeamCity")]
		[ExpectedException(typeof(FileNotFoundException))]
		public void CopyPictureFile_BadFile_Throws()
		{
			var person = Person.CreateFromName(_prj, "P.Collins");
			person.CopyPictureFile("invalid.jpg");
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the CopyPictureFile method when the picture file is null.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		[Category("SkipOnTeamCity")]
		[ExpectedException(typeof(NullReferenceException))]
		public void CopyPictureFile_NullFile_Throws()
		{
			var person = Person.CreateFromName(_prj, "Flock of Seagulls");
			person.CopyPictureFile(null);
		}

		[Test]
		public void CopyPictureFile_PersonIsSaved_CopiesAndRenames()
		{
			foreach (var ext in new[] { "jpg", "gif", "tif", "png", "bmp", "dib" })
			{
				var person = Person.CreateFromName(_prj, "Beethoven");
				person.Save();
				string srcPicFile = null;

				try
				{
					srcPicFile = Path.Combine(_prj.PeopleFolder, "~SpongPic~");
					srcPicFile = Path.ChangeExtension(srcPicFile, ext);
					File.CreateText(srcPicFile).Close();

					person.CopyPictureFile(srcPicFile);

					var expected = Path.Combine(person.Folder, "Beethoven");
					expected = Path.ChangeExtension(expected, ext);
					Assert.IsTrue(File.Exists(expected));
					Assert.IsTrue(File.Exists(srcPicFile));
					File.Delete(Path.Combine(person.Folder, "Beethoven.person"));
				}
				finally
				{
					File.Delete(srcPicFile);
				}
			}
		}

		[Test]
		public void CopyPictureFile_PersonNotYetSaved_CopiesAndRenames()
		{
			var person = Person.CreateFromName(_prj, "Beethoven");

			using (var pic = new TempFile())
			{
				person.CopyPictureFile(pic.Path);
				using (TemporaryFolder.TrackExisting(person.Folder))
				{
					var expected = Path.Combine(person.Folder, "Beethoven");

					expected = Path.ChangeExtension(expected, Path.GetExtension(pic.Path));
					Assert.IsTrue(File.Exists(expected));
				}
			}
		}

		[Test]
		public void CanChoosePicture_PersonHasNoName_False()
		{
			var person = Person.CreateFromName(_prj, string.Empty);
			Assert.IsFalse(person.CanChoosePicture);
		}


		[Test]
		public void CanChoosePicture_PersonHasName_True()
		{
			var person = Person.CreateFromName(_prj, "Beethoven");
			Assert.IsTrue(person.CanChoosePicture);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the Rename method.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void Rename()
		{
			var person = Person.CreateFromName(_prj, "Chumbawamba");
			person.Save();

			var picFile = Path.Combine(person.Folder, "Chumbawamba.jpg");
			File.CreateText(picFile).Close();

			Assert.IsTrue(Directory.Exists(person.Folder));
			Assert.IsTrue(File.Exists(picFile));
			Assert.IsTrue(File.Exists(person.FullFilePath));
			Assert.IsFalse(Directory.Exists(Path.Combine(_prj.PeopleFolder, "B_Joel")));

			var prevPersonFolder = person.Folder;
			person.Rename("B:Joel");

			Assert.IsFalse(Directory.Exists(prevPersonFolder));
			Assert.IsTrue(Directory.Exists(person.Folder));
			Assert.IsTrue(person.Folder.EndsWith("B_Joel"));
			Assert.AreEqual("B_Joel.person", person.FileName);

			Assert.IsTrue(File.Exists(person.FullFilePath));
			Assert.IsTrue(File.Exists(Path.Combine(person.Folder, "B_Joel.jpg")));
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the Delete method.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void Delete()
		{
			var person = Person.CreateFromName(_prj, "U2");
			person.Save();
			var picFile = Path.Combine(person.Folder, "U2.tif");
			File.CreateText(picFile).Close();

			var personFile = Path.Combine(person.Folder, "U2.person");
			var personFolder = person.Folder;

			Assert.IsTrue(Directory.Exists(personFolder));
			Assert.IsTrue(File.Exists(personFile));
			Assert.IsTrue(File.Exists(picFile));

			person.Delete();

			Assert.IsFalse(File.Exists(personFile));
			Assert.IsFalse(File.Exists(picFile));
			Assert.IsFalse(Directory.Exists(personFolder));
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the Load method.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void Load()
		{
			var person = Person.CreateFromName(_prj, @"\Sweet");
			person.PrimaryLanguage = "English";
			person.OtherLangauge0 = "Farsi";
			person.OtherLangauge1 = "Lithuanian";
			person.Notes = "It's a beautiful day";
			person.LearnedLanguageIn = "Cradle";
			person.BirthYear = "around 1812";
			person.ContactInfo = "Around the corner";
			person.Education = "PhD";
			person.FathersLanguage = "Farsi";
			person.MothersLanguage = "Lithuanian";
			person.Gender = Gender.Female;
			person.PrimaryOccupation = "Bus Driver";

			person.Save();
			Assert.IsTrue(File.Exists(person.FullFilePath));

			// Create from full path to the person's file.
			person = Person.Load(_prj, person.FullFilePath);
			Assert.AreEqual(@"\Sweet", person.FullName);
			Assert.AreEqual("English", person.PrimaryLanguage);
			Assert.AreEqual("Farsi", person.OtherLangauge0);
			Assert.AreEqual("Lithuanian", person.OtherLangauge1);
			Assert.AreEqual("It's a beautiful day", person.Notes);
			Assert.AreEqual("Cradle", person.LearnedLanguageIn);
			Assert.AreEqual("around 1812", person.BirthYear);
			Assert.AreEqual("Around the corner", person.ContactInfo);
			Assert.AreEqual("PhD", person.Education);
			Assert.AreEqual("Farsi", person.FathersLanguage);
			Assert.AreEqual("Lithuanian", person.MothersLanguage);
			Assert.AreEqual(Gender.Female, person.Gender);
			Assert.AreEqual("Bus Driver", person.PrimaryOccupation);
			Assert.IsNull(person.OtherLangauge2);
			Assert.IsNull(person.OtherLangauge3);

			// Create from full path to the person's folder.
			var path = Path.Combine(_prj.PeopleFolder, "_Sweet");
			person = Person.Load(_prj, path);
			Assert.IsNotNull(person);
			Assert.AreEqual(@"\Sweet", person.FullName);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Tests the CreateFromFile method when the path of the file name is invalid or
		/// incomplete.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[Test]
		public void CreateFromFile_WithIncompletePath()
		{
			var person = Person.CreateFromName(_prj, @"Smash?Mouth");
			person.Education = "BS, Whitworth College";
			person.Save();
			Assert.IsTrue(File.Exists(person.FullFilePath));

			person = Person.Load(_prj, @"junk\preceding\filename\Smash_Mouth.person");
			Assert.IsNotNull(person);
			Assert.AreEqual("BS, Whitworth College", person.Education);
		}



		[Test]
		public void ChangeName_HadSameNameBefore_ReturnsTrue()
		{
			var person = Person.CreateFromName(_prj, "Pedro");
			person.Save();
			person.ChangeName("Pedro");
			Assert.AreEqual("Pedro", person.FullName);
			Assert.AreEqual("Pedro", Path.GetFileName(person.Folder));
			Assert.AreEqual("Pedro", Path.GetFileNameWithoutExtension(person.FullFilePath));
		}

		[Test, Category("SkipOnTeamCity"), ExpectedException(typeof(ApplicationException))]
		public void ChangeName_NewIdIsEmptyString_Throws()
		{
			var person = Person.CreateFromName(_prj, "Pedro");
			person.Save();
			person.ChangeName("");
		}

		[Test, Category("SkipOnTeamCity"), ExpectedException(typeof(ApplicationException))]
		public void ChangeName_NewIdIsInvalidFolderName_Throws()
		{
			var person = Person.CreateFromName(_prj, "Pedro");
			person.Save();
		   person.ChangeName("chan*ge");
		}

		[Test]
		public void ChangeName_ShouldChange_NameAndFolderAndFileChange()
		{
			var person = Person.CreateFromName(_prj, "Pedro");
			person.Save();
			string originalPath = person.FullFilePath;
			person.ChangeName("change");
			Assert.AreEqual("change", person.FullName);
			Assert.AreEqual("change", Path.GetFileName(person.Folder));
			Assert.AreEqual("change", Path.GetFileNameWithoutExtension(person.FullFilePath));
			Assert.IsFalse(Directory.Exists(originalPath));
			Assert.IsFalse(File.Exists(Path.Combine(person.Folder, "Pedro." + Sponge.PersonFileExtension)));
		}

		[Test]
		public void ChangeName_ShouldChange_NameChangePersisted()
		{
			var person = Person.CreateFromName(_prj, "Pedro");
			person.FathersLanguage = "esperanto";
			person.Save();
			person.ChangeName("change");
			person.Save();
			var same = Person.Load(_prj, Path.Combine(Path.Combine(_prj.Folder, "people"), "change"));
			Assert.AreEqual("change", same.FullName);
			Assert.AreEqual("esperanto", same.FathersLanguage);
		}

		[Test]
		public void ChangeName_HasFilesWithOldName_RenamesFiles()
		{
			var person = Person.CreateFromName(_prj, "Pedro");
			person.Save();
			File.CreateText(Path.Combine(person.Folder, "Pedro.jpg")).Close();
			person.ChangeName("change");
			Assert.IsTrue(File.Exists(Path.Combine(person.Folder, "change.jpg")));
			Assert.IsFalse(File.Exists(Path.Combine(person.Folder, "Pedro.jpg")));
		}
	}
}
