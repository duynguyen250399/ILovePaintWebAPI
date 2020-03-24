using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
