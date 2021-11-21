using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPaymentSystem.Db
{
    public class PaymentData : IPaymentDataBase
    {
        public PaymentData(string ConnString)
        {
            this.ConnectionString = ConnString;
        }
        private string ConnectionString { get; set; }
        public IEnumerable<Payment> GetPayments(int PageNumber, int PageLenght, DateTime? date, string CustomerName, string sortOrder)
        {
            List<Payment> result = new List<Payment>();

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder("select c.Name, pm.Sum, pm.PaymentDate from Payment pm left join Customer c on c.Customer_ID = pm.Customer_ID where c.Name like ifnull($CustomerName, c.Name) and pm.PaymentDate = ifnull($PaymentDate, pm.PaymentDate)");

                var command = connection.CreateCommand();
                command.Parameters.AddWithValue("$CustomerName", $"%{CustomerName}%");
                command.Parameters.AddWithValue("$PaymentDate", date.HasValue ? date.Value.ToString("yyyy-MM-dd") : DBNull.Value);
                if (!string.IsNullOrEmpty(sortOrder)) sb.Append(" order by ").Append(sortOrder);
                sb.Append($" limit {PageLenght} offset {(PageNumber - 1) * PageLenght};");

                command.CommandText = sb.ToString();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(0);
                        double sum = reader.GetDouble(1);
                        DateTime Pdate = DateTime.Parse(reader[2].ToString());
                        result.Add(new Payment() { CustomerName = name, PaymentDate = Pdate, Sum = sum });
                    }
                }
            }
            return result;
        }
    }
}
