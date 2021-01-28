using System;
using System.Collections.Generic;
using System.Text;

namespace MySql.Data.Core
{
   public class Payment
    {
        public int Id { get; set; }
        public int InvoiceNo { get; set; }
        public double Cash { get; set; }
        public double PChange { get; set; }

    }
}
