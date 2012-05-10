using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Ru.GameSchool.BusinessLayer.Services;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.BusinessLayer.Enums;
using Ru.GameSchool.BusinessLayerTests.Classes;
using Ru.GameSchool.Utilities;
using UserType = Ru.GameSchool.BusinessLayer.Enums.UserType;

namespace Ru.GameSchool.BusinessLayerTests
{


    /// <summary>
    ///This is a test class for LevelServiceTest and is intended
    ///to contain all LevelServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LevelServiceTest
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

        private IGameSchoolEntities _mockRepository;
        private LevelService _levelService;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _mockRepository = MockRepository.GenerateMock<IGameSchoolEntities>();
            _levelService = new LevelService();
            _levelService.SetDatasource(_mockRepository);
        }

        #endregion

        /// <summary>
        ///A test for CreateLevel
        ///</summary>
        [TestMethod()]
        public void CreateLevelTest()
        {
            var levelData = new FakeObjectSet<Level>();

            Level expected = new Level();
            expected.LevelId = 1;
            expected.Name = "Borð 1";
            expected.CourseId = 1;
            expected.Start = DateTime.Now;
            expected.Stop = DateTime.Now.AddDays(7);



            levelData.AddObject(expected);

            _mockRepository.Expect(x => x.Levels).Return(levelData);

            //_mockRepository.Expect(x => x.AttachTo("Levels",expected));
            _mockRepository.Expect(x => x.SaveChanges()).Return(1);

            _levelService.CreateLevel(expected);

            var actual = _levelService.GetLevel(1);

            Assert.AreEqual(actual.Name, expected.Name);


            _mockRepository.VerifyAllExpectations(); // Make sure everything was called correctly.
        }

        /// <summary>
        ///A test for CreateLevelExamResult
        ///</summary>
        [TestMethod()]
        public void CreateLevelExamResultTest()
        {
            // ÞARF AÐ LAGFÆRA
            var levelExamId = 1;
            var userInfoId = 1;
            var levelId = 1;
            var levelExamResult = new LevelExamResult
                                      {
                                          Grade = 5,
                                          LevelExamId = levelExamId,
                                          UserInfoId = userInfoId,
                                          LevelExam = new LevelExam
                                                          {
                                                              CreateDateTime = DateTime.Now,
                                                              Description = "Lýsing",
                                                              Name = "Nafn",
                                                              GradePercentageValue = 5,
                                                              LevelExamId = levelExamId,
                                                              LevelId = levelId
                                                          },
                                          UserInfo = new UserInfo
                                                         {
                                                             CreateDateTime = DateTime.Now,
                                                             UserInfoId = userInfoId,
                                                             Password = "ww",
                                                             Fullname = "Ólafur",
                                                             Email = "óli@ru.is",
                                                             StatusId = 1,
                                                             Username = "Username"
                                                         }
                                      };
            var levelExamResultData = new FakeObjectSet<LevelExamResult>();
            levelExamResultData.AddObject(levelExamResult);


            _mockRepository.Expect(x => x.LevelExamResults).Return(levelExamResultData);
            _mockRepository.Expect(x => x.SaveChanges()).Return(1);

            _levelService.CreateLevelExamResult(levelExamResult);
            _mockRepository.VerifyAllExpectations();

        }

        /// <summary>
        ///A test for GetLevelProject
        ///</summary>
        [TestMethod()]
        public void GetLevelProjectByUserIdTest_UserInfoCourseLevelLevelProject_CollectionOfLevelProjects()
        {
            var userInfoId = 7;

            var userData = new FakeObjectSet<UserInfo>();
            var courseData = new FakeObjectSet<Course>();
            var levelProjectData = new FakeObjectSet<LevelProject>();
            var levelData = new FakeObjectSet<Level>();

            LevelProject levelProject1 = new LevelProject { Stop = DateTime.Now.AddDays(7), Start = DateTime.Now, Name = "Verkefni", LevelProjectId = 1, ContentID = "Content", Description = "Lýsing", GradePercentageValue = 5, LevelId = 1 };
            LevelProject levelProject2 = new LevelProject { Stop = DateTime.Now.AddDays(7), Start = DateTime.Now, Name = "Verkefni2", LevelProjectId = 2, ContentID = "Conten2t", Description = "Lýsing2", GradePercentageValue = 1, LevelId = 1 };
            UserInfo user1 = new UserInfo { Fullname = "Davíð Einarsson", Email = "davide09@ru.is", StatusId = 1, Username = "davidein", UserInfoId = userInfoId, Password = "Wtf" };
            Course course = new Course { CourseId = 1, Name = "Vefforritun I", CreateDateTime = DateTime.Now, Identifier = "VEFF", Start = DateTime.Now, Stop = DateTime.Now.AddDays(28), DepartmentId = 1, CreditAmount = 6, Description = "Lýsing á veff" };
            Level lvl = new Level { CourseId = 1, CreateDateTime = DateTime.Now, LevelId = 1, Name = "Level", Start = DateTime.Now, Stop = DateTime.Now.AddDays(7), OrderNum = 5 };

            course.Levels.Add(lvl);
            user1.Courses.Add(course);
            lvl.LevelProjects.Add(levelProject1);
            lvl.LevelProjects.Add(levelProject2);

            userData.AddObject(user1);
            courseData.AddObject(course);
            levelProjectData.AddObject(levelProject1);
            levelProjectData.AddObject(levelProject2);
            levelData.AddObject(lvl);

            _mockRepository.Expect(x => x.UserInfoes).Return(userData);
            _mockRepository.Expect(x => x.Courses).Return(courseData);
            _mockRepository.Expect(x => x.LevelProjects).Return(levelProjectData);
            _mockRepository.Expect(x => x.Levels).Return(levelData);

            var query = _levelService.GetLevelProjectsByUserId(userInfoId);

            var expectedUser = query.SelectMany(x => x.Level.Course.UserInfoes).FirstOrDefault();
            var expectedFirstLevelProject = query.Where(s => s.LevelProjectId == 1).FirstOrDefault();
            var expectedSecondLevelProject = query.Where(s => s.LevelProjectId == 2).FirstOrDefault();
            var expectedLevel = query.Where(x => x.Level.LevelId == 1).Select(x => x.Level).FirstOrDefault();
            var expectedCourse =
                query.Where(x => x.Level.Course.CourseId == 1).Select(x => x.Level.Course).FirstOrDefault();

            Assert.AreEqual(expectedCourse.Description, course.Description);
            Assert.AreEqual(expectedLevel.Name,lvl.Name);
            Assert.AreEqual(expectedSecondLevelProject.Name, levelProject2.Name);
            Assert.AreEqual(expectedFirstLevelProject.Name,levelProject1.Name);
            Assert.AreEqual(expectedUser.Fullname,user1.Fullname);
            Assert.AreEqual(expectedUser.Fullname, user1.Fullname);
            Assert.AreEqual(expectedUser.Fullname, user1.Fullname);
            Assert.AreEqual(expectedUser.Fullname, user1.Fullname);

            
        }



        /// <summary>
        ///A test for UpdateLevelMaterial
        ///</summary>
        [TestMethod()]
        public void UpdateLevelMaterialTest()
        {
            var materialData = new FakeObjectSet<LevelMaterial>();

            LevelMaterial expected = new LevelMaterial();
            expected.Title = "Fyrstu skilaboðin";
            expected.Description = "Ekki sérlega merkileg skilaboð";
            expected.ContentId = System.Guid.NewGuid();


            materialData.AddObject(expected);

            _mockRepository.Expect(x => x.LevelMaterials).Return(materialData);

            //_mockRepository.Expect(x => x.AttachTo("UserInfo", userInfo));
            _mockRepository.Expect(x => x.SaveChanges()).Return(1);

            _levelService.UpdateLevelMaterial(expected);


            _mockRepository.VerifyAllExpectations(); // Make sure everything was called correctly.
        }


        /// <summary>
        ///A test for AnswerLevelExamQuestion
        ///</summary>
        [TestMethod()]
        public void AnswerLevelExamQuestionTest()
        {
            /* Setup user data */
            int userInfoId = 1;

            var userData = new FakeObjectSet<UserInfo>();

            var user = UserServiceTest.GetUser(userInfoId, UserType.Student);

            userData.AddObject(user);

            /* Setup levelexamanswer data */
            var answerData = new FakeObjectSet<LevelExamAnswer>();
            
            var levelExamAnswer = new LevelExamAnswer();
            levelExamAnswer.LevelExamAnswerId = 1;
            levelExamAnswer.LevelExamQuestionId = 1;
            levelExamAnswer.UserInfoes.Add(user);

            var levelExamAnswerNumberTwo = new LevelExamAnswer();
            levelExamAnswerNumberTwo.LevelExamAnswerId = 2;
            levelExamAnswerNumberTwo.LevelExamQuestionId = 1;

            answerData.AddObject(levelExamAnswer);
            answerData.AddObject(levelExamAnswerNumberTwo);

            /* Setup levelexamquestion data*/
            var questionData = new FakeObjectSet<LevelExamQuestion>();

            var levelExamQuestion = new LevelExamQuestion();
            levelExamQuestion.LevelExamQuestionId = 1;
            levelExamQuestion.LevelExamAnswers.Add(levelExamAnswer);
            levelExamQuestion.LevelExamAnswers.Add(levelExamAnswerNumberTwo);

            questionData.AddObject(levelExamQuestion);

            /* Setup the mock expectations */
            _mockRepository.Expect(x => x.UserInfoes).Return(userData);
            _mockRepository.Expect(x => x.LevelExamAnswers).Return(answerData);
            _mockRepository.Expect(x => x.LevelExamQuestions).Return(questionData);
            _mockRepository.Expect(x => x.SaveChanges()).Return(1);

            /* Test the business logic */
            _levelService.AnswerLevelExamQuestion(1, userInfoId);
            _levelService.AnswerLevelExamQuestion(2,userInfoId);

            /* Verify all the mock calls */
            _mockRepository.VerifyAllExpectations();
        }
    }
}
