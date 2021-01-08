﻿using System;
using System.Collections.Generic;

namespace Arhive_MDM.Models
{
    class Orders
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int WorkerId { get; set; }

        public Worker Worker { get; set; }

        public int Payment { get; set; }

        public int PaymentIsDone { get; set; }

        public ICollection<OrderContent> OrderContents { get; set; }

        public ICollection<Documents> Documents { get; set; }
        
        public DateTime TimeCreated { get; set; }

        public DateTime TimeCompleted { get; set; }
    }
}
