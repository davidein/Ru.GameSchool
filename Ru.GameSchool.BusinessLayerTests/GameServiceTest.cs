using System.ComponentModel;
using Ru.GameSchool.BusinessLayer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ru.GameSchool.DataLayer.Repository;
using System.Collections.Generic;
using Rhino.Mocks;
using System.Linq;
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

            Assert.AreEqual(expected.Points, points);

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
        public void GetTopTenList_UserInfoCoursePoint_IEnumerableTupleIntUserInfo1()
        {
            var userData = new FakeObjectSet<UserInfo>();
            var courseData = new FakeObjectSet<Course>();
            var pointData = new FakeObjectSet<Point>();

            var userInfoId = 1;
            var courseId = 1;
            var departmentId = 1;
            var levelId = 1;
            #region data
            
            UserInfo user = new UserInfo { Fullname = "Davíð Einarsson", Email = "davide09@ru.is", StatusId = 1, Username = "davidein", UserInfoId = 1, Password = "Wtf" };
            Course course = new Course { CourseId = courseId, Name = "Vefforritun I", CreateDateTime = DateTime.Now, Identifier = "VEFF", Start = DateTime.Now, Stop = DateTime.Now.AddDays(28), DepartmentId = departmentId, CreditAmount = 6, Description = "Lýsing á veff" };
            Point point1 = new Point { CourseId = courseId, Description = "Þú fékkst stig", LevelId = levelId, UserInfoId = userInfoId, PointsId = 1, Points = 5 };
            Point point2 = new Point { CourseId = courseId, Description = "Þú fékkst stig 2", LevelId = levelId, UserInfoId = userInfoId, PointsId = 2, Points = 25 };
            Point point3 = new Point { CourseId = courseId, Description = "Þú fékkst stig 3", LevelId = levelId, UserInfoId = userInfoId, PointsId = 3, Points = 55 };

            course.UserInfoes.Add(user);
            course.Points.Add(point1);
            course.Points.Add(point2);
            course.Points.Add(point3);
            user.Points.Add(point1);
            user.Points.Add(point2);
            user.Points.Add(point3);

            userData.AddObject(user);
            courseData.AddObject(course);
            pointData.AddObject(point1);
            pointData.AddObject(point2);
            pointData.AddObject(point3);
            #endregion
            _mockRepository.Expect(x => x.UserInfoes).Return(userData);
            _mockRepository.Expect(x => x.Courses).Return(courseData);
            _mockRepository.Expect(x => x.Points).Return(pointData);

            var topTen = _gameService.GetTopTenList(courseId);

            var actualUser = topTen.Select(x => x.Item2).FirstOrDefault();

            Assert.AreSame(actualUser, user);
            Assert.AreEqual(actualUser.Points, user.Points);
            Assert.AreEqual(course.Name, user.Courses.Select(x => x.Name).FirstOrDefault());
            Assert.AreEqual(user.Username, actualUser.Username);
        }

        /// <summary>
        ///A test for GetTopTenList
        ///</summary>
        [TestMethod()]
        public void GetTopTenList_UserInfoCoursePoint_IEnumerableTupleIntUserInfo()
        {
            // lysandinafn_input_expectedreturn
            var userData = new FakeObjectSet<UserInfo>();
            var courseData = new FakeObjectSet<Course>();
            var pointData = new FakeObjectSet<Point>();
            
            var userInfoId1 = 1;
            var userInfoId2 = 2;
            var userInfoId3 = 3;

            var courseId = 1;
            var departmentId = 1;
            var levelId = 1;

            #region data
            
            Course course = new Course { CourseId = courseId, Name = "Vefforritun I", CreateDateTime = DateTime.Now, Identifier = "VEFF", Start = DateTime.Now, Stop = DateTime.Now.AddDays(28), DepartmentId = departmentId, CreditAmount = 6, Description = "Lýsing á veff" };

            UserInfo user1 = new UserInfo { Fullname = "Davíð Einarsson", Email = "davide09@ru.is", StatusId = 1, Username = "davidein", UserInfoId = userInfoId1, Password = "Wtf" };
            Point point1_1 = new Point { CourseId = courseId, Description = "Þú fékkst stig", LevelId = levelId, UserInfoId = userInfoId1, PointsId = 1, Points = 5 };
            Point point2_1 = new Point { CourseId = courseId, Description = "Þú fékkst stig 2", LevelId = levelId, UserInfoId = userInfoId1, PointsId = 2, Points = 25 };
            Point point3_1 = new Point { CourseId = courseId, Description = "Þú fékkst stig 3", LevelId = levelId, UserInfoId = userInfoId1, PointsId = 3, Points = 55 };

            course.UserInfoes.Add(user1);

            course.Points.Add(point1_1);
            course.Points.Add(point2_1);
            course.Points.Add(point3_1);

            user1.Points.Add(point1_1);
            user1.Points.Add(point2_1);
            user1.Points.Add(point3_1);

            userData.AddObject(user1);
            courseData.AddObject(course);
            pointData.AddObject(point1_1);
            pointData.AddObject(point2_1);
            pointData.AddObject(point3_1);

            UserInfo user2 = new UserInfo { Fullname = "Davíð Einarsson", Email = "davide09@ru.is", StatusId = 1, Username = "davidein", UserInfoId = userInfoId2, Password = "Wtf" };
            Point point1_2 = new Point { CourseId = courseId, Description = "Þú fékkst stig", LevelId = levelId, UserInfoId = userInfoId2, PointsId = 4, Points = 5 };
            Point point2_2 = new Point { CourseId = courseId, Description = "Þú fékkst stig 2", LevelId = levelId, UserInfoId = userInfoId2, PointsId = 5, Points = 25 };
            Point point3_2 = new Point { CourseId = courseId, Description = "Þú fékkst stig 3", LevelId = levelId, UserInfoId = userInfoId2, PointsId = 6, Points = 55 };
            Point point4_2 = new Point { CourseId = courseId, Description = "Þú fékkst stig", LevelId = levelId, UserInfoId = userInfoId2, PointsId = 7, Points = 5 };
            Point point5_2 = new Point { CourseId = courseId, Description = "Þú fékkst stig 2", LevelId = levelId, UserInfoId = userInfoId2, PointsId = 8, Points = 25 };
            Point point6_2 = new Point { CourseId = courseId, Description = "Þú fékkst stig 3", LevelId = levelId, UserInfoId = userInfoId2, PointsId = 9, Points = 55 };
            
            course.UserInfoes.Add(user2);
            
            course.Points.Add(point1_2);
            course.Points.Add(point2_2);
            course.Points.Add(point3_2);
            course.Points.Add(point4_2);
            course.Points.Add(point5_2);
            course.Points.Add(point6_2);

            user2.Points.Add(point1_2);
            user2.Points.Add(point2_2);
            user2.Points.Add(point3_2);
            user2.Points.Add(point4_2);
            user2.Points.Add(point5_2);
            user2.Points.Add(point6_2);

            userData.AddObject(user2);
            pointData.AddObject(point1_2);
            pointData.AddObject(point2_2);
            pointData.AddObject(point3_2);
            pointData.AddObject(point4_2);
            pointData.AddObject(point5_2);
            pointData.AddObject(point6_2);

            UserInfo user3 = new UserInfo { Fullname = "Davíð Einarsson", Email = "davide09@ru.is", StatusId = 1, Username = "davidein", UserInfoId = userInfoId3, Password = "Wtf" };
            Point point1_3 = new Point { CourseId = courseId, Description = "Þú fékkst stig", LevelId = levelId, UserInfoId = userInfoId3, PointsId = 10, Points = 5 };
            Point point2_3 = new Point { CourseId = courseId, Description = "Þú fékkst stig 2", LevelId = levelId, UserInfoId = userInfoId3, PointsId = 11, Points = 25 };
            Point point3_3 = new Point { CourseId = courseId, Description = "Þú fékkst stig 3", LevelId = levelId, UserInfoId = userInfoId3, PointsId = 12, Points = 55 };
            Point point4_3 = new Point { CourseId = courseId, Description = "Þú fékkst stig", LevelId = levelId, UserInfoId = userInfoId3, PointsId = 13, Points = 5 };
            Point point5_3 = new Point { CourseId = courseId, Description = "Þú fékkst stig 2", LevelId = levelId, UserInfoId = userInfoId3, PointsId = 14, Points = 25 };
            Point point6_3 = new Point { CourseId = courseId, Description = "Þú fékkst stig 3", LevelId = levelId, UserInfoId = userInfoId3, PointsId = 15, Points = 55 };
            Point point7_3 = new Point { CourseId = courseId, Description = "Þú fékkst stig", LevelId = levelId, UserInfoId = userInfoId3, PointsId = 16, Points = 5 };
            Point point8_3 = new Point { CourseId = courseId, Description = "Þú fékkst stig 2", LevelId = levelId, UserInfoId = userInfoId3, PointsId = 17, Points = 25 };
            Point point9_3 = new Point { CourseId = courseId, Description = "Þú fékkst stig 3", LevelId = levelId, UserInfoId = userInfoId3, PointsId = 18, Points = 55 };
            Point point10_3 = new Point { CourseId = courseId, Description = "Þú fékkst stig", LevelId = levelId, UserInfoId = userInfoId3, PointsId = 19, Points = 5 };
            Point point11_3 = new Point { CourseId = courseId, Description = "Þú fékkst stig 2", LevelId = levelId, UserInfoId = userInfoId3, PointsId = 20, Points = 25 };
            Point point12_3 = new Point { CourseId = courseId, Description = "Þú fékkst stig 3", LevelId = levelId, UserInfoId = userInfoId3, PointsId = 21, Points = 55 };

            course.UserInfoes.Add(user3);

            course.Points.Add(point1_3);
            course.Points.Add(point2_3);
            course.Points.Add(point3_3);
            course.Points.Add(point4_3);
            course.Points.Add(point5_3);
            course.Points.Add(point6_3);
            course.Points.Add(point7_3);
            course.Points.Add(point8_3);
            course.Points.Add(point9_3);
            course.Points.Add(point10_3);
            course.Points.Add(point11_3);
            course.Points.Add(point12_3);

            user3.Points.Add(point1_3);
            user3.Points.Add(point2_3);
            user3.Points.Add(point3_3);
            user3.Points.Add(point4_3);
            user3.Points.Add(point5_3);
            user3.Points.Add(point6_3);
            user3.Points.Add(point7_3);
            user3.Points.Add(point8_3);
            user3.Points.Add(point9_3);
            user3.Points.Add(point10_3);
            user3.Points.Add(point11_3);
            user3.Points.Add(point12_3);

            userData.AddObject(user3);
            pointData.AddObject(point1_3);
            pointData.AddObject(point2_3);
            pointData.AddObject(point3_3);
            pointData.AddObject(point4_3);
            pointData.AddObject(point5_3);
            pointData.AddObject(point6_3);
            pointData.AddObject(point7_3);
            pointData.AddObject(point8_3);
            pointData.AddObject(point9_3);
            pointData.AddObject(point10_3);
            pointData.AddObject(point11_3);
            pointData.AddObject(point12_3);

            #endregion

            _mockRepository.Expect(x => x.UserInfoes).Return(userData);
            _mockRepository.Expect(x => x.Courses).Return(courseData);
            _mockRepository.Expect(x => x.Points).Return(pointData);

            var topTen = _gameService.GetTopTenList(courseId);

            var expectedUser1 = topTen.Select(x => x.Item2).Where(d => d.UserInfoId == userInfoId1).FirstOrDefault();
            var expectedUser2 = topTen.Select(x => x.Item2).Where(d => d.UserInfoId == userInfoId2).FirstOrDefault();
            var expectedUser3 = topTen.Select(x => x.Item2).Where(d => d.UserInfoId == userInfoId3).FirstOrDefault();

            Assert.IsTrue(user1.Points.Select(x => x.Points).FirstOrDefault() > 0);
            Assert.IsTrue(user2.Points.Select(x => x.Points).FirstOrDefault() > 0);
            Assert.IsTrue(user3.Points.Select(x => x.Points).FirstOrDefault() > 0);

            Assert.IsNotNull(expectedUser1.Points);
            Assert.IsNotNull(expectedUser2.Points);
            Assert.IsNotNull(expectedUser3.Points);
            Assert.IsNotNull(course);
            Assert.IsNotNull(course.Points);
            Assert.IsNotNull(course.UserInfoes);

            Assert.IsNotNull(user1.Points);
            Assert.IsNotNull(user1.Points);
            Assert.IsNotNull(user1.Points);
            
            Assert.AreSame(expectedUser1, user1);
            Assert.AreSame(expectedUser2, user2);
            Assert.AreSame(expectedUser3, user3);

            Assert.AreNotSame(expectedUser1,user3);

            Assert.AreEqual(expectedUser1.Username,user1.Username);
            Assert.AreEqual(expectedUser2.Username, user3.Username);
            Assert.AreEqual(expectedUser3.Username, user3.Username);
        }
    }
}