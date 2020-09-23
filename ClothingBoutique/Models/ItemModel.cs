using System.ComponentModel.DataAnnotations;

namespace ClothingBoutique.Models
{
    public class ItemModel 
    {
        [Key]
        public int id{get;set;}
        [Display(Name="Name")]
        public string itemName{get;set;}

        [Display(Name="Price")]
        [Range(0,200, ErrorMessage = "An item cannot be more than $200")]
        public int price{get;set;}

        [Display(Name="Category")]
        public string itemCategory{get;set;}

        [Display(Name="In Stock")]
        public bool inStock{get;set;}

        [Display(Name="Featured")]
        public bool featured{get;set;}
    }
}