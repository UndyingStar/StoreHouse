using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Orders
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public int OrderID { get; set; }

        [Display(Name = "ФИО клиента")]
        public string ClientFullName { get; set; }

        [Display(Name = "Стоимость")]
        public decimal TotalPrice { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Статус")]
        public string Status { get; set; }

        [Display(Name = "Тип")]
        public string Type { get; set; }

        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

    }
}
