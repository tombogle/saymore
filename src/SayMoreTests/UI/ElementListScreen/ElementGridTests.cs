using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using NUnit.Framework;
using SayMore.Model;
using SayMore.Model.Files;
using SayMore.UI.ElementListScreen;
using Palaso.TestUtilities;
using SayMoreTests.Model;

namespace SayMoreTests.UI.ElementListScreen
{
	/// ----------------------------------------------------------------------------------------
	[TestFixture]
	public class ElementGridTests
	{
		private ElementGrid _grid;
		private TemporaryFolder _tmpFolder;

		/// ------------------------------------------------------------------------------------
		[SetUp]
		public void Setup()
		{
			_grid = new ElementGrid();
			_tmpFolder = new TemporaryFolder("ElementGridTests");
		}

		/// ------------------------------------------------------------------------------------
		[TearDown]
		public void TearDown()
		{
			_tmpFolder.Dispose();
			_tmpFolder = null;
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void SetFileType_UsingEventType_CreatesCorrectColumns()
		{
			_grid.SetFileType(new EventFileType(null));

			Assert.AreEqual(3, _grid.ColumnCount);
			Assert.IsNotNull(_grid.Columns.Cast<DataGridViewColumn>().FirstOrDefault(x => x.Name.ToLower() == "id"));
			Assert.IsNotNull(_grid.Columns.Cast<DataGridViewColumn>().FirstOrDefault(x => x.Name.ToLower() == "status"));
			Assert.IsNotNull(_grid.Columns.Cast<DataGridViewColumn>().FirstOrDefault(x => x.Name.ToLower() == "stages"));
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void SetFileType_UsingPersonType_CreatesCorrectColumns()
		{
			_grid.SetFileType(new PersonFileType(null));

			Assert.AreEqual(2, _grid.ColumnCount);
			Assert.IsNotNull(_grid.Columns.Cast<DataGridViewColumn>().FirstOrDefault(x => x.Name.ToLower() == "id"));
			Assert.IsNotNull(_grid.Columns.Cast<DataGridViewColumn>().FirstOrDefault(x => x.Name.ToLower() == "status"));
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void Items_SetToNull_ReturnsEmptyList()
		{
			_grid.Items = null;
			Assert.IsNotNull(_grid.Items);
			Assert.AreEqual(0, _grid.Items.Count());
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void Items_SetToValidList_ReturnsValues()
		{
			var p1 = ProjectElementTests.CreatePerson(_tmpFolder.Path, "fred");
			var p2 = ProjectElementTests.CreatePerson(_tmpFolder.Path, "barney");

			_grid.Items = new[] { p1, p2 };

			Assert.IsTrue(_grid.Items.Contains(p1));
			Assert.IsTrue(_grid.Items.Contains(p2));
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void SelectElement_SelectNull_Ignores()
		{
			_grid.Items = new[]
			{
				ProjectElementTests.CreatePerson(_tmpFolder.Path, "fred"),
				ProjectElementTests.CreatePerson(_tmpFolder.Path, "barney")
			};

			_grid.SelectElement(1);
			Assert.AreEqual("barney", _grid.GetCurrentElement().Id);
			_grid.SelectElement(null);
			Assert.AreEqual("barney", _grid.GetCurrentElement().Id);
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void SelectElement_SelectSomethingWhenListEmpty_Throws()
		{
			Assert.Throws<ArgumentException>(() =>
				_grid.SelectElement(ProjectElementTests.CreatePerson(_tmpFolder.Path, "barney")));
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void SelectElement_SelectSomethingNotInList_Throws()
		{
			_grid.Items = new[]
			{
				ProjectElementTests.CreatePerson(_tmpFolder.Path, "fred"),
				ProjectElementTests.CreatePerson(_tmpFolder.Path, "barney")
			};

			Assert.Throws<ArgumentException>(() =>
				_grid.SelectElement(ProjectElementTests.CreatePerson(_tmpFolder.Path, "dino")));
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void SelectElement_SelectSomethingUsingIndex_CorrectlySelects()
		{
			_grid.Items = new[]
			{
				ProjectElementTests.CreatePerson(_tmpFolder.Path, "fred"),
				ProjectElementTests.CreatePerson(_tmpFolder.Path, "barney")
			};

			_grid.SelectElement(1);
			Assert.AreEqual("barney", _grid.GetCurrentElement().Id);
			_grid.SelectElement(0);
			Assert.AreEqual("fred", _grid.GetCurrentElement().Id);
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void SelectElement_SelectSomethingUsingElement_CorrectlySelects()
		{
			var p1 = ProjectElementTests.CreatePerson(_tmpFolder.Path, "fred");
			var p2 = ProjectElementTests.CreatePerson(_tmpFolder.Path, "barney");

			_grid.Items = new[] { p1, p2 };

			_grid.SelectElement(p2);
			Assert.AreEqual(p2, _grid.GetCurrentElement());
			_grid.SelectElement(p1);
			Assert.AreEqual(p1, _grid.GetCurrentElement());
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void SelectElement_IndexOutOfRange_Throws()
		{
			_grid.Items = new[]
			{
				ProjectElementTests.CreatePerson(_tmpFolder.Path, "fred"),
				ProjectElementTests.CreatePerson(_tmpFolder.Path, "barney")
			};

			Assert.Throws<IndexOutOfRangeException>(() => _grid.SelectElement(-1));
			Assert.Throws<IndexOutOfRangeException>(() => _grid.SelectElement(2));
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void GetCurrentElement_WhenListEmpty_ReturnsNull()
		{
			Assert.IsNull(_grid.GetCurrentElement());
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void GetSelectedElements_WhenListEmpty_ReturnsEmptyList()
		{
			Assert.AreEqual(0, _grid.GetSelectedElements().Count());
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void GetSelectedElements_WhenSingleElementSelected_ReturnsIt()
		{
			_grid.Items = new[]
			{
				ProjectElementTests.CreatePerson(_tmpFolder.Path, "fred"),
				ProjectElementTests.CreatePerson(_tmpFolder.Path, "barney"),
			};

			_grid.SelectElement(1);
			Assert.AreEqual(1, _grid.GetSelectedElements().Count());
			Assert.AreEqual("barney", _grid.GetSelectedElements().ElementAt(0).Id);
		}

		/// ------------------------------------------------------------------------------------
		[Test]
		public void GetSelectedElements_WhenMultipleElementsSelected_ReturnsThem()
		{
			_grid.Items = new[]
			{
				ProjectElementTests.CreatePerson(_tmpFolder.Path, "fred"),
				ProjectElementTests.CreatePerson(_tmpFolder.Path, "barney"),
				ProjectElementTests.CreatePerson(_tmpFolder.Path, "dino")
			};

			_grid.Rows[0].Selected = true;
			_grid.Rows[2].Selected = true;
			Assert.AreEqual(2, _grid.GetSelectedElements().Count());
			Assert.IsNotNull(_grid.GetSelectedElements().First(x => x.Id == "fred"));
			Assert.IsNotNull(_grid.GetSelectedElements().First(x => x.Id == "dino"));

			_grid.Rows[1].Selected = true;
			Assert.AreEqual(3, _grid.GetSelectedElements().Count());
		}
	}
}