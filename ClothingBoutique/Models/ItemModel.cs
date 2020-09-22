using System.ComponentModel.DataAnnotations;

namespace ClothingBoutique.Models
{
    public class ItemModel 
    {
        [Key]
        public int id{get;set;}
        public string itemName{get;set;}
        // TODO : Add error message
        [Range(0,200)]
        public int price{get;set;}
        public string itemCategory{get;set;}
        public bool inStock{get;set;}
        public bool featured{get;set;}
    }
}