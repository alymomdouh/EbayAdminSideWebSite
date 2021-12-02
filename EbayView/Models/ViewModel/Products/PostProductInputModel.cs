namespace EbayView.Models.ViewModel.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations; 
    using global::Models;

    public class PostProductInputModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Price is Required")]
        public float Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Description Is Required")]
        public string Description { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "AdminId Is Required")]
        public int AdminId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "CatId Is Required")]
        public int CatId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "BrandId Is Required")]
        public int BrandId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "StockId Is Required")]
        public int StockId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "SubCatId Is Required")]
        public int SubCatId { get; set; }


        //   added by aly   get names from db to select 
        public List<Category> AvailableCategories { get; set; }
        public List<SubCategory> AvailableSubCategories { get; set; }
        public List<Brands> AvailableBrands { get; set; }
        public List<Stock> AvailableStock { get; set; }
    }
}
