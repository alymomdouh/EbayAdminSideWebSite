﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayView.Models.ViewModel.Products
{
    public class CreateProductInputModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int AdminId { get; set; }
        public string AdminName { get; set; } 
        public int CatId { get; set; }
        public int BrandId { get; set; }
        public int StockId { get; set; }
        public int SubCatId { get; set; }
        public string[] imgspathes { get; set; }// inside controller insert imgs path
    }
}