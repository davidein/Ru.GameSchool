using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.Web.Classes;
using Ru.GameSchool.Web.Classes.Helper;

namespace Ru.GameSchool.Web.Controllers
{
    public class MaterialController : BaseController
    {

        // GET: /Material/
        [Authorize(Roles = "Student, Teacher")]
        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                int courseIdValue = id.Value;
                IEnumerable<LevelMaterial> materials = CourseService.GetCourseMaterials(courseIdValue).OrderByDescending(m => m.CreateDateTime); ;
                ViewBag.Materials = materials;
                ViewBag.CourseName = CourseService.GetCourse(courseIdValue).Name;
                ViewBag.Courseid = CourseService.GetCourse(courseIdValue).CourseId;

                ViewBag.Title = "Listi yfir kennsluefni";
                return View(materials);
            }

            return RedirectToAction("NotFound", "Home");
        }

        public ActionResult Index(int? id, int? contentTypeId)
        {
            if (id.HasValue && contentTypeId.HasValue)
            {
                int courseIdValue = id.Value;
                int contentTypeIdValue = contentTypeId.Value;
                IEnumerable<LevelMaterial> materials = CourseService.GetCourseMaterials(courseIdValue, contentTypeIdValue).OrderByDescending(m => m.CreateDateTime); ;
                ViewBag.Materials = materials;
                ViewBag.CourseName = CourseService.GetCourse(courseIdValue).Name;
                ViewBag.Courseid = CourseService.GetCourse(courseIdValue).CourseId;
                ViewBag.Title = "Listi yfir kennsluefni - " + CourseService.GetContentTypeNameById(contentTypeIdValue);
                return View(materials);
            }

            return RedirectToAction("NotFound", "Home");
        }


        [HttpGet]
        [Authorize(Roles = "Student, Teacher")]
        public ActionResult Get(int? id)
        {
            if (id.HasValue)
            {
                var material = LevelService.GetLevelMaterial(id.Value);
                var filepath = Settings.ProjectMaterialVirtualFolder + material.ContentId.ToString();
        
                if (material.ContentType.ContentTypeId == 1)
                {
                    ViewBag.File = filepath;
                    ViewBag.CourseName = material.Level.Course.Name;
                    ViewBag.Courseid = material.Level.Course.CourseId;
                    ViewBag.Title = material.ContentType.Name;
                    ViewBag.Name = material.Title;
                    ViewBag.Description = material.Description;
                    ViewBag.LevelMaterialId = material.LevelMaterialId;
                    return View(material);
                }
                else
                {
                    return new DownloadResult { VirtualPath = filepath, FileDownloadName = material.Filename };
                }


            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Student, Teacher")]
        public ActionResult Download(int? id)
        {
            if (id.HasValue)
            {
                var material = LevelService.GetLevelMaterial(id.Value);
                var filepath = Settings.ProjectMaterialVirtualFolder + material.ContentId.ToString();
             
                return new DownloadResult {VirtualPath = filepath, FileDownloadName = material.Filename};
            }
            return RedirectToAction("NotFound", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int? id)
        {

            ViewBag.LevelCount = GetLevelCounts(id.Value);
            ViewBag.ContentTypes = LevelService.GetContentTypes();
            ViewBag.CourseId = id.Value;
            ViewBag.CourseName = CourseService.GetCourse(id.Value).Name;
            ViewBag.Courseid = CourseService.GetCourse(id.Value).CourseId;
            ViewBag.Title = "Búa til kennsluefni";

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(LevelMaterial levelMaterial, int? id)
        {

            if (ModelState.IsValid)
            {
                if (levelMaterial.File.FirstOrDefault() != null)
                {
                    foreach (var file in levelMaterial.File)
                    {
                        Guid contentId = Guid.NewGuid();
                        if (file.ContentLength > 0)
                        {
                            var path = Path.Combine(Server.MapPath("~/Upload"), contentId.ToString());
                            ViewBag.ContentId = contentId;
                            file.SaveAs(path);
                            levelMaterial.ContentId = contentId;
                            levelMaterial.Filename = file.FileName;
                        }
                    }
                }
                
                levelMaterial.CreateDateTime = DateTime.Now;
                LevelService.CreateLevelMaterial(levelMaterial, id.Value);


                return RedirectToAction("Get", new { id = levelMaterial.LevelMaterialId });

            }
            ViewBag.LevelCount = GetLevelCounts(id.Value);
            ViewBag.ContentTypes = LevelService.GetContentTypes();
            ViewBag.CourseId = id.Value;

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {


            if (id.HasValue)
            {
                var material = LevelService.GetLevelMaterial(id.Value);
                var courseId = material.Level.CourseId;
                ViewBag.LevelCount = GetLevelCounts(courseId);
                ViewBag.ContentTypes = LevelService.GetContentTypes();
                ViewBag.CourseName = CourseService.GetCourse(courseId).Name;
                ViewBag.Courseid = CourseService.GetCourse(courseId).CourseId;
                ViewBag.Title = "Breyta kennsluefni";

                return View(material);
            }
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(LevelMaterial levelMaterial, int? id)
        {
            if (ModelState.IsValid)
            {
                var material = LevelService.GetLevelMaterial(levelMaterial.LevelMaterialId);
                var courseId = material.Level.CourseId;
                if (TryUpdateModel(material))
                {
                    if (material.File != null)
                    {
                        foreach (var file in material.File)
                        {
                            Guid contentId = Guid.NewGuid();
                            if (file.ContentLength > 0)
                            {
                                var path = Path.Combine(Server.MapPath("~/Upload"), contentId.ToString()); //TODO: Add function to check for file extensions
                                ViewBag.ContentId = contentId;
                                file.SaveAs(path);
                                material.ContentId = contentId;
                                material.Filename = file.FileName;
                            }
                        }
                    }

                    ViewBag.CourseName = CourseService.GetCourse(courseId).Name;
                    ViewBag.Courseid = CourseService.GetCourse(courseId).CourseId;
                    ViewBag.Title = "Breyta kennsluefni";
                    ViewBag.LevelCount = GetLevelCounts(courseId);
                    ViewBag.ContentTypes = LevelService.GetContentTypes();
                    ViewBag.SuccessMessage = "Kennslugagn hefur verið uppfært";

                    LevelService.UpdateLevelMaterial(material);
                    return View(levelMaterial);
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Gat ekki uppfært kennslugagn! Lagfærðu villur og reyndur aftur.";
                if (id.HasValue)
                {
                    return View(LevelService.GetLevelMaterial(id.Value));
                }   
            }
            ViewBag.LevelCount = GetLevelCounts(levelMaterial.Level.CourseId);
            ViewBag.LevelMaterialId = levelMaterial.LevelMaterialId;
            ViewBag.ContentTypes = LevelService.GetContentTypes();
            return View();
        }

        public IEnumerable<SelectListItem> GetLevelCounts(int courseId)
        {
            for (int j = 0; j <= LevelService.GetLevels(courseId).Count(); j++)
            {
                var elementAtOrDefault = LevelService.GetLevels(courseId).ElementAtOrDefault(j);
                if (elementAtOrDefault != null)
                    yield return new SelectListItem
                    {
                        Text = (elementAtOrDefault.Name == null ? "None" : elementAtOrDefault.Name),
                        Value = (elementAtOrDefault.LevelId.ToString() == "" ? "0" : elementAtOrDefault.LevelId.ToString())
                    };
            }
        }
    }
}