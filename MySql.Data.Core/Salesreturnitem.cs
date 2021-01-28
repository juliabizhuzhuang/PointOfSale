using System.ComponentModel.DataAnnotations;
namespace MySql.Data.Core
{
    public class Salesreturnitem
    {
        public int Id { get; set; }
        [Required, StringLength(45)]
        public string InvoiceNo { get; set; }
  public int ProductID { get; set; }
        public double Quantity { get; set; }
    }
}
