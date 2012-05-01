using Rhino.Mocks;
using Ru.GameSchool.BusinessLayer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Ru.GameSchool.BusinessLayerTests.Classes;
using Ru.GameSchool.DataLayer.Repository;
using System.Collections.Generic;

namespace Ru.GameSchool.BusinessLayerTests
{
    
    
    /// <summary>
    ///This is a test class for UserServiceTest and is intended
    ///to contain all UserServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserServiceTest
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
        ///A test for GetUser
        ///</summary>
        [TestMethod()]
        public void GetUserTest()
        {
            //Assign
            var mockRepository = MockRepository.GenerateMock<IGameSchoolEntities>();
            var userService = new UserService();
            userService.SetDatasource(mockRepository);

            var userData = new FakeObjectSet<UserInfo>();

            UserInfo expected = new UserInfo();
            expected.Fullname = "Davíð Einarsson";
            expected.Email = "davide09@ru.is";
            expected.StatusId = 1;
            expected.Username = "davidein";
            expected.UserInfoId = 1;

            userData.AddObject(expected);

            mockRepository.Expect(x => x.UserInfoes).Return(userData);

            int userId = 1; 
            
            UserInfo actual;
            actual = userService.GetUser(userId);
            
            Assert.AreEqual(expected.Username, actual.Username);
        }

        /// <summary>
        ///A test for GetUsers
        ///</summary>
        [TestMethod()]
        public void GetUsersTest()
        {
            UserService target = new UserService(); // TODO: Initialize to an appropriate value
            IEnumerable<UserInfo> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<UserInfo> actual;
            actual = target.GetUsers();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Login
        ///</summary>
        [TestMethod()]
        public void LoginTest()
        {
            var mockRepository = MockRepository.GenerateMock<IGameSchoolEntities>();
            var userService = new UserService();
            userService.SetDatasource(mockRepository);

            var userData = new FakeObjectSet<UserInfo>();

            UserInfo expected = new UserInfo();
            expected.Fullname = "Davíð Einarsson";
            expected.Email = "davide09@ru.is";
            expected.StatusId = 1;
            expected.Username = "davidein";
            expected.UserInfoId = 1;
            expected.Password = "Wtf";

            userData.AddObject(expected);

            mockRepository.Expect(x => x.UserInfoes).Return(userData);


            string userName = "davidein";
            string password = "wrongpassword"; 

            UserInfo actual= userService.Login(userName, password);

            Assert.IsNull(actual);

            password = expected.Password;

            actual = userService.Login(userName, password);

            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.Username, actual.Username);
        }

        /// <summary>
        ///A test for UpdateUser
        ///</summary>
        [TestMethod()]
        public void UpdateUserTest()
        {
            UserService target = new UserService(); // TODO: Initialize to an appropriate value
            UserInfo userInfo = null; // TODO: Initialize to an appropriate value
            target.UpdateUser(userInfo);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
