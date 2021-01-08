using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arhive_MDM.Models
{
    class Worker
    {
        public int Id { get; set; }

        [Required] public string FIO { get; set; }

        [Required] public string Login { get; set; }

        [Required] public string Password { get; set; }

        [Required] public string Role { get; set; }

        [Required] public float Salary { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
