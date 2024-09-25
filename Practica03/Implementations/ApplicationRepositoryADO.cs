using Practica02.Contracts;
using Practica02.Models;
using Practica02.Properties;
using System.Data;
using System.Data.SqlClient;

namespace Practica02.Implementations
{
    public class ApplicationRepositoryADO : IApplicationRepository
    {
        public bool Delete(int id)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_DELETE_ARTICLE", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_ARTICLE", id);
                result = cmd.ExecuteNonQuery() == 1;
                t.Commit();
            } catch (Exception ex)
            {
                t.Rollback();
                Console.WriteLine(ex.ToString());
            } finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return result;
        }

        public List<Articulo> GetAll()
        {
            List<Articulo> articulos = new List<Articulo>();
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            try
            {
                cnn.Open();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_GET_ALL_ARTICLES",cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
                foreach(DataRow row in dt.Rows)
                {
                    Articulo art = new Articulo()
                    {
                        IdArticulo = (int)row[0],
                        Descripcion = (string)row[1],
                        PrecioUnitario = Convert.ToDouble(row[2])
                    };
                    articulos.Add(art);
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return articulos;
        }

        public Articulo GetById(int id)
        {
            Articulo result = new Articulo();
            SqlConnection cnn = new SqlConnection (Resources.CnnString);
            try
            {
                cnn.Open();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_GET_ARTICLE_BY_ID", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_ARTICLE", id);
                dt.Load(cmd.ExecuteReader());
                foreach(DataRow row in dt.Rows)
                {
                    result.IdArticulo =(int) row[0];
                    result.Descripcion = (string)row[1];
                    result.PrecioUnitario = Convert.ToDouble(row[2]);
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            } finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return result;
        }

        public bool Save(Articulo articulo)
        {
            bool result = false;
            SqlConnection cnn = new SqlConnection(Resources.CnnString);
            SqlTransaction t = null;
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("SP_ADD_ARTICLE", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NAME", articulo.Descripcion);
                cmd.Parameters.AddWithValue("@UNIT_PRICE",(double) articulo.PrecioUnitario);
                if (articulo.IdArticulo != null)
                {
                    cmd.CommandText = "SP_UPDATE_ARTICLE";
                    cmd.Parameters.AddWithValue("@ID_ARTICLE",articulo.IdArticulo);
                }
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
