using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arhive_MDM.Models
{
    class Documents // Я бы убрал s, для Orders тоже. Всё-таки это модель одиночного объекта.yesyes
    {
        public int Id { get; set; }

        public int CaseId { get; set; }

        public Case Case { get; set; }

        public string FileName { get; set; }

        public byte[] File { get; set; }

        public DateTime TimeCreated { get; set; }

    }
}
