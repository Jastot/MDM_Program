using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arhive_MDM.Models
{
    class Client
    {
        public int Id { get; set; }

        [Required] public string FIO { get; set; }

        [Required] public string ContactNumber { get; set; }

        [Required] public string Address { get; set; }

        public ICollection<Case> Cases { get; set; }

        public ICollection<Orders> Orders { get; set; }

    }
}
