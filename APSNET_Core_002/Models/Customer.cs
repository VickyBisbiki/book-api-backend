using APSNET_Core_002.Controllers;
using System.ComponentModel.DataAnnotations;

namespace APSNET_Core_002.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }    

        [Required]
        [EmailAddress]
        public string Email { get; set; }


    }
}
