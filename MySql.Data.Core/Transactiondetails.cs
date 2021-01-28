using System.ComponentModel.DataAnnotations;


namespace MySql.Data.Core
{
    public class Transactiondetails
    {
        public int id { get; set; }
        [Required, StringLength(50)]
        public string InvoiceNo { get; set; }
       public int ProductNo { get; set; }
        public double ItemPrice { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
    }
}
