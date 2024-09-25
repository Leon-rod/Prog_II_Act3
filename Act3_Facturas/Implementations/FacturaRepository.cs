using Act3_Facturas.Contracts;
using Act3_Facturas.Models;
using Act3_Facturas.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act3_Facturas.Implementations
{
    public class FacturaRepository : IFacturaRepository
    {
        public List<Invoice> Get(DateTime? date, int? idPaymentType)
        {
            List<Invoice> result = new List<Invoice>();
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            try
            {
                cnn.Open();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_GET_INVOICE_BY_FILTERS",cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (date != null )
                    cmd.Parameters.AddWithValue("@DATE", date);
                if (idPaymentType != null)
                    cmd.Parameters.AddWithValue("@PAYMENT_TYPE", idPaymentType);
                dt.Load(cmd.ExecuteReader());
                foreach(DataRow row in dt.Rows)
                {
                    Invoice oInvoice = new Invoice()
                    {
                        Id = (int)row[0],
                        DateRegister = (DateTime)row[1],
                        paymentType = new PaymentType()
                        {
                            Id = (int)row[2]
                        },
                        Client = (string) row[3]
                    };
                    result.Add(oInvoice);
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public bool Save(Invoice invoice)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_ADD_INVOICE",cnn,t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DATE", invoice.DateRegister);
                cmd.Parameters.AddWithValue("@ID_PAYMENT_TYPE", invoice.paymentType.Id);
                cmd.Parameters.AddWithValue("@CLIENT", invoice.Client);
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@ID_INVOICE",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(param);
                result = cmd.ExecuteNonQuery() == 1;
                t.Commit();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                t.Rollback();
            } finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open) 
                    cnn.Close();
            }
            return result;
        }

        public bool Update(Invoice invoice)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_UPDATE_INVOICE", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DATE", invoice.DateRegister);
                cmd.Parameters.AddWithValue("@ID_PAYMENT_TYPE", invoice.paymentType.Id);
                cmd.Parameters.AddWithValue("@CLIENT", invoice.Client);
                cmd.Parameters.AddWithValue("@ID_INVOICE", invoice.Id);
                result = cmd.ExecuteNonQuery() == 1;
                t.Commit();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                t.Rollback();
            } finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return result;
        }
    }
}
