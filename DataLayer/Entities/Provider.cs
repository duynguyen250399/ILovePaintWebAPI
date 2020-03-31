using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Provider
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(11)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(30)]
        public string Email { get; set; }
       
    }

}
