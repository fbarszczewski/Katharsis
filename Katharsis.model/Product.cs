using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katharsis.model
{
    public class Product
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Rma_Id { get; set; }
    }
}
