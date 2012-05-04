using System.ComponentModel;
using Ru.GameSchool.BusinessLayer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ru.GameSchool.DataLayer.Repository;
using System.Collections.Generic;
using Rhino.Mocks;
using Ru.GameSchool.BusinessLayerTests.Classes;

namespace Ru.GameSchool.BusinessLayerTests
{


    /// <summary>
    ///This is a test class for GameServiceTest and is intended
    ///to contain all GameServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GameServiceTest
    {

        private IGameSchoolEntities _mockRepository;
        private GameService _gameService;
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
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

        

        
        
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _mockRepository = MockRepository.GenerateMock<IGameSchoolEntities>();
            _gameService = new GameService();
            _gameService.SetDatasource(_mockRepository);
        }


        /// <summary>
        ///A test for AddPointsToLevel
        ///</summary>
        [TestMethod()]
        public void AddPointsToLevel()
        {
            var userData = new FakeObjectSet<Level>();
            var levelId = 1;
            var pointsId = 1;
            var userInfoId = 1;
            var courseId = 1;
            var points = 50;
            Point point = new Point
                              {
                                  CourseId = courseId,
                                  LevelId = levelId,
                                  PointsId = pointsId,
                                  Points = points,
                                  UserInfoId = userInfoId,
                                  Description = "Description"
                              };
            Assert.IsNotNull(point);

            //TODO: Finish implementation
        }

        /// <summary>
        ///A test for CalculatePoints
        ///</summary>
        [TestMethod()]
        public void CalculatePointsTest()
        {

        }

        /// <summary>
        /// A terst for GetPoints
        ///</summary>
        [TestMethod()]
        public void GetPointsTest() 
        {
            var userData = new FakeObjectSet<Point>();
            var userInfoId = 1;
            var levelId = 1;
            var expected = new Point
                               {
                                   UserInfoId = userInfoId,
                                   CourseId = 1,
                                   LevelId = levelId,
                                   PointsId = 1,
                                   Points = 50,
                                   Description = "Description"
                               };

            userData.AddObject(expected);

            _mockRepository.Expect(x => x.Points).Return(userData);

            int points = _gameService.GetPoints(userInfoId, levelId);

            Assert.AreEqual(expected.Points,points);

            _mockRepository.VerifyAllExpectations();
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

        public List<Point> GetDummyList()
        {
            return new List<Point>
                (
                new Point[]
                    {
                        new Point
                            {
                                CourseId = 1,
                                LevelId = 1,
                                PointsId = 1,
                                UserInfoId = 1,
                                Description = "You has points",
                                Points = 50
                            },
                        new Point
                            {
                                CourseId = 2,
                                LevelId = 2,
                                PointsId = 2,
                                UserInfoId = 2,
                                Description = "You has points",
                                Points = 350
                            },
                        new Point
                            {
                                CourseId = 3,
                                LevelId = 3,
                                PointsId = 3,
                                UserInfoId = 3,
                                Description = "You has points",
                                Points = 150
                            },
                    }
                );
        }
        public IEnumerable<Tuple<int,UserInfo>> GetTopTenListDummy()
        {
            return null;
        }
    }

}