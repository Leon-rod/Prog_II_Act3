using Act3_Facturas.Models;

namespace Practica02.Services.Contracts
{
    public interface IInvoiceManager
    {
        List<Invoice> Get(DateTime? date, int? idPaymenType);
        bool Save(Invoice invoice);
        bool Update(Invoice invoice);

    }
}
