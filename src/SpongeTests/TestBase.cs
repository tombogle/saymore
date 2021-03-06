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
// File: TestBase.cs
// Responsibility: Olson
//
// <remarks>
// </remarks>
// ---------------------------------------------------------------------------------------------
using NUnit.Framework;
using Palaso.TestUtilities;
using SIL.Sponge.Model;
using SilUtils;

namespace SIL.Sponge
{
	/// ----------------------------------------------------------------------------------------
	/// <summary>
	///
	/// </summary>
	/// ----------------------------------------------------------------------------------------
	public class TestBase
	{
		protected const string kTestPrjName = "~~Moldy Sponge";
		protected const string kTestPrjFileName = "~~MoldySponge.sprj";
		protected const string kTestSessionName = "~~Fungus";

		protected SpongeProject _prj;
		protected TemporaryFolder _mainAppFldr;

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Runs before each test.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[SetUp]
		public virtual void TestSetup()
		{
			SessionFileBase.PreventGettingMediaFileDurationsUsingDirectX = true;
			_mainAppFldr = new TemporaryFolder("~SpongeTestProjects~");
			ReflectionHelper.SetField(typeof(Sponge), "s_mainAppFldr", _mainAppFldr.FolderPath);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Runs after each test.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		[TearDown]
		public virtual void TestTearDown()
		{
			if (_mainAppFldr != null)
				_mainAppFldr.Dispose();
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Initializes a test project.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		protected void InitProject()
		{
			_prj = ReflectionHelper.GetResult(typeof(SpongeProject),
				"Create", kTestPrjName) as SpongeProject;

			ReflectionHelper.SetProperty(typeof(MainWnd), "CurrentProject", _prj);
		}
	}
}
