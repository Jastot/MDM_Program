using System;
using System.Collections.Generic;

namespace Arhive_MDM.Models
{
    class Documents 
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Orders Orders { get; set; }

        public string FileName { get; set; }

        public string FileLink { get; set; }

        public DateTime TimeCreated { get; set; }

    }
}
