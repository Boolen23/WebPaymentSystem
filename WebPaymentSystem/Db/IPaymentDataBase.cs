using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPaymentSystem.Db
{
    public interface IPaymentDataBase
    {
        IEnumerable<Payment> GetPayments(int PageNumber, int PageLenght, DateTime? date, string CustomerName, string sortOrder);
    }
}
