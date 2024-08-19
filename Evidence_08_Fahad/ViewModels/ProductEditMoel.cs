using Evidence_08_Fahad.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Evidence_08_Fahad.ViewModels
{
    public class ProductEditMoel
    {
        public int ProductId { get; set; }
        [Required, StringLength(50)]
        public string ProductName { get; set; } = default!;
        [Required, Column(TypeName = "money")]
        public decimal? Price { get; set; }
        [Required, DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ExpireDate { get; set; }
        public IFormFile? Picture { get; set; } = default!;
        public virtual List<Sale> Sales { get; set; } = new List<Sale>();
    }
}
