using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPaymentSystem.Db
{
    public struct Payment
    {
        public string CustomerName { get; set; }
        public double Sum { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
