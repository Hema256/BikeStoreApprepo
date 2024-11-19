using System.ComponentModel.DataAnnotations;

namespace BikeStore_FrontEnd.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        // Navigation Properties
        //public ICollection<Product> Products { get; set; }
    }

}
