using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ru.GameSchool.DataLayer.Repository;
using Ru.GameSchool.Web.Classes;

namespace Ru.GameSchool.Web.Controllers
{
    public class MaterialController : BaseController
    {
        //
        // GET: /Material/
        //[Authorize(Roles = "Student")
        //[Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<LevelMaterial> materials = LevelService.GetLevelMaterials(); ;
            ViewBag.Materials = materials;

            return View();
        }
        //[Authorize(Roles = "Student")]
        public ActionResult Get(int? LevelMaterialId)
        {
            if (LevelMaterialId.HasValue)
            {
                var material = LevelService.GetLevelMaterial(LevelMaterialId.Value);

                return View(material);

            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(int courseId)
        {
            ViewBag.LevelCount = GetLevelCounts(courseId);
            ViewBag.ContentTypes = LevelService.GetContentTypes();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(LevelMaterial levelMaterial, int courseId)
        {
            if (ModelState.IsValid)
            {
                ViewBag.LevelCount = GetLevelCounts(courseId);
                ViewBag.ContentTypes = LevelService.GetContentTypes();
                LevelService.CreateLevelMaterial(levelMaterial);
                //ViewBag.ContentTypes = LevelService.GetContentTypes();
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? levelMaterialId, int courseId)
        {
            ViewBag.LevelCount = GetLevelCounts(courseId);
            ViewBag.ContentTypes = LevelService.GetContentTypes();

            if (levelMaterialId.HasValue)
            {
                var material = LevelService.GetLevelMaterial(levelMaterialId.Value);
                return View(material);
            }
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? levelMaterialId, LevelMaterial levelMaterial, int courseId)
        {
            if (ModelState.IsValid)
            {
                ViewBag.LevelCount = GetLevelCounts(courseId);
                ViewBag.ContentTypes = LevelService.GetContentTypes();
                if (TryUpdateModel(levelMaterial))
                {
                    LevelService.UpdateLevelMaterial(levelMaterial);
                    ViewBag.SuccessMessage = "Kennslugagn hefur verið uppfært";
                    return View(levelMaterial);
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Gat ekki uppfært kennslugagn! Lagfærðu villur og reyndur aftur.";
                if (levelMaterialId != null) return View(LevelService.GetLevelMaterial(levelMaterialId.Value));
            }
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
                        Text = elementAtOrDefault.Name,
                        Value = elementAtOrDefault.LevelId.ToString()
                    };
            }
        }
    }
}