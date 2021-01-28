using System;
using System.ComponentModel.DataAnnotations;
namespace MySql.Data.Core
{
    public class Salesreturn
    {
        public int Id { get; set; }
        [Required, StringLength(45)]
        public string InvoiceNo { get; set; }
        public double VatAmount { get; set; }
        public double SubTotalAmount { get; set; }
        public double TotalAmount { get; set; }
       public int UserID { get; set; }
        public DateTime Returedate { get; set; }
    }
}
