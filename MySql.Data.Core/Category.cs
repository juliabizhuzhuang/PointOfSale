using System.ComponentModel.DataAnnotations;

namespace MySql.Data.Core
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(80)]
        public string CategoryName { get; set; }
        [Required, StringLength(255)]
        public string Description { get; set; }
       }
}
