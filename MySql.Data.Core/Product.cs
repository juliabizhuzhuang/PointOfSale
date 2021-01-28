using System.ComponentModel.DataAnnotations;

namespace MySql.Data.Core
{
    public class Product
    {
        public int Id { get; set; }
        [Required, StringLength(80)]
        public string ProductCode { get; set; }
        [Required, StringLength(255)]
        public string Description { get; set; }
        [Required, StringLength(50)]
        public string Barcode { get; set; }
        [Required, StringLength(255)]
        public int MaxJsonLength { get; set; }
        [Required, StringLength(255)]
        public int ReorderLevel { get; set; }
        public int CategoryNo { get; set; }
    }
}
