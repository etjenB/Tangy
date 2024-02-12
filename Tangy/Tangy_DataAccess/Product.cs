using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tangy_DataAccess
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ShopFavorites { get; set; }
        public bool CustomerFavorites { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
