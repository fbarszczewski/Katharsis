using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katharsis.model
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Date { get; set; }

        public int Rma_Id { get; set; }
    }
}
