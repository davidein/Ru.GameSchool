using System.Collections.Generic;
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
        public IEnumerable<Level> GetLevels()
        {
            return GameSchoolEntities.Levels;
        }
        /// <summary>
        /// Update a level object with new changes and persist it to the datasource.
        /// </summary>
        /// <param name="level">Level instance with updated values.</param>
        public void UpdateLevel(Level level)
        {
            throw new System.NotImplementedException();
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
            var query = GameSchoolEntities.LevelExams.Where(le => le.LevelExamId == levelExamId);

            var levelExam = query.FirstOrDefault();

            if (levelExam == null)
            {
                return null;
            }

            return levelExam;
        }

        public IEnumerable<LevelExam> GetLevelExams()
        {
            return GameSchoolEntities.LevelExams;
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
            return userInfoId > 0 ? GameSchoolEntities.spc_GetAllUsersLevelProjects(userInfoId.Value) : null;
        }

        public IEnumerable<LevelProject> GetLevelProjects()
        {
            return GameSchoolEntities.LevelProjects;
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
        public IEnumerable<LevelExamQuestion> GetLevelExamQuestions()
        {
            return GameSchoolEntities.LevelExamQuestions;
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
                
            }
        }
    }
}
