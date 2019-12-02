using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Укажите Ваше имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Адрес доставки")]
        [Display(Name="Первый адрес")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Укажите счет для оплаты")]
        [Display(Name = "Счет для оплаты")]
        public string paymentBill { get; set; }

        [Required(ErrorMessage = "Укажите почту для обратной связи")]
        [Display(Name = "Почта")]
        public string email { get; set; }

        public bool GiftWrap { get; set; }
    }
}
