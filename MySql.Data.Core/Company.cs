using System.ComponentModel.DataAnnotations;

namespace MySql.Data.Core
{
    public class Company
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public string Address { get; set; }

        [Required, StringLength(255)]
        public string PhoneNo { get; set; }

        [Required, StringLength(255)]
        public string Email { get; set; }

        [Required, StringLength(255)]
        public string Website { get; set; }

        [Required, StringLength(255)]
        public string TINNumber { get; set; }
        public double HInvoice { get; set; }
    }
}
