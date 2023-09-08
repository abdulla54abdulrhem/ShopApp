using System.ComponentModel.DataAnnotations;

namespace Market.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required,MaxLength(150)]
        public string Name { get; set; } 
        public string ?Description { get; set;}
        [Display(Name="Choose the picture")]

        public IFormFile? Picture { get; set;}
        public string ?PictureUrl { get; set;}

        [Range(0, float.MaxValue, ErrorMessage = "The price must be positive")]
       
        public float ? Price { get; set;}


        [Range(0,int.MaxValue,ErrorMessage ="The quantity must be positive number")]
       
        public int quantity { get; set; }
        
        public string SellerId { get; set; }
        public virtual AppUser ?Seller { get; set; }
    }
}
