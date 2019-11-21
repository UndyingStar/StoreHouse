using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IMaterialRepository repository;

        public NavController(IMaterialRepository repo)
        {
            repository = repo;
        }
        
        public PartialViewResult Menu(string type = null)
        {
            ViewBag.SelectedType = type;

            IEnumerable<string> types = repository.Materials
                .Select(material => material.Type)
                .Distinct()
                .OrderBy(x => x);
            
            return PartialView("FlexMenu",types);
        }
    }
}