using Act3_Facturas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica02.Services;
using Practica02.Services.Contracts;

namespace Practica02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private IInvoiceManager invoiceController = new InvoiceManager();

        [HttpGet]
        public IActionResult Get([FromQuery]DateTime? date,[FromQuery] int? idPayment)
        {
            List<Invoice> invoices = invoiceController.Get((date != null) ? date : null,(idPayment != null) ? idPayment : null);
            if (invoices.Count > 0)
                return Ok(invoices);
            else
                return Ok("No hay facturas cargadas");
        }
        [HttpPost]
        public IActionResult Post([FromBody]Invoice invoice)
        {
            try
            {
                invoiceController.Save(invoice);
                return Ok("Se ha guardado la factura con exito");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put([FromBody]Invoice invoice)
        {
            try
            {
                invoiceController.Update(invoice);
                return Ok("Se ha actualizado la factura con exito");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
