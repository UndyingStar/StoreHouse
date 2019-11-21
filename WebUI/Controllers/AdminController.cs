using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        IMaterialRepository repositoryM;
        IOrdersRepository repositoryO;

        public AdminController(IMaterialRepository repoM, IOrdersRepository repoO)
        {
            repositoryM = repoM;
            repositoryO = repoO;
        }

        public ViewResult Index()
        {
            return View(repositoryM.Materials);
        }

        public ViewResult Orders()
        {
            return View(repositoryO.Orders);
        }

        public ViewResult Edit(int materialID)
        {
            Materials material = repositoryM.Materials.FirstOrDefault(b => b.MaterialID == materialID);

            return View(material);
        }

        [HttpPost]
        public ActionResult Edit(Materials material)
        {
            if (ModelState.IsValid)
            {
                repositoryM.SaveMaterial(material);
                TempData["message"] = string.Format("Изменение информации о книге \"{0}\" сохранены", material.Title);
                return RedirectToAction("Index");
            }
            else
            {
                return View(material);
            }
        }

        [HttpPost]
        public ActionResult Delete(Materials material)
        {
                repositoryM.DeleteMaterial(material);
                return RedirectToAction("Index");
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Materials material)
        {
            repositoryM.CreateMaterial(material);
            return RedirectToAction("Index");
        }
    }
}