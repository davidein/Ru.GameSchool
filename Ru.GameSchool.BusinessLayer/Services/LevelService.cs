using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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



        /// <summary>
        /// Returns a collection of level instances
        /// </summary>
        /// <returns>IEnumerable of level instances.</returns>
        public IEnumerable<Level> GetLevels(int CourseId)
        {
            return GameSchoolEntities.Levels.Where(x=> x.CourseId == CourseId);
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
            var exams = list.Where(x=>x.Level.Course.UserInfoes.Where(y=>y.UserInfoId == userInfoId).Count()>0);



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
                var query = GameSchoolEntities.LevelProjects
                    .Where(l => l.LevelProjectId == levelProject.LevelProjectId);

                var levelProjectToUpdate = query.FirstOrDefault();

                levelProjectToUpdate.Description = levelProject.Description;
                levelProjectToUpdate.GradePercentageValue = levelProject.GradePercentageValue;
                levelProjectToUpdate.Name = levelProject.Name;
                levelProjectToUpdate.Start = levelProject.Start;
                levelProjectToUpdate.Stop = levelProject.Stop;
                //levelProjectToUpdate.ProjectUrl = levelProject.ProjectUrl;

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
            return GameSchoolEntities.LevelExamQuestions.Where(x=>x.LevelExamId == levelExamId);
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


        public IEnumerable<LevelMaterial> GetCourseMaterials(int CourseId)
        {
            //return GameSchoolEntities.LevelMaterials.Where(l=> l.LevelId in );
            var returnList = (from x in GameSchoolEntities.LevelMaterials
                              join y in GameSchoolEntities.Levels on x.LevelId equals y.LevelId
                              where y.CourseId == CourseId
                              select x);
            return returnList;
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

        public IEnumerable<Ru.GameSchool.DataLayer.Repository.ContentType> GetContentTypes()
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

            var query =
                GameSchoolEntities.LevelProjects.SelectMany(
                    c =>
                    c.Level.Course.UserInfoes.Where(x => x.UserInfoId == userInfoId).SelectMany(
                        d => d.Courses.SelectMany(f => f.Levels.SelectMany(k => k.LevelProjects))));
            return query;
        }
    }
}
