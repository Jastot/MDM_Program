using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arhive_MDM.Models
{
    class Case
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public Worker Worker { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public DateTime TimeCreated { get; set; }

        public DateTime TimeCompleted { get; set; }

        public ICollection<Documents> Documents { get; set; }
    }
}
