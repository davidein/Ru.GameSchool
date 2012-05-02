using Ru.GameSchool.BusinessLayer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ru.GameSchool.DataLayer.Repository;
using System.Collections.Generic;
using Rhino.Mocks;

namespace Ru.GameSchool.BusinessLayerTests
{
    
    
    /// <summary>
    ///This is a test class for GameServiceTest and is intended
    ///to contain all GameServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GameServiceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GameService Constructor
        ///</summary>
        [TestMethod()]
        public void GameServiceConstructorTest()
        {
            GameService target = new GameService();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for AddPointsToLevel
        ///</summary>
        [TestMethod()]
        public void AddPointsToLevelTest()
        {
            GameService target = new GameService(); // TODO: Initialize to an appropriate value
            int userInfoId = 0; // TODO: Initialize to an appropriate value
            int levelId = 0; // TODO: Initialize to an appropriate value
            int points = 0; // TODO: Initialize to an appropriate value
            target.AddPointsToLevel(userInfoId, levelId, points);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CalculatePoints
        ///</summary>
        [TestMethod()]
        public void CalculatePointsTest()
        {
            GameService target = new GameService(); // TODO: Initialize to an appropriate value
            target.CalculatePoints();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetPoints
        ///</summary>
        [TestMethod()]
        public void GetPointsTest()
        {
            GameService target = new GameService(); // TODO: Initialize to an appropriate value
            int userInfoId = 0; // TODO: Initialize to an appropriate value
            int levelId = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetPoints(userInfoId, levelId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPointsComparedToUsers
        ///</summary>
        [TestMethod()]
        public void GetPointsComparedToUsersTest()
        {

            GameService target = new GameService(); // TODO: Initialize to an appropriate value
            int userId = 0; // TODO: Initialize to an appropriate value
            int courseId = 0; // TODO: Initialize to an appropriate value
            target.GetPointsComparedToUsers(userId, courseId);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetTopTenList
        ///</summary>
        [TestMethod()]
        public void GetTopTenListTest()
        {
            var mockRepository = MockRepository.GenerateMock<IGameSchoolEntities>();
            var gameService = new GameService();
            gameService.SetDatasource(mockRepository);

            GameService target = new GameService(); // TODO: Initialize to an appropriate value
            int courseId = 0; // TODO: Initialize to an appropriate value
            IEnumerable<Tuple<int, UserInfo>> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Tuple<int, UserInfo>> actual;
            actual = target.GetTopTenList(courseId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
