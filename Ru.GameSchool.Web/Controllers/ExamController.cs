using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ru.GameSchool.DataLayer.Repository;
using System.Web.Security;
using Ru.GameSchool.Web.Classes;
using Ru.GameSchool.Web.Classes.Helper;

namespace Ru.GameSchool.Web.Controllers
{
    public class ExamController : BaseController
    {
        [HttpGet]
        [Authorize(Roles = "Student, Teacher")]
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                ViewBag.Title = "Listi yfir próf";

                var user = MembershipHelper.GetUser();

                ViewBag.UserInfoId = user.UserInfoId; 

                var course = CourseService.GetCourse(id.Value);

                var exams = LevelService.GetLevelExamsByCourseId(id.Value, user.UserInfoId);

                ViewBag.Course = course;
                ViewBag.CourseName = course.Name;
                ViewBag.CourseId = course.CourseId;

                return View(exams);
            }
            return View();
        }

        #region Student

        [HttpGet]
        [Authorize(Roles = "Student, Teacher")]
        public ActionResult Get(int? id)
        {
            if (id.HasValue)
            {
                ViewBag.Title = "Skoða próf";              
                var exam = LevelService.GetLevelExam(id.Value);
                ViewBag.CourseId = exam.Level.CourseId;
                return View(exam);
            }
            return View();
        }

        [Authorize(Roles = "Student, Teacher")]
        public ActionResult TakeExam(int? id)
        {
            if (id.HasValue)
            {
                var exam = LevelService.GetLevelExam(id.Value);
                if (exam != null)
                {
                    var question = exam.LevelExamQuestions.OrderBy(x => x.LevelExamQuestionId).FirstOrDefault();

                    return RedirectToAction("Question", "Exam", new {id = question.LevelExamQuestionId});
                }
            }
            return RedirectToAction("NotFound", "Home");
        }

        [Authorize(Roles = "Student, Teacher")]
        public ActionResult Question(int? id)
        {
            if (id.HasValue)
            {
                var user = MembershipHelper.GetUser();
                var examQuestion = LevelService.GetLevelExamQuestion(id.Value);

                if (!LevelService.HasAccessToExam(examQuestion.LevelExamId, user.UserInfoId))
                    return RedirectToAction("Index", "Exam", new {id = examQuestion.LevelExam.Level.CourseId});

                ViewBag.Placement = LevelService.GetLevelExamQuestionPlacement(id.Value);

                var answer = LevelService.GetUserQuestionAnswer(examQuestion.LevelExamQuestionId,
                                                                user.UserInfoId);

                ViewBag.CourseId = examQuestion.LevelExam.Level.CourseId;
                ViewBag.CourseName = examQuestion.LevelExam.Level.Course.Name;

                //ViewBag.Title = "Spurning " + ViewBag.Placement + " af " + examQuestion.LevelExam.LevelExamQuestions.Count();

                if (answer != -1)
                    ViewBag.UserAnswer = answer;

                return View(examQuestion);
            }
            return RedirectToAction("NotFound", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Student, Teacher")]
        public ActionResult Question(LevelExamAnswer model)
        {
            if (model != null)
            {
                var user = MembershipHelper.GetUser();
                if (model.LevelExamAnswerId != 0)
                {
                    LevelService.AnswerLevelExamQuestion(model.LevelExamAnswerId, user.UserInfoId);                    
                }

                var nextQuestion = LevelService.GetNextLevelExamQuestion(model.LevelExamQuestionId);

                if (nextQuestion == null)
                {
                    var examQuestion = LevelService.GetLevelExamQuestion(model.LevelExamQuestionId);
                    var firstQuestion = LevelService.GetFirstQuestionByExamId(examQuestion.LevelExamId);
                    return RedirectToAction("Question", "Exam", new { id = firstQuestion.LevelExamQuestionId });
                }
                return RedirectToAction("Question", "Exam", new {id = nextQuestion.LevelExamQuestionId});
            }
            return RedirectToAction("NotFound", "Home");
        }

        [Authorize(Roles = "Student, Teacher")]
        public ActionResult ReturnExam(int? id)
        {
            if (id.HasValue)
            {
                var user = MembershipHelper.GetUser();
                if (LevelService.HasAccessToExam(id.Value, user.UserInfoId))
                {
                    LevelService.ReturnExam(id.Value, user.UserInfoId);



                    return View();
                }
            }
            return RedirectToAction("NotFound", "Home");
        }
        #endregion

        #region Teacher

        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id.HasValue)
            {
                ViewBag.GradePercentageValues = GetPercentageValue();
                ViewBag.Levels = new SelectList(LevelService.GetLevels(id.Value), "LevelId", "Name");
                ViewBag.CourseId = id.Value;
                ViewBag.CourseName = CourseService.GetCourse(id.Value).Name;
                ViewBag.Title = "Búa til nýtt próf";

                LevelExam model = new LevelExam();

                return View(model);
            }

            return RedirectToAction("NotFound", "Home");
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Create(LevelExam levelExam)
        {
            if (ModelState.IsValid)
            {
                LevelService.CreateLevelExam(levelExam);

                return RedirectToAction("Edit", "Exam", new {id = levelExam.LevelExamId});
            }
            return View(levelExam);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult ExamQuestions(int? id)
        {
            LevelExamQuestion model = new LevelExamQuestion();
            if (id.HasValue)
            {
                var exam = LevelService.GetLevelExam(id.Value);
                ViewBag.CourseId = exam.Level.CourseId;
                ViewBag.CourseName = exam.Level.Course.Name;
                ViewBag.Exam = exam;
                ViewBag.QuestionList = exam.LevelExamQuestions;

                model.LevelExamId = id.Value;
            }
            return View(model);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult ExamQuestions(LevelExamQuestion model)
        {
            if (model != null)
            {
                var exam = LevelService.GetLevelExam(model.LevelExamId);
                ViewBag.CourseId = exam.Level.CourseId;
                ViewBag.CourseName = exam.Level.Course.Name;
                ViewBag.Exam = exam;
                ViewBag.QuestionList = exam.LevelExamQuestions;

                LevelService.CreateLevelExamQuestion(model);
            }

            return View(model);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult ExamAnswer(LevelExamAnswer model)
        {
            if (model!=null)
            {
                if (model.LevelExamQuestionId == 0)
                    return RedirectToAction("NotFound", "Home");

                LevelService.CreateLevelExamAnswer(model);
                var item = LevelService.GetLevelExamQuestion(model.LevelExamQuestionId);

                return RedirectToAction("ExamQuestions", "Exam", new {id = item.LevelExamId});
            }

            return RedirectToAction("NotFound", "Home");
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteLevelExamAnswer(int? id)
        {
            if (id.HasValue)
            {
                var item = LevelService.GetLevelExamAnswer(id.Value);
                var levelExamId = item.LevelExamQuestion.LevelExamId;
                LevelService.DeleteLevelExamAnswer(id.Value);
                return RedirectToAction("ExamQuestions", "Exam", new { id = levelExamId });
            }
            return RedirectToAction("NotFound", "Home");
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult DeleteLevelExamQuestion(int? id)
        {
            if (id.HasValue)
            {
                var item = LevelService.GetLevelExamQuestion(id.Value);
                LevelService.DeleteLevelExamQuestion(id.Value);
                return RedirectToAction("ExamQuestions", "Exam", new { id = item.LevelExamId });
            }
            return RedirectToAction("NotFound", "Home");
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {

            if (id.HasValue)
            {
                var exam = LevelService.GetLevelExam(id.Value);
                ViewBag.Levels = new SelectList(LevelService.GetLevels(exam.Level.CourseId), "LevelId", "Name");
                ViewBag.GradePercentageValue = GetPercentageValue();

                ViewBag.CourseId = exam.Level.CourseId;
                ViewBag.CourseName = exam.Level.Course.Name;
                return View(exam);
            }
            return View();
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult Edit(LevelExam model)
        {
            var exam = LevelService.GetLevelExam(model.LevelExamId);
            ViewBag.Levels = new SelectList(LevelService.GetLevels(exam.Level.CourseId), "LevelId", "Name");
            ViewBag.GradePercentageValue = GetPercentageValue();

            if (ModelState.IsValid)
            {
                var levelExam = LevelService.GetLevelExam(model.LevelExamId);
                if (TryUpdateModel(levelExam))
                {
                    LevelService.UpdateLevelExam(levelExam);
                    ViewBag.SuccessMessage = "Upplýsingar um próf hafa verið uppfærðar";
                    return View(levelExam);
                }
            }
            ViewBag.ErrorMessage = "Náði ekki að skrá/uppfæra upplýsingar! Lagfærðu villur og reyndur aftur.";

            return View(model);
        }

        #endregion


        #region helper methods
        /*
        public IEnumerable<SelectListItem> GradePercentageValue()
        {
            for (int j = 1; j <= 10; j++)
            {
                yield return new SelectListItem
                {
                    Text = string.Format("{0}%", j),
                    Value = j.ToString()
                };
            }
        }*/
        public IEnumerable<SelectListItem> GetPercentageValue()
        {
            for (int j = 1; j <= 100; j++)
            {
                yield return new SelectListItem
                {
                    Text = string.Format("{0}%", j),
                    Value = j.ToString()
                };
            }
        }

        public IEnumerable<SelectListItem> GetLevelCounts(int courseId)
        {
            for (int j = 0; j <= LevelService.GetLevels(courseId).Count(); j++)
            {
                var elementAtOrDefault = LevelService.GetLevels(courseId).ElementAtOrDefault(j);
                if (elementAtOrDefault != null)
                    yield return new SelectListItem
                    {
                        Text = elementAtOrDefault.Name,
                        Value = elementAtOrDefault.LevelId.ToString()
                    };
            }
        }

        /*
        private LevelExam CreateLevelExam(FormCollection collection)
        {
            return new LevelExam
            {
                GradePercentageValue = Convert.ToDouble(collection["GradePercentageValue"]),
                Description = collection["Description"],
                CreateDateTime = DateTime.Now,
                Name = collection["Name"],
                LevelId = Convert.ToInt32(collection["LevelId"]),
                LevelExamQuestions = new Collection<LevelExamQuestion>
                                     {
                                            new LevelExamQuestion
                                             {
                                                 Question = collection["question1"],
                                                 LevelExamAnswers = new Collection<LevelExamAnswer>
                                                                        {
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer1_1"],
                                                                                    Correct = Convert.ToBoolean(collection["check1_1"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer1_2"],
                                                                                    Correct = Convert.ToBoolean(collection["check1_2"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer1_3"],
                                                                                    Correct = Convert.ToBoolean(collection["check1_3"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer1_4"],
                                                                                    Correct = Convert.ToBoolean(collection["check1_4"])
                                                                                }
                                                                        }
                                             },
                                             new LevelExamQuestion
                                             {
                                                 Question = collection["question2"],
                                                 LevelExamAnswers = new Collection<LevelExamAnswer>
                                                                        {
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer2_1"],
                                                                                    Correct = Convert.ToBoolean(collection["check2_1"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer2_2"],
                                                                                    Correct = Convert.ToBoolean(collection["check2_2"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer2_3"],
                                                                                    Correct = Convert.ToBoolean(collection["check2_3"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer2_4"],
                                                                                    Correct = Convert.ToBoolean(collection["check2_4"])
                                                                                }
                                                                        }
                                             },
                                             new LevelExamQuestion
                                             {
                                                 Question = collection["question3"],
                                                 LevelExamAnswers = new Collection<LevelExamAnswer>
                                                                        {
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer3_1"],
                                                                                    Correct = Convert.ToBoolean(collection["check3_1"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer3_2"],
                                                                                    Correct = Convert.ToBoolean(collection["check3_2"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer3_3"],
                                                                                    Correct = Convert.ToBoolean(collection["check3_3"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer3_4"],
                                                                                    Correct = Convert.ToBoolean(collection["check3_4"])
                                                                                }
                                                                        }
                                             },
                                             new LevelExamQuestion
                                             {
                                                 Question = collection["question4"],
                                                 LevelExamAnswers = new Collection<LevelExamAnswer>
                                                                        {
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer4_1"],
                                                                                    Correct = Convert.ToBoolean(collection["check4_1"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer4_2"],
                                                                                    Correct = Convert.ToBoolean(collection["check4_2"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer4_3"],
                                                                                    Correct = Convert.ToBoolean(collection["check4_3"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer4_4"],
                                                                                    Correct = Convert.ToBoolean(collection["check4_4"])
                                                                                }
                                                                        }
                                             },
                                             new LevelExamQuestion
                                             {
                                                 Question = collection["question5"],
                                                 LevelExamAnswers = new Collection<LevelExamAnswer>
                                                                        {
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer5_1"],
                                                                                    Correct = Convert.ToBoolean(collection["check5_1"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer5_2"],
                                                                                    Correct = Convert.ToBoolean(collection["check5_2"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer4_3"],
                                                                                    Correct = Convert.ToBoolean(collection["check5_3"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer5_4"],
                                                                                    Correct = Convert.ToBoolean(collection["check5_4"])
                                                                                }
                                                                        }
                                             },
                                             new LevelExamQuestion
                                             {
                                                 Question = collection["question6"],
                                                 LevelExamAnswers = new Collection<LevelExamAnswer>
                                                                        {
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer6_1"],
                                                                                    Correct = Convert.ToBoolean(collection["check6_1"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer6_2"],
                                                                                    Correct = Convert.ToBoolean(collection["check6_2"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer6_3"],
                                                                                    Correct = Convert.ToBoolean(collection["check6_3"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer6_4"],
                                                                                    Correct = Convert.ToBoolean(collection["check6_4"])
                                                                                }
                                                                        }
                                             },
                                             new LevelExamQuestion
                                             {
                                                 Question = collection["question7"],
                                                 LevelExamAnswers = new Collection<LevelExamAnswer>
                                                                        {
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer7_1"],
                                                                                    Correct = Convert.ToBoolean(collection["check7_1"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer7_2"],
                                                                                    Correct = Convert.ToBoolean(collection["check7_2"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer7_3"],
                                                                                    Correct = Convert.ToBoolean(collection["check7_3"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer7_4"],
                                                                                    Correct = Convert.ToBoolean(collection["check7_4"])
                                                                                }
                                                                        }
                                             },
                                                                                          new LevelExamQuestion
                                             {
                                                 Question = collection["question8"],
                                                 LevelExamAnswers = new Collection<LevelExamAnswer>
                                                                        {
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer8_1"],
                                                                                    Correct = Convert.ToBoolean(collection["check8_1"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer8_2"],
                                                                                    Correct = Convert.ToBoolean(collection["check8_2"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer8_3"],
                                                                                    Correct = Convert.ToBoolean(collection["check8_3"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer8_4"],
                                                                                    Correct = Convert.ToBoolean(collection["check8_4"])
                                                                                }
                                                                        }
                                             },
                                             new LevelExamQuestion
                                             {
                                                 Question = collection["question9"],
                                                 LevelExamAnswers = new Collection<LevelExamAnswer>
                                                                        {
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer9_1"],
                                                                                    Correct = Convert.ToBoolean(collection["check9_1"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer9_2"],
                                                                                    Correct = Convert.ToBoolean(collection["check9_2"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer9_3"],
                                                                                    Correct = Convert.ToBoolean(collection["check9_3"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer9_4"],
                                                                                    Correct = Convert.ToBoolean(collection["check9_4"])
                                                                                }
                                                                        }
                                             },
                                             new LevelExamQuestion
                                             {
                                                 Question = collection["question10"],
                                                 LevelExamAnswers = new Collection<LevelExamAnswer>
                                                                        {
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer10_1"],
                                                                                    Correct = Convert.ToBoolean(collection["check10_1"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer10_2"],
                                                                                    Correct = Convert.ToBoolean(collection["check10_2"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer10_3"],
                                                                                    Correct = Convert.ToBoolean(collection["check10_3"])
                                                                                },
                                                                                new LevelExamAnswer
                                                                                {
                                                                                    Answer = collection["answer10_4"],
                                                                                    Correct = Convert.ToBoolean(collection["check10_4"])
                                                                                }
                                                                        }
                                             }
                                     }
            };
        }
        */
        #endregion


        /*
        public ActionResult Statistics(int id)
        {
            return View();
        }*/

    }
}