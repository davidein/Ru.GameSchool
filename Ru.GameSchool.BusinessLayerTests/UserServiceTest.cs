using System.Linq;
using Rhino.Mocks;
using Ru.GameSchool.BusinessLayer.Enums;
using Ru.GameSchool.BusinessLayer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ru.GameSchool.BusinessLayerTests.Classes;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.Utilities;
using UserType = Ru.GameSchool.BusinessLayer.Enums.UserType;

namespace Ru.GameSchool.BusinessLayerTests
{
    
    
    /// <summary>
    ///This is a test class for UserServiceTest and is intended
    ///to contain all UserServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserServiceTest
    {
        //private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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

        private IGameSchoolEntities _mockRepository;
        private UserService _userService;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _mockRepository = MockRepository.GenerateMock<IGameSchoolEntities>();
            _userService = new UserService();
            _userService.SetDatasource(_mockRepository);
        }
        #endregion


        /// <summary>
        ///A test for GetUser
        ///</summary>
        [TestMethod]
        public void GetUserTest()
        {
            //Assign
            var userData = new FakeObjectSet<UserInfo>();

            var expected = new UserInfo();
            expected.Fullname = "Davíð Einarsson";
            expected.Email = "davide09@ru.is";
            expected.StatusId = 1;
            expected.Username = "davidein";
            expected.UserInfoId = 1;

            userData.AddObject(expected);

            _mockRepository.Expect(x => x.UserInfoes).Return(userData);

            int userId = 1;

            UserInfo actual = _userService.GetUser(userId);
            
            Assert.IsNotNull( actual);

            Assert.AreEqual(expected.Username, actual.Username);
            
        
            _mockRepository.VerifyAllExpectations(); // Make sure everything was correctly called.
        }

        /// <summary>
        ///A test for GetUsers
        ///</summary>
        [TestMethod]
        public void GetUsersTest()
        {
            var userData = new FakeObjectSet<UserInfo>();

            UserInfo expected = new UserInfo();
            expected.Fullname = "Davíð Einarsson";
            expected.Email = "davide09@ru.is";
            expected.StatusId = 1;
            expected.Username = "davidein";
            expected.UserInfoId = 1;
            expected.Password = "Wtf";

            userData.AddObject(expected);

            _mockRepository.Expect(x => x.UserInfoes).Return(userData);

            var users = _userService.GetUsers();

            Assert.AreEqual(users.Count(), 1);

            _mockRepository.VerifyAllExpectations(); // Make sure everything was called correctly.
        }

        /// <summary>
        ///A test for Login
        ///</summary>
        [TestMethod]
        public void LoginTest()
        {
            var userData = new FakeObjectSet<UserInfo>();

            string originalPassword = "Wtf";

            UserInfo expected = new UserInfo();
            expected.Fullname = "Davíð Einarsson";
            expected.Email = "davide09@ru.is";
            expected.StatusId = (int)UserStatus.Active;
            expected.Username = "davidein";
            expected.UserInfoId = 1;
            expected.Password = PasswordUtilities.ComputeHash(originalPassword);

            userData.AddObject(expected);

            _mockRepository.Expect(x => x.UserInfoes).Return(userData);


            string userName = "davidein";
            string password = "wrongpassword"; 

            UserInfo actual= _userService.Login(userName, password,"::1");

            Assert.IsNull(actual);

            password = originalPassword;

            actual = _userService.Login(userName, password,"::1");

            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.Username, actual.Username);

            _mockRepository.VerifyAllExpectations(); // Make sure everything was called correctly.
        }

        /// <summary>
        ///A test for UpdateUser
        ///</summary>
        [TestMethod]
        public void UpdateUserTest()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.Fullname = "Davíð Einarsson";
            userInfo.Email = "davide09@ru.is";
            userInfo.StatusId = 1;
            userInfo.Username = "davidein";
            userInfo.UserInfoId = 1;
            userInfo.Password = "Wtf";

            _mockRepository.Expect(x => x.SaveChanges()).Return(1);
                
            _userService.UpdateUser(userInfo);

            _mockRepository.VerifyAllExpectations(); // Make sure everything was called correctly.
        }

        #region Helper functions
        public static UserInfo GetUser(int userInfoId, UserType userType)
        {
            UserInfo userInfo = new UserInfo();
            userInfo.Fullname = "Davíð Einarsson";
            userInfo.Email = "davide09@ru.is";
            userInfo.StatusId = (int) userType;
            userInfo.Username = "davidein";
            userInfo.UserInfoId = userInfoId;
            userInfo.Password = "Wtf";

            return userInfo;
        }
        #endregion
    }
}
