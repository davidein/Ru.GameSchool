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
            var materials = LevelService.GetLevelMaterials();
            ViewBag.Material = materials.ToList();

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
        public ActionResult Create()
        {
            ViewBag.LevelCount = GetLevelCounts();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Create(LevelMaterial levelMaterial)
        {
            if (ModelState.IsValid)
            {
                LevelService.CreateLevelMaterial(levelMaterial);
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id.HasValue)
                {
                    var material = LevelService.GetLevelMaterial(id.Value);
                    ViewBag.Material = material;
                }
            }
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult Edit(LevelMaterial levelMaterial)
        {
            if (ModelState.IsValid)
            {
                if (TryUpdateModel(levelMaterial))
                {
                    LevelService.UpdateLevelMaterial(levelMaterial);
                }
            }
            return View();
        }

        public IEnumerable<SelectListItem> GetLevelCounts()
        {
            for (int j = 1; j <= LevelService.GetLevels().Count(); j++)
            {
                yield return new SelectListItem
                {
                    Text = j.ToString(),
                    Value = j.ToString()
                };

            }
        }
    }
}