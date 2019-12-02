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
    public class CartController : Controller
    {

        private IMaterialRepository materialRepository;
        private IOrderProcessor orderProcessor;
        private IOrdersRepository orderRepository;

        public CartController(IMaterialRepository repoM, IOrderProcessor processor, IOrdersRepository repoO)
        {
            materialRepository = repoM;
            orderRepository = repoO;
            orderProcessor = processor;
        }

        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel
                {
                    Cart = cart,
                    ReturnUrl = returnUrl
                });
        }

        public RedirectToRouteResult AddToCart(Cart cart,int materialID, string returnUrl)
        {
            Materials material = materialRepository.Materials
                .FirstOrDefault(m => m.MaterialID == materialID);

            if (material != null)
            { 
                cart.AddItem(material, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int materialID, string returnUrl)
        {
            Materials material = materialRepository.Materials
                .FirstOrDefault(m => m.MaterialID == materialID);

            if (material != null)
            {
                   
                cart.RemoveLine(material);
            }
            
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, корзина пуста!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                orderRepository.CreateOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(new ShippingDetails());
            }
        }        
    }
}