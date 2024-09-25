using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act3_Facturas.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime DateRegister { get; set; }
        public PaymentType paymentType { get; set; }
        public string Client { get; set; }
    }
}
