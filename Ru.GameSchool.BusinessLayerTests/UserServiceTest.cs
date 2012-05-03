using System.Linq;
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
        private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
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

            var expected = new UserInfo();
            expected.Fullname = "Davíð Einarsson";
            expected.Email = "davide09@ru.is";
            expected.StatusId = 1;
            expected.Username = "davidein";
            expected.UserInfoId = 1;

            userData.AddObject(expected);

            mockRepository.Expect(x => x.UserInfoes).Return(userData);

            int userId = 1;

            UserInfo actual = userService.GetUser(userId);
            
            Assert.IsNotNull( actual);

            Assert.AreEqual(expected.Username, actual.Username);
            
        
            mockRepository.VerifyAllExpectations(); // Make sure everything was correctly called.
        }

        /// <summary>
        ///A test for GetUsers
        ///</summary>
        [TestMethod()]
        public void GetUsersTest()
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

            var users = userService.GetUsers();

            Assert.AreEqual(users.Count(), 1);

            mockRepository.VerifyAllExpectations(); // Make sure everything was called correctly.
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

            mockRepository.VerifyAllExpectations(); // Make sure everything was called correctly.
        }

        /// <summary>
        ///A test for UpdateUser
        ///</summary>
        [TestMethod()]
        public void UpdateUserTest()
        {
            var mockRepository = MockRepository.GenerateMock<IGameSchoolEntities>();
            var userService = new UserService();
            userService.SetDatasource(mockRepository);

            UserInfo userInfo = new UserInfo();
            userInfo.Fullname = "Davíð Einarsson";
            userInfo.Email = "davide09@ru.is";
            userInfo.StatusId = 1;
            userInfo.Username = "davidein";
            userInfo.UserInfoId = 1;
            userInfo.Password = "Wtf";

            mockRepository.Expect(x => x.AttachTo("UserInfo", userInfo));
            mockRepository.Expect(x => x.SaveChanges()).Return(1);
                
            userService.UpdateUser(userInfo);

            mockRepository.VerifyAllExpectations(); // Make sure everything was called correctly.
        }
    }
}
