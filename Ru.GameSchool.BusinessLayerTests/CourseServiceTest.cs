using Ru.GameSchool.BusinessLayer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ru.GameSchool.DataLayer.Repository;
using System.Collections.Generic;
using Rhino.Mocks;
using Ru.GameSchool.BusinessLayerTests.Classes;
using System.Linq;

namespace Ru.GameSchool.BusinessLayerTests
{
    
    
    /// <summary>
    ///This is a test class for CourseServiceTest and is intended
    ///to contain all CourseServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CourseServiceTest
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
        ///A test for GetCoursesByUserInfoId
        ///</summary>
        [TestMethod()]
        public void GetCoursesByUserInfoIdTest()
        {
            var mockRepository = MockRepository.GenerateMock<IGameSchoolEntities>();
            var courseService = new CourseService();
            courseService.SetDatasource(mockRepository);

            //Fake user data
            var userData = new FakeObjectSet<UserInfo>();

            var expected = new UserInfo();
            expected.Fullname = "Davíð Einarsson";
            expected.Email = "davide09@ru.is";
            expected.StatusId = 1;
            expected.Username = "davidein";
            expected.UserInfoId = 1;

            userData.AddObject(expected);

            mockRepository.Expect(x => x.UserInfoes).Return(userData);

            //Fake course data
            var courseData = new FakeObjectSet<Course>();

            var expectedCourse = new Course();
            expectedCourse.CourseId = 1;
            expectedCourse.CreateDateTime = DateTime.Now;
            expectedCourse.CreditAmount = 3;
            expectedCourse.DepartmentId = 1;
            expectedCourse.Description = "Daniel teaches extreme pole fitness programming";
            expectedCourse.Name = "Extreme pole fitness programming";
            expectedCourse.Start = DateTime.Now.AddMonths(-1);
            expectedCourse.Stop = DateTime.Now.AddMonths(2);
            expectedCourse.UserInfoes.Add(expected);

            courseData.AddObject(expectedCourse);

            mockRepository.Expect(x => x.Courses).Return(courseData);

            var courses = courseService.GetCoursesByUserInfoId(expected.UserInfoId);
            
            Assert.AreEqual(courseData.FirstOrDefault().CourseId, courses.FirstOrDefault().CourseId);
            Assert.AreEqual(courses.Count(), 1);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetCoursesByUserInfoId
        ///</summary>
        [TestMethod()]
        public void GetCoursesByUserInfoIdTest_CourseTime()
        {
            var mockRepository = MockRepository.GenerateMock<IGameSchoolEntities>();
            var courseService = new CourseService();
            courseService.SetDatasource(mockRepository);

            //Fake user data
            var userData = new FakeObjectSet<UserInfo>();

            var expected = new UserInfo();
            expected.Fullname = "Davíð Einarsson";
            expected.Email = "davide09@ru.is";
            expected.StatusId = 1;
            expected.Username = "davidein";
            expected.UserInfoId = 1;

            userData.AddObject(expected);

            mockRepository.Expect(x => x.UserInfoes).Return(userData);

            //Fake course data
            var courseData = new FakeObjectSet<Course>();

            var expectedCourse = new Course();
            expectedCourse.CourseId = 1;
            expectedCourse.CreateDateTime = DateTime.Now;
            expectedCourse.CreditAmount = 3;
            expectedCourse.DepartmentId = 1;
            expectedCourse.Description = "Daniel teaches extreme pole fitness programming";
            expectedCourse.Name = "Extreme pole fitness programming";
            expectedCourse.Start = DateTime.Now.AddMonths(-1);
            expectedCourse.Stop = DateTime.Now.AddMonths(2);
            expectedCourse.UserInfoes.Add(expected);

            courseData.AddObject(expectedCourse);

            //Another Course added that is in the future and should not be returned
            expectedCourse = new Course();
            expectedCourse.CourseId = 2;
            expectedCourse.CreateDateTime = DateTime.Now;
            expectedCourse.CreditAmount = 3;
            expectedCourse.DepartmentId = 1;
            expectedCourse.Description = "Daniel teaches extreme pole fitness programming II";
            expectedCourse.Name = "Extreme pole fitness programming II";
            expectedCourse.Start = DateTime.Now.AddMonths(10);
            expectedCourse.Stop = DateTime.Now.AddMonths(20);
            expectedCourse.UserInfoes.Add(expected);

            courseData.AddObject(expectedCourse);

            mockRepository.Expect(x => x.Courses).Return(courseData);

            var courses = courseService.GetCoursesByUserInfoId(expected.UserInfoId);

            Assert.AreEqual(courseData.FirstOrDefault().CourseId, courses.FirstOrDefault().CourseId);
            Assert.AreEqual(courses.Count(), 1);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetCourseGrades
        ///</summary>
        [TestMethod()]
        public void GetCourseGradesTest()
        {
            CourseService target = new CourseService(); // TODO: Initialize to an appropriate value
            int courseId = 0; // TODO: Initialize to an appropriate value
            IEnumerable<CourseGrade> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<CourseGrade> actual;
            actual = target.GetCourseGrades(courseId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetCourseGradeByCourseIdAndUserInfoId
        ///</summary>
        [TestMethod()]
        public void GetCourseGradeByCourseIdAndUserInfoIdTest()
        {
            CourseService target = new CourseService(); // TODO: Initialize to an appropriate value
            int courseId = 0; // TODO: Initialize to an appropriate value
            int userInfoId = 0; // TODO: Initialize to an appropriate value
            CourseGrade expected = null; // TODO: Initialize to an appropriate value
            CourseGrade actual;
            actual = target.GetCourseGradeByCourseIdAndUserInfoId(courseId, userInfoId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
