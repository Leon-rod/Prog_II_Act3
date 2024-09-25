using Act3_Facturas.Contracts;
using Act3_Facturas.Implementations;
using Act3_Facturas.Models;
using Practica02.Services.Contracts;

namespace Practica02.Services
{
    public class InvoiceManager : IInvoiceManager
    {
        public IFacturaRepository _facturaRepository { get; set; }
        public InvoiceManager()
        {
            this._facturaRepository = new FacturaRepository();
        }
        public List<Invoice> Get(DateTime? date, int? idPaymenType)
        {
            return _facturaRepository.Get(date, idPaymenType);
        }

        public bool Save(Invoice invoice)
        {
            return _facturaRepository.Save(invoice);
        }

        public bool Update(Invoice invoice)
        {
            return _facturaRepository.Update(invoice);
        }
    }
}
