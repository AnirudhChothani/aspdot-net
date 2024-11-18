using System.ComponentModel.DataAnnotations;

namespace Forjob.Models
{
    public class ProductModel
    {
        [Required]
        public int? ProductID { get; set; }
        [Required]
        public String ProductName { get; set; }
        [Required]
        public double ProductPrice { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int UserID { get; set; } = 101;
    }
}
