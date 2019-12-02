﻿using Domain.Abstract;
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
        IOrderProcessor orderProcessor;
        IUsersRepository repositoryU;

        public AdminController(IMaterialRepository repoM, IOrdersRepository repoO, IOrderProcessor processor, IUsersRepository repoU)
        {
            repositoryM = repoM;
            repositoryO = repoO;
            orderProcessor = processor;
            repositoryU = repoU;
        }

        [Authorize]
        public ViewResult Users()
        {
            if (User.Identity.Name.Contains("Admin"))
            {
                return View(repositoryU.Users);
            }
            else return View("NotEnoughRoots");
        }

        public ViewResult AddUser()
        {
            return View();
        }

        public ViewResult Edit(int materialID)
        {
            Materials material = repositoryM.Materials.FirstOrDefault(m => m.MaterialID == materialID);
            return View(material);
        }

        public ViewResult UpdateUser(int userID)
        {
            Users user = repositoryU.Users.FirstOrDefault(u => u.UserID == userID);
            return View(user);
        }

        [HttpPost]
        public ActionResult UpdateUser(Users user)
        {
            if (ModelState.IsValid)
            {
                repositoryU.SaveUser(user);
                TempData["message"] = string.Format("Изменение информации о пользователе \"{0}\" сохранены", user.Login);
                return RedirectToAction("Users");
            }
            else
            {
                return View(user);
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(Users user)
        {
            repositoryU.DeleteUser(user);
            return RedirectToAction("Users");
        }

        [HttpPost]
        public ActionResult AddUser(Users user)
        {
            repositoryU.CreateUser(user);
            return RedirectToAction("Users");
        }

        [Authorize]
        public ViewResult Index()
        {
            return View(repositoryM.Materials);
        }
        [Authorize]
        public ViewResult Orders()
        {
            return View(repositoryO.Orders);
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
        public ActionResult Engage(Orders order)
        {
            repositoryO.EngageOrder(order);
            orderProcessor.ProcessResponse(order);
            return RedirectToAction("Orders");
        }

        [HttpPost]
        public ActionResult Delievered(Orders order)
        {
            repositoryO.DelieveredOrder(order);
            return RedirectToAction("Orders");
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