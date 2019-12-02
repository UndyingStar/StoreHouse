using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class MaterialsController : Controller
    {
        private IMaterialRepository repository;
        public int pageSize = 4;

        public MaterialsController(IMaterialRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string type, int page = 1)
        {
            MaterialsListViewModel model = new MaterialsListViewModel()
            {
                Materials = repository.Materials
                .Where(m => type == null || m.Type == type)
                .OrderBy(material => material.MaterialID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = type == null ? 
                        repository.Materials.Count() : 
                        repository.Materials.Where(book => book.Type == type).Count()
                },
                CurrentType = type
            };
            
            return View(model);
        }
    }
}