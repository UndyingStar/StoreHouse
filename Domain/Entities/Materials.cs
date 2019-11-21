using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Materials
    {
        [Key]
        [HiddenInput(DisplayValue=false)]
        [Display(Name = "ID")]
        public int MaterialID { get; set; }

        [Display(Name="Название")]
        [Required(ErrorMessage="Пожалуйста, введите название")]
        public string Title { get; set; }

        [Display(Name = "Количество")]
        [Required(ErrorMessage = "Пожалуйста, укажите количество")]
        public int Amount { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание")]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Единица измерения")]
        [Required(ErrorMessage = "Пожалуйста, введите единицу измерения")]
        public string Unit { get; set; }

        [Display(Name = "Тип материала")]
        [Required(ErrorMessage = "Пожалуйста, укажите тип")]
        public string Type { get; set; }

        [Display(Name = "Цена (руб)")]
        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage = "Пожалуйста, введите значение цены")]
        public decimal Price { get; set; }
    }
}
