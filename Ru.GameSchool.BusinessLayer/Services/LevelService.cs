using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using Ru.GameSchool.BusinessLayer.Exceptions;
using Ru.GameSchool.DataLayer;
using Ru.GameSchool.DataLayer.Repository;

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
        public IEnumerable<Level> GetLevels(int CourseId)
        {
            return GameSchoolEntities.Levels.Where(x => x.CourseId == CourseId);
        }
        /// <summary>
        /// Update a level object with new changes and persist it to the datasource.
        /// </summary>
        /// <param name="level">Level instance with updated values.</param>
        public void UpdateLevel(Level level)
        {
            Save();
            //throw new System.NotImplementedException();
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

        public IEnumerable<LevelExam> GetLevelExams(int courseId, int userInfoId)
        {
            var list = GameSchoolEntities.LevelExams.Where(x => x.Level.CourseId == courseId);
            var exams = list.Where(x => x.Level.Course.UserInfoes.Where(y => y.UserInfoId == userInfoId).Count() > 0);



            return exams;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelExam">Levelexam instance to update.</param>
        public void UpdateLevelExam(LevelExam levelExam)
        {
            if (levelExam != null)
            {

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
        public void UpdateLevelProjectFromResult(LevelProject levelProject)
        {
            if (levelProject != null)
            {
                var levelProjToUpdate = GetLevelProject(levelProject.LevelProjectId);
                levelProjToUpdate.ContentID = levelProject.ContentID;
                levelProjToUpdate.UserFeedback = levelProject.UserFeedback;
                levelProjToUpdate.LevelProjectResults.Add(levelProject.LevelProjectResults.ElementAtOrDefault(0));
                Save();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelExamResult"></param>
        public void CreateLevelExamResult(LevelExamResult levelExamResult)
        {
            if (levelExamResult != null)
            {
                GameSchoolEntities.LevelExamResults.AddObject(levelExamResult);
                Save();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelExamResult"></param>
        public void UpdateLevelExamResult(LevelExamResult levelExamResult)
        {
            if (levelExamResult != null)
            {

            }
        }

        public IEnumerable<LevelExamResult> GetLevelExamResults()
        {
            return GameSchoolEntities.LevelExamResults;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelExamId"></param>
        /// <returns></returns>
        public LevelExamResult GetLevelExamResults(int levelExamId)
        {
            if (levelExamId < 0)
            {
                return null;
            }

            var query = GameSchoolEntities.LevelExamResults
                .Where(lvr => lvr.LevelExamId == levelExamId);

            var levelExamResult = query.FirstOrDefault();

            if (levelExamResult == null)
            {
                return null;
            }

            return levelExamResult;
        }

        /// <summary>
        /// 
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
        /// 
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

        public void AnswerLevelExamQuestion(int answerId, int userInfoId)
        {
            var answer = GameSchoolEntities.LevelExamAnswers.Where(x => x.LevelExamAnswerId == answerId).SingleOrDefault();

            if (answer == null)
                throw new GameSchoolException(string.Format(
                    "Answer does not exist. AnswerId = {0}", answerId));
 
            var question =
                GameSchoolEntities.LevelExamQuestions.Where(x => x.LevelExamQuestionId == answer.LevelExamQuestionId).Single();

            var questionAnswered = question.LevelExamAnswers.Where(p => p.UserInfoes.Where(u => u.UserInfoId == userInfoId).Count() > 0);

            var user = GameSchoolEntities.UserInfoes.Where(u => u.UserInfoId == userInfoId).Single();

            if (questionAnswered.Count() > 0)
            {
                foreach (var levelExamAnswer in questionAnswered)
                {
                    levelExamAnswer.UserInfoes.Remove(user);
                }
            }

            answer.UserInfoes.Add(user);

            Save();
        }

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

        public LevelExamQuestion GetFirstQuestionByExamId(int levelExamId)
        {
            var question = GameSchoolEntities.LevelExamQuestions.Where(x => x.LevelExamId == levelExamId).OrderBy(x=>x.LevelExamQuestionId);
            if (question.Count() > 0)
                return question.First();

            return null;
        }

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

                levelExamResult.Grade = (exam.LevelExamQuestions.Count()*correctAnswer.Count())/(10*1.0);

                GameSchoolEntities.LevelExamResults.AddObject(levelExamResult);
                Save();

                int points = 5;

                ExternalNotificationContainer.CreateNotification(string.Format("Þú hefur fengið {0} fyrir prófið \"{1}\"", levelExamResult.Grade, exam.Name), string.Format("/Exam/Index/{0}",exam.Level.CourseId), userInfoId);
                ExternalPointContainer.AddPointsToLevel(userInfoId, exam.LevelId, points,
                                                        string.Format("Þú hefur fengið {0} stig fyrir prófið \"{1}\".",
                                                                      points, exam.Name));
                    
                return levelExamResult.Grade;
            }
            return 0;
        }

        public bool HasAccessToExam(int levelExamId, int userInfoId)
        {
            var levelExam = GameSchoolEntities.LevelExams.Where(x => x.LevelExamId == levelExamId).Single();

            var user = GameSchoolEntities.UserInfoes.Where(u => u.UserInfoId == userInfoId).Single();

            if (levelExam.Level.Course.UserInfoes.Where(u => u.UserInfoId == userInfoId).Count() > 0)
            {
                if (levelExam.LevelExamResults.Where(x=>x.UserInfoId == userInfoId).Count() == 0)
                    return true;
            }

            return false;
        }

        public LevelExamQuestion GetNextLevelExamQuestion(int levelExamQuestionId)
        {
            var question = GameSchoolEntities.LevelExamQuestions.Where(x => x.LevelExamQuestionId == levelExamQuestionId).Single();

            if (question == null)
                throw new GameSchoolException(string.Format(
                    "Question does not exist. QuestionId = {0}", levelExamQuestionId));

            bool next = false;
            foreach (var item in question.LevelExam.LevelExamQuestions.OrderBy(x=>x.LevelExamQuestionId))
            {
                if (next)
                    return item;

                if (item.LevelExamQuestionId == levelExamQuestionId)
                    next = true;
            }

            return null;
        }

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

        public void DeleteLevelExamQuestion(int levelExamQuestionId)
        {
            if (levelExamQuestionId > 0)
            {
                var item = GetLevelExamQuestion(levelExamQuestionId);

                GameSchoolEntities.LevelExamQuestions.DeleteObject(item);
                Save();
            }
        }

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
        /// 
        /// </summary>
        /// <param name="levelExamQuestion"></param>
        public void UpdateLevelExamQuestion(LevelExamQuestion levelExamQuestion)
        {
            if (levelExamQuestion != null)
            {

            }
        }

        /// <summary>
        /// Method that returns a collection of leveexamquestion objects.
        /// </summary>
        /// <returns>IEnumerable collection of levelexamquestions.</returns>
        public IEnumerable<LevelExamQuestion> GetLevelExamQuestions(int levelExamId)
        {
            return GameSchoolEntities.LevelExamQuestions.Where(x => x.LevelExamId == levelExamId);
        }

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
        /// 
        /// </summary>
        /// <param name="levelExamResult"></param>
        /// <param name="user"></param>
        public void CreateUserLevelExamResult(LevelExamResult levelExamResult, UserInfo user)
        {
            if (levelExamResult != null && user != null)
            {
                user.LevelExamResults.Add(levelExamResult);
                Save();
            }
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
    }
}
