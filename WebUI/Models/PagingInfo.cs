﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }     // Общее кол-во товаров
        public int ItemsPerPage { get; set; }   // Кол-во товаров на странице
        public int CurrentPage { get; set; }    // Номер текущей страницы
        public int TotalPages                   // Общее кол-во страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}