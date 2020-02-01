using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katharsis.model
{
    public class Rma
    {

        public int Id { get; set; }
        public string StartDate { get; set; }
        public string UpdateDate { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public string So { get; set; }
        public string InvoiceDate { get; set; }
        public Client Client { get; set; }
        public List<Product> Products { get; set; }
        public string Status { get; set; }

    }
}
