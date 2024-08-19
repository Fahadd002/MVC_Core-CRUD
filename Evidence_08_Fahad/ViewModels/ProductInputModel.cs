using Evidence_08_Fahad.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Evidence_08_Fahad.ViewModels
{
    public class ProductInputModel
    {
        public int ProductId { get; set; }
        [Required, StringLength(50), Display(Name = "Product name")]
        public string ProductName { get; set; } = default!;
        [Required, Column(TypeName = "money")]
        public decimal? Price { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Ex. Date")]
        public DateTime? ExpireDate { get; set; }
        [Required]
        public IFormFile Picture { get; set; } = default!;
        public virtual List<Sale> Sales { get; set; } = new List<Sale>();
    }
}
