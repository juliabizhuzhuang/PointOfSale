using System.ComponentModel.DataAnnotations;

namespace MySql.Data.Core
{
    public class Staff
    {
        public int StaffId { get; set; }

        [Required, StringLength(45)]
        public string Lastnam { get; set; }

        [Required, StringLength(45)]
        public string Firstname { get; set; }

        [Required, StringLength(1)]
        public string MI { get; set; }
        [Required, StringLength(45)]
        public string Street { get; set; }
        [Required, StringLength(45)]
        public string Barangay { get; set; }
        [Required, StringLength(45)]
        public string City { get; set; }
        [Required, StringLength(45)]
        public string Province { get; set; }
        [Required, StringLength(45)]
        public string ContactNo { get; set; }
        [Required, StringLength(45)]
        public string User { get; set; }
        [Required, StringLength(45)]
        public string Role { get; set; }
        [Required, StringLength(45)]
        public string Upassword { get; set; }
    }
}

