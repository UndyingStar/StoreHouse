using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
   public class Users
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public int UserID { get; set; }

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Пожалуйста, введите логин")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пожалуйста, введите пароль")]
        public string Password { get; set; }

        [Display(Name = "Роль")]
        [Required(ErrorMessage = "Пожалуйста, укажите роль")]
        public string Role { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Пожалуйста, введите фамилию")]
        public string FirstName { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Пожалуйста, введите имя")]
        public string LastName { get; set; }
    }
}
