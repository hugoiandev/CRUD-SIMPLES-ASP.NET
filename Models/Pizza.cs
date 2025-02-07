using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace ASP.NET.Models
{
    public class Pizza
    {

        [Key]
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        public bool? IsGlutenFree { get; set; }

    }
}
