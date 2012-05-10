using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using Ru.GameSchool.BusinessLayer.Exceptions;
using Ru.GameSchool.DataLayer;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.BusinessLayer.Enums;

namespace Ru.GameSchool.BusinessLayer.Services
{ // vantar, öll gert fyrir level
    /// <summary>
    /// Service class that abstracts the interraction around the level entity with the data layer.
    /// </summary> 
    public class LevelService : BaseService
    {
        /// <summary>
        /// Persist a level instance object to the datasource.
        /// </summary>
        /// <param name="level">Level instance to add.</param>
        public void CreateLevel(Level level)
        {
            if (level != null)
            {
                GameSchoolEntities.Levels.AddObject(level);
                Save();
            }
        }
        /// <summary>
        /// Gets a instance of Level from the datasource. 
        /// </summary>
        /// <param name="levelId">The id of a level object to get.</param>
        /// <returns>A new level instance or null</returns>
        public Level GetLevel(int levelId)
        {
            if (0 > levelId)
            {
                return null;
            }
            var query = GameSchoolEntities.Levels.Where(x => x.LevelId == levelId);

            var level = query.FirstOrDefault();

            if (level == null)
            {
                return null;
            }

            return level;
        }


        ////Delete?
        
        public IEnumerable<Level> GetLevels()
        {
            return GameSchoolEntities.Levels;
        }


        /// <summary>
        /// Returns a collection of level instances
        /// </summary>
        /// <returns>IEnumerable of level instances.</returns>
        public IEnumerable<Level> GetLevels(int courseId)
        {
            return GameSchoolEntities.Levels.Where(x => x.CourseId == courseId);
        }

        /// <summary>
        /// Update a level object with new changes and persist it to the datasource.
        /// </summary>
        /// <param name="level">Level instance with updated values.</param>
        public void UpdateLevel(Level level)
        {
            Save();
        }

        /// <summary>
        /// Persist a levelexam instance object to the datasource.
        /// </summary>
        /// <param name="levelExam">A levelexam persist.</param>
        public void CreateLevelExam(LevelExam levelExam)
        {
            if (levelExam != null)
            {
                levelExam.CreateDateTime = DateTime.Now;

                GameSchoolEntities.LevelExams.AddObject(levelExam);
                Save();
            }
        }

        /// <summary>
        /// Get a level exam by LevelExamId.
        /// </summary>
        /// <param name="levelExamId"></param>
        /// <returns></returns>
        public LevelExam GetLevelExam(int levelExamId)
        {
            if (levelExamId < 0)
            {
                return null;
            }
            var query =
                GameSchoolEntities.LevelExams.Where(le => le.LevelExamId == levelExamId).Include("LevelExamQuestions");

            var levelExam = query.FirstOrDefault();

            if (levelExam == null)
            {
                return null;
            }

            return levelExam;
        }

        /// <summary>
        /// Gets a level exam by CourseId and UserInfoId.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userInfoId"></param>
        /// <returns>IEnumerable of LevelExams</returns>
        public IEnumerable<LevelExam> GetLevelExamsByCourseId(int courseId, int userInfoId)
        {
            var list = GameSchoolEntities.LevelExams.Where(x => x.Level.CourseId == courseId);
            var exams = list.Where(x => x.Level.Course.UserInfoes.Where(y => y.UserInfoId == userInfoId).Count() > 0);

            var user = GameSchoolEntities.UserInfoes.Where(x => x.UserInfoId == userInfoId).Single();
            if (user.UserTypeId != (int)Enums.UserType.Teacher)
                exams = exams.Where(x => x.Start <= DateTime.Now);

            return exams;
        }

        /// <summary>
        /// Gets a level exam by LevelId and UserInfoId.
        /// </summary>
        /// <param name="levelId"></param>
        /// <param name="userInfoId"></param>
        /// <returns>IEnumerable of LevelExams</returns>
        public IEnumerable<LevelExam> GetLevelExamsByLevelId(int levelId, int userInfoId)
        {
            var list = GameSchoolEntities.LevelExams.Where(x => x.LevelId == levelId);
            var exams = list.Where(x => x.Level.Course.UserInfoes.Where(y => y.UserInfoId == userInfoId).Count() > 0);

            return exams;
        }

