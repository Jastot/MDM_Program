using System;
using System.Collections.Generic;


namespace Arhive_MDM.Models
{
    class OrderContent
    {
        public int Id { get; set; }

        public int OrdersId { get; set; }

        public Orders Orders { get; set; }

        public string Info { get; set; }

        public int FileId { get; set; }
        public string FileLink { get; set; }
    }
}
