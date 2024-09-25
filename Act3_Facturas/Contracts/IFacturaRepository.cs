using Act3_Facturas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act3_Facturas.Contracts
{
    public interface IFacturaRepository
    {
        bool Save(Invoice invoice);
        List<Invoice> Get(DateTime? date, int? idPaymentType);
        bool Update(Invoice invoice);
    }
}