        /// <summary>
        /// Updates a levelexam.
        /// </summary>
        /// <param name="levelExam">Levelexam instance to update.</param>
        public void UpdateLevelExam(LevelExam levelExam)
        {
            if (levelExam != null)
            {
                Save();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelProject"></param>
        public void CreateLevelProject(LevelProject levelProject)
        {
            if (levelProject != null)
            {
                GameSchoolEntities.LevelProjects.AddObject(levelProject);
                Save();
            }
        }

        public LevelProject GetLevelProject(int levelProjectId)
        {
            if (levelProjectId < 0)
            {
                return null;
            }

            var query = GameSchoolEntities.LevelProjects.Where(l => l.LevelProjectId == levelProjectId);

            var levelProject = query.FirstOrDefault();

            if (levelProject == null)
            {
                return null;
            }
            return levelProject;
        }

        public IEnumerable<LevelProject> GetUserLevelProject(int? userInfoId)
        {
            return (userInfoId.HasValue && userInfoId.Value > 0) ? GameSchoolEntities.GetAllUserLevelProjects(userInfoId.Value) : null;
        }

        public IEnumerable<LevelProject> GetLevelProjects()
        {
            return GameSchoolEntities.LevelProjects;
        }

        public IEnumerable<LevelProject> GetLevelProjectsByCourseId(int courseId)
        {
            if (0 > courseId)
            {
                return null;
            }
            var query = GameSchoolEntities.Levels.Where(c => c.CourseId == courseId);

            var levelProjects = query.SelectMany(x => x.LevelProjects)
                                     .AsEnumerable();

            if (levelProjects == null)
            {
                return null;
            }

            return levelProjects;
        }

        public bool HasAccess(int levelId, int userInfoId)
        {
            if ((levelId = userInfoId) > 0)
            {
                var levelQuery = GameSchoolEntities.Levels.Where(l => l.LevelId == levelId).SingleOrDefault();

                if (levelQuery.Course.UserInfoes.Where(x => x.UserInfoId == userInfoId).Count() > 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelProject"></param>
        public void UpdateLevelProject(LevelProject levelProject)
        {
            if (levelProject != null)
            {
                var levelProjToUpdate =
                    GameSchoolEntities.LevelProjects.Where(s => s.LevelProjectId == levelProject.LevelProjectId).
                        FirstOrDefault();

                levelProjToUpdate.ContentID = levelProject.ContentID;
                levelProjToUpdate.Description = levelProject.Description;
                levelProjToUpdate.GradePercentageValue = levelProject.GradePercentageValue;
                levelProjToUpdate.Name = levelProject.Name;
                levelProjToUpdate.Start = levelProject.Start;
                levelProjToUpdate.Stop = levelProject.Stop;
                levelProjToUpdate.UserFeedback = levelProject.UserFeedback;
                Save();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelProject"></param>
        public void UpdateLevelProjectFromResult(LevelProject levelProject, int userInfoId)
        {
            if (levelProject != null)
            {
                if (!(levelProject.Stop > DateTime.Now))
                {
                    var levelProjToUpdate = GetLevelProject(levelProject.LevelProjectId);
                    levelProjToUpdate.ContentID = levelProject.ContentID;
                    levelProjToUpdate.UserFeedback = levelProject.UserFeedback;
                    levelProjToUpdate.LevelProjectResults.Add(levelProject.LevelProjectResults.ElementAtOrDefault(0));
                    
                    int points = 10;


                    var query = GameSchoolEntities.UserInfoes.Where(s => s.UserInfoId == userInfoId).FirstOrDefault();
                    if (query != null) // sernda notification á kennara
                    {
                        var userInCourse =
                            query.Username;
                        var teacher =
                            GameSchoolEntities.Courses.SelectMany(
                                x => x.UserInfoes.Where(d => d.UserInfoId != userInfoId && d.UserTypeId == 2)).
                                FirstOrDefault();
                        ExternalNotificationContainer.CreateNotification(
                            string.Format("Nemandi {0} hefur skilað verkefni {1} í áfanga {2}",
                                          userInCourse, levelProjToUpdate.Name, levelProjToUpdate.Level.Course.Name),
                            string.Format("/Project/Index/{0}", levelProjToUpdate.Level.CourseId),
                            teacher.UserInfoId);
                    }


                    ExternalNotificationContainer.CreateNotification(string.Format("Þú hefur fengið {0} stig fyrir að skila verkefni \"{1}\"",
                        points, levelProjToUpdate.Name), string.Format("/Project/Index/{0}", levelProjToUpdate.Level.CourseId), userInfoId);

                    ExternalPointContainer.AddPointsToLevel(userInfoId, levelProjToUpdate.LevelId, points,
                                                            string.Format("Þú hefur fengið {0} stig fyrir verkefnið \"{1}\".",
                                                                          points, levelProjToUpdate.Name));

                    Save();
                }
            }
        }

        /// <summary>
        /// Creates a level exam question.
        /// </summary>
        /// <param name="levelExamQuestion"></param>
        public void CreateLevelExamQuestion(LevelExamQuestion levelExamQuestion)
        {
            if (levelExamQuestion != null)
            {
                GameSchoolEntities.LevelExamQuestions.AddObject(levelExamQuestion);
                Save();
            }
        }

        /// <summary>
        /// Creates a LevelExamAnswer
        /// </summary>
        /// <param name="levelExamAnswer"></param>
        public void CreateLevelExamAnswer(LevelExamAnswer levelExamAnswer)
        {
            if (levelExamAnswer != null)
            {
                GameSchoolEntities.LevelExamAnswers.AddObject(levelExamAnswer);
                Save();
            }
        }

        /// <summary>
        /// Creates a user answer for a specific exam question.
        /// </summary>
        /// <param name="answerId"></param>
        /// <param name="userInfoId"></param>
        public void AnswerLevelExamQuestion(int answerId, int userInfoId)
        {
            var answer = GameSchoolEntities.LevelExamAnswers.Where(x => x.LevelExamAnswerId == answerId).SingleOrDefault();

            if (answer == null)
                throw new GameSchoolException(string.Format(
                    "Answer does not exist. AnswerId = {0}", answerId));

            var question =
                GameSchoolEntities.LevelExamQuestions.Where(x => x.LevelExamQuestionId == answer.LevelExamQuestionId).Single();

            /* Get the current users answers to the given question. */
            var questionAnswered = question.LevelExamAnswers.Where(p => p.UserInfoes.Where(u => u.UserInfoId == userInfoId).Count() > 0);

            var user = GameSchoolEntities.UserInfoes.Where(u => u.UserInfoId == userInfoId).Single();

            /* Remove all the old answers. (If any) */
            if (questionAnswered.Count() > 0)
            {
                foreach (var levelExamAnswer in questionAnswered)
                {
                    levelExamAnswer.UserInfoes.Remove(user);
                }
            }

            /* Add the new answer. */
            answer.UserInfoes.Add(user);

            Save();
        }

        /// <summary>
        /// Gets the users LevelExamAnswerId for an exam question. 
        /// </summary>
        /// <param name="levelExamQuestionId"></param>
        /// <param name="userInfoId"></param>
        /// <exception cref="GameSchoolException">If the question does not exist.</exception>
        /// <returns>-1 if no answer is found otherwise a LevelExamAnswerId.</returns>
        public int GetUserQuestionAnswer(int levelExamQuestionId, int userInfoId)
        {
            var question = GameSchoolEntities.LevelExamQuestions.Where(x => x.LevelExamQuestionId == levelExamQuestionId).Single();

            if (question == null)
                throw new GameSchoolException(string.Format(
                    "Question does not exist. QuestionId = {0}", levelExamQuestionId));

            var questionAnswered = question.LevelExamAnswers.Where(p => p.UserInfoes.Where(u => u.UserInfoId == userInfoId).Count() > 0);

            if (questionAnswered.Count() > 0)
            {
                foreach (var levelExamAnswer in questionAnswered)
                {
                    return levelExamAnswer.LevelExamAnswerId;
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets the first question for a given exam.
        /// </summary>
        /// <param name="levelExamId"></param>
        /// <returns></returns>
        public LevelExamQuestion GetFirstQuestionByExamId(int levelExamId)
        {
            var question = GameSchoolEntities.LevelExamQuestions.Where(x => x.LevelExamId == levelExamId).OrderBy(x => x.LevelExamQuestionId);
            if (question.Count() > 0)
                return question.First();

            return null;
        }

        /// <summary>
        /// Closes an exam and gives the user a grade, points and notification.
        /// </summary>
        /// <param name="levelExamId"></param>
        /// <param name="userInfoId"></param>
        /// <returns></returns>
        public double ReturnExam(int levelExamId, int userInfoId)
        {
            if (HasAccessToExam(levelExamId, userInfoId))
            {
                var levelExamResult = new LevelExamResult();
                levelExamResult.UserInfoId = userInfoId;
                levelExamResult.LevelExamId = levelExamId;

                var exam = GameSchoolEntities.LevelExams.Where(x => x.LevelExamId == levelExamId).Single();

                var correctAnswer = exam.LevelExamQuestions.Where(
                    x =>
                    x.LevelExamAnswers.Where(y => y.UserInfoes.Where(z => z.UserInfoId == userInfoId).Count() > 0).Where
                        (t => t.Correct).Count() > 0);

                levelExamResult.Grade = ((correctAnswer.Count() * 1.0) / exam.LevelExamQuestions.Count()) * 10;

                GameSchoolEntities.LevelExamResults.AddObject(levelExamResult);
                Save();

                if (ExternalNotificationContainer != null)
                    ExternalNotificationContainer.CreateNotification(string.Format("Þú hefur fengið {0} fyrir prófið \"{1}\"", levelExamResult.Grade, exam.Name), string.Format("/Exam/Index/{0}", exam.Level.CourseId), userInfoId);
                if (ExternalPointContainer != null)
                {
                    const int totalPointsPerGradeUnit = 5;
                    int points = ExternalPointContainer.CalculatePointsByGrade(levelExamResult.Grade,
                                                                               totalPointsPerGradeUnit);
                    if (points > 0)
                    {
                        ExternalPointContainer.AddPointsToLevel(userInfoId, exam.LevelId, points,
                                                                string.Format(
                                                                    "Þú hefur fengið {0} stig fyrir prófið \"{1}\".",
                                                                    points, exam.Name));
                    }
                }
                return levelExamResult.Grade;
            }
            return 0;
        }

        /// <summary>
        /// Checks if the user has access to an exam.
        /// </summary>
        /// <param name="levelExamId"></param>
        /// <param name="userInfoId"></param>
        /// <returns></returns>
        public bool HasAccessToExam(int levelExamId, int userInfoId)
        {
            var levelExam = GameSchoolEntities.LevelExams.Where(x => x.LevelExamId == levelExamId).Single();

            if (levelExam.Stop <= DateTime.Now)
                return false;

            if (levelExam.Level.Course.UserInfoes.Where(u => u.UserInfoId == userInfoId).Count() > 0)
            {
                if (levelExam.LevelExamResults.Where(x => x.UserInfoId == userInfoId).Count() == 0)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the next question in a level exam.
        /// </summary>
        /// <param name="levelExamQuestionId">The current level exam question id.</param>
        /// <returns></returns>
        public LevelExamQuestion GetNextLevelExamQuestion(int levelExamQuestionId)
        {
            var question = GameSchoolEntities.LevelExamQuestions.Where(x => x.LevelExamQuestionId == levelExamQuestionId).Single();

            if (question == null)
                throw new GameSchoolException(string.Format(
                    "Question does not exist. QuestionId = {0}", levelExamQuestionId));

            bool next = false;
            foreach (var item in question.LevelExam.LevelExamQuestions.OrderBy(x => x.LevelExamQuestionId))
            {
                if (next)
                    return item;

                if (item.LevelExamQuestionId == levelExamQuestionId)
                    next = true;
            }

            return null;
        }

        /// <summary>
        /// Gets a level exam answer.
        /// </summary>
        /// <param name="levelExamAnswerId"></param>
        /// <returns></returns>
        public LevelExamAnswer GetLevelExamAnswer(int levelExamAnswerId)
        {
            if (levelExamAnswerId > 0)
            {
                var item = (from x in GameSchoolEntities.LevelExamAnswers
                            where x.LevelExamAnswerId == levelExamAnswerId
                            select x).SingleOrDefault();

                return item;
            }
            return null;
        }

        /// <summary>
        /// Deletes a level exam question. Also deletes all children answers.
        /// </summary>
        /// <param name="levelExamQuestionId"></param>
        public void DeleteLevelExamQuestion(int levelExamQuestionId)
        {
            if (levelExamQuestionId > 0)
            {
                var item = GetLevelExamQuestion(levelExamQuestionId);

                var children = item.LevelExamAnswers;

                for (int i = 0; i <= children.Count(); i++)
                {
                    GameSchoolEntities.LevelExamAnswers.DeleteObject(children.ElementAt(i));
                }

                GameSchoolEntities.LevelExamQuestions.DeleteObject(item);
                Save();
            }
        }

        /// <summary>
        /// Deletes a level exam answer.
        /// </summary>
        /// <param name="levelExamAnswerId"></param>
        public void DeleteLevelExamAnswer(int levelExamAnswerId)
        {
            if (levelExamAnswerId > 0)
            {
                var item = GetLevelExamAnswer(levelExamAnswerId);

                GameSchoolEntities.LevelExamAnswers.DeleteObject(item);
                Save();
            }
        }

        /// <summary>
        /// Gets a level exam question.
        /// </summary>
        /// <param name="levelExamQuestionsId"></param>
        /// <returns></returns>
        public LevelExamQuestion GetLevelExamQuestion(int levelExamQuestionsId)
        {
            if (levelExamQuestionsId < 0)
            {
                return null;
            }
            var query = GameSchoolEntities.LevelExamQuestions.Where(l => l.LevelExamQuestionId == levelExamQuestionsId);

            var levelExamQuestion = query.FirstOrDefault();

            if (levelExamQuestion == null)
            {
                return null;
            }
            return levelExamQuestion;
        }

        /// <summary>
        /// Get the placement of the question in the level exam question list.
        /// </summary>
        /// <param name="levelExamQuestionId"></param>
        /// <returns></returns>
        public int GetLevelExamQuestionPlacement(int levelExamQuestionId)
        {
            var question =
                GameSchoolEntities.LevelExamQuestions.Where(x => x.LevelExamQuestionId == levelExamQuestionId).
                    SingleOrDefault();

            if (question==null)
                throw new GameSchoolException(string.Format("Question not found. QuestionId = {0}",levelExamQuestionId));

            int placement = 0;

            foreach (var levelExamQuestion in question.LevelExam.LevelExamQuestions.OrderBy(x=>x.LevelExamQuestionId))
            {
                placement++;
                if (levelExamQuestion.LevelExamQuestionId == levelExamQuestionId)
                    return placement;
            }

            return placement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelProjectResult"></param>
        /// <param name="user"></param>
        public void CreateUserLevelProjectResult(LevelProjectResult levelProjectResult, UserInfo user)
        {
            if (levelProjectResult != null && user != null)
            {
                user.LevelProjectResults.Add(levelProjectResult);
                Save();
            }
        }




        public IEnumerable<LevelMaterial> GetLevelMaterials()
        {
            return GameSchoolEntities.LevelMaterials;
        }
        public IEnumerable<LevelMaterial> GetLevelMaterials(int levelId)
        {
            return GameSchoolEntities.LevelMaterials.Where(l => l.LevelId == levelId);
        }

        public IEnumerable<LevelMaterial> GetLevelMaterials(int levelId, int contentTypeId)
        {
            return GameSchoolEntities.LevelMaterials.Where(l => l.LevelId == levelId && l.ContentTypeId == contentTypeId);
        }



        public LevelMaterial GetLevelMaterial(int levelMaterialId)
        {
            if (0 > levelMaterialId)
            {
                return null;
            }
            var query = GameSchoolEntities.LevelMaterials.Where(l => l.LevelMaterialId == levelMaterialId);

            var levelMaterial = query.FirstOrDefault();

            if (levelMaterial == null)
            {
                return null;
            }

            return levelMaterial;
        }

        public void CreateLevelMaterial(LevelMaterial levelMaterial)
        {
            if (levelMaterial != null)
            {
                GameSchoolEntities.LevelMaterials.AddObject(levelMaterial);
                Save();
            }
        }

        public void UpdateLevelMaterial(LevelMaterial levelMaterial)
        {
            if (levelMaterial != null)
            {
                var query = GameSchoolEntities.LevelMaterials
                    .Where(l => l.LevelMaterialId == levelMaterial.LevelMaterialId);

                var levelMaterialToUpdate = query.FirstOrDefault();
                if (levelMaterialToUpdate != null)
                {
                    levelMaterialToUpdate.ContentId = levelMaterial.ContentId;
                    levelMaterialToUpdate.ContentTypeId = levelMaterial.ContentTypeId;
                    levelMaterialToUpdate.Url = levelMaterial.Url;
                    levelMaterialToUpdate.Description = levelMaterial.Description;
                    levelMaterialToUpdate.Title = levelMaterial.Title;
                }
                Save();

            }
        }

        public IEnumerable<ContentType> GetContentTypes()
        {
            var contentTypes = from x in GameSchoolEntities.ContentTypes
                               select x;

            return contentTypes;
        }

        /// <summary>
        /// Get a collection of levelprojectresult instances by userinfoid
        /// </summary>
        /// <param name="userInfoId">Id of a userInfo instance.</param>
        /// <returns>Collection of levelprojectresult objects.</returns>
        public IEnumerable<LevelProjectResult> GetLevelProjectResultsByUserId(int userInfoId)
        {
            if (0 > userInfoId)
            {
                yield break;
            }

            var query = GameSchoolEntities.LevelProjectResults.Where(u => u.UserInfoId == userInfoId)
                .AsEnumerable();

            if (query == null)
            {
                yield break;
            }

            foreach (var levelProjectResult in query)
            {
                yield return levelProjectResult;
            }
        }

        public IEnumerable<LevelProject> GetLevelProjectsByCourseIdAndUserInfoId(int userInfoId, int courseId)
        {
            if (0 > userInfoId | 0 > courseId)
            {
                return null;
            }

            var query =
                GameSchoolEntities.Courses.Where(c => c.CourseId == courseId)
                                          .SelectMany(c => c.UserInfoes
                                              .Where(d => d.UserInfoId == userInfoId).SelectMany(x => x.Courses
                                                  .SelectMany(d => d.Levels
                                                      .SelectMany(g => g.LevelProjects))));

            return query;
        }

        public IEnumerable<LevelProject> GetLevelProjectsByUserId(int userInfoId)
        {
            if (0 > userInfoId)
            {
                return null;
            }
            // þarf að lagfæra, síar ekki út með id
            var levelProjectQuery =
                GameSchoolEntities.LevelProjects.Select(x => x).Include("Level").Include("LevelProjectResults").Include(
                    "Level.Course");

            return levelProjectQuery;
        }

        public bool DeleteLevelProject(int levelProjectId)
        {
            if (levelProjectId < 0)
            {
                return false;
            }
            var query = GameSchoolEntities.LevelProjects.Where(l => l.LevelProjectId == levelProjectId);

            var levelProject = query.SingleOrDefault();

            if (levelProject == null)
            {
                return false;
            }

            GameSchoolEntities.LevelProjects.DeleteObject(levelProject);
            Save();
            return true;
        }


        public void CreateLevelProjectResult(LevelProjectResult levelProjectResult)
        {
            if (levelProjectResult != null)
            {
                GameSchoolEntities.LevelProjectResults.AddObject(levelProjectResult);
                Save();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelProjectId"></param>
        /// <returns></returns>
        public IEnumerable<LevelProjectResult> GetlevelProjectResultsByLevelProjectId(int levelProjectId)
        {
            return levelProjectId > 0
                       ? GameSchoolEntities.LevelProjectResults.Where(i => i.LevelProjectId == levelProjectId)
                       : null;
        }

        /// <summary>
        /// Returns a instance of levelprojectresult by levelproject id
        /// </summary>
        /// <param name="levelProjectId"></param>
        /// <returns></returns>
        public LevelProjectResult GetlevelProjectResultByLevelProjectId(int levelProjectId)
        {
            if (0 > levelProjectId)
            {
                return null;
            }

            var query = GameSchoolEntities.LevelProjectResults.Where(l => l.LevelProjectId == levelProjectId);

            var levelProjectResult = query.FirstOrDefault();

            if (levelProjectResult == null)
            {
                return null;
            }

            return levelProjectResult;
        }

        /// <summary>
        /// Updates a instance of levelproject
        /// </summary>
        /// <param name="levelProjectResult"></param>
        public void UpdateLevelProjectResult(LevelProjectResult levelProjectResult)
        {
            if (levelProjectResult != null)
            {
                var levelProjectResultToUpdate = GetlevelProjectResultByLevelProjectId(levelProjectResult.LevelProjectId);
                levelProjectResultToUpdate.Grade = levelProjectResult.Grade;
                levelProjectResultToUpdate.TeacherFeedback = levelProjectResult.TeacherFeedback;
                levelProjectResultToUpdate.GradeDate = DateTime.Now;

                var levelProject = GetLevelProject(levelProjectResult.LevelProjectId);
                int points = 10;

                ExternalNotificationContainer.CreateNotification(string.Format("Þú hefur fengið {0} fyrir verkefnið \"{1}\"",
                    levelProjectResult.Grade, levelProject.Name), string.Format("/Project/Index/{0}", levelProject.Level.CourseId), levelProjectResult.UserInfoId);

                ExternalPointContainer.AddPointsToLevel(levelProjectResult.UserInfoId, levelProject.LevelId, points,
                                                        string.Format("Þú hefur fengið {0} stig fyrir verkefnið \"{1}\".",
                                                                      points, levelProject.Name));

                Save();
            }
        }

        /// <summary>
        /// Returns a instance of levelprojectresult by levelprojectresultid
        /// </summary>
        /// <param name="id">Levelprojectresultid</param>
        /// <returns>LEvelprojectresult instance of null</returns>
        public LevelProjectResult GetlevelProjectResultsByLevelProjectResultId(int id)
        {
            if (0 > id)
            {
                return null;
            }

            var query = GameSchoolEntities.LevelProjectResults.Where(i => i.LevelProjectResultId == id);

            var levelProjectResult = query.FirstOrDefault();

            if (levelProjectResult == null)
            {
                return null;
            }

            return levelProjectResult;
        }

        /// <summary>
        /// Adds a levelproject instance to a level
        /// </summary>
        /// <param name="levelproject"></param>
        /// <param name="courseId"></param>
        public void AddLevelProjectToCourseAndLevel(LevelProject levelproject, int courseId)
        {
            if (levelproject != null && courseId > 0)
            {
                var level = GameSchoolEntities.Levels.FirstOrDefault(l => l.LevelId == levelproject.LevelId && l.CourseId == courseId);

                if (level != null)
                {
                    level.LevelProjects.Add(levelproject);

                    var allUsersInThisCourse =
                        GameSchoolEntities.UserInfoes.SelectMany(s => s.Courses.Where(d => d.CourseId == courseId)).
                            SelectMany(x => x.UserInfoes);
                    foreach (var user in allUsersInThisCourse.Where(s => s.UserTypeId == 1).Distinct())
                    {
                        ExternalNotificationContainer.CreateNotification(string.Format("Nýtt verkefni er komið í áfangann {0} með nafninu \"{1}\"",
                            levelproject.Level.Course.Name, levelproject.Name), string.Format("/Project/Index/{0}", levelproject.Level.CourseId), user.UserInfoId);
                    }
                }

                Save();
            }
        }

        /// <summary>
        /// Returns all level instances by course id
        /// </summary>
        /// <param name="courseId">Id of course </param>
        /// <returns>Collection of levels</returns>
        public IEnumerable<Level> GetLevelsByCourseId(int courseId)
        {
            return courseId > 0 ? GameSchoolEntities.Levels.Where(c => c.CourseId == courseId) : null;
        }

        public IEnumerable<LevelProject> GetLevelProjectsByLevelId(int levelId)
        {
            return levelId > 0 ? GameSchoolEntities.LevelProjects.Where(l => l.LevelId == levelId) : null;
        }

        public IEnumerable<LevelTab> GetLevelTabsByCourseIdAndUserInfoId(int courseId, int userInfoId)
        {
            var levels = (from x in GameSchoolEntities.Levels
                          where x.CourseId == courseId
                          select x).OrderBy(y => y.LevelId);

            var levelTabs = new List<LevelTab>();

            bool previousWasComplete = false;

            foreach (var level in levels)
            {
                var currentLevelTab = new LevelTab();
                currentLevelTab.levelId = level.LevelId;
                currentLevelTab.levelName = level.Name;

                var levelexams = from x in level.LevelExams
                                 select x;

                var levelproject = from x in level.LevelProjects
                                   select x;

                var levelexamreturns = from x in level.LevelExams
                                       where x.LevelExamResults.Where(y => y.UserInfoId == userInfoId).Count() > 0
                                       select x;

                var levelprojectreturns = from x in level.LevelProjects
                                          where x.LevelProjectResults.Where(y => y.UserInfoId == userInfoId).Count() > 0
                                          select x;

                

                if (level.Start <= DateTime.Now)
                {
                    currentLevelTab.enabled = true;
                }
                else if (previousWasComplete)
                {
                    currentLevelTab.enabled = true;
                }
                else
                {
                    currentLevelTab.enabled = false;
                }

                bool levelHasEnded = false;
                if (level.Stop < DateTime.Now)
                {
                    levelHasEnded = true;
                }

                if (levelexams.Count() == levelexamreturns.Count() && levelproject.Count() == levelprojectreturns.Count())
                {
                    previousWasComplete = true;
                    currentLevelTab.levelCompleteness = LevelCompleteness.Complete;
                }
                else
                {
                    previousWasComplete = false;
                    if (levelHasEnded)
                    {
                        currentLevelTab.enabled = false;
                        currentLevelTab.levelCompleteness = LevelCompleteness.Failed;
                    }
                    else
                    {
                        currentLevelTab.levelCompleteness = LevelCompleteness.Incomplete;
                    }
                    
                }


                levelTabs.Add(currentLevelTab);
            }

            return levelTabs;
        }
    }

    public class LevelTab
    {
        public int levelId {get; set;}
        public string levelName {get; set; }
        public bool enabled { get; set; }
        public LevelCompleteness levelCompleteness { get; set; }
    }


}
