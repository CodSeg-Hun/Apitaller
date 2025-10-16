using ApiTaller.Repositorio.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System;
using System.Data.SqlClient;

namespace ApiTaller.Repositorio
{
    public class ServicioSQL : IServicio
    {

        private string CadenaConexion;
        private readonly ILogger<ServicioSQL> log;
        public ServicioSQL(AccesoDatos cadenaConexion, ILogger<ServicioSQL> l)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
            this.log = l;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }


        public static DataSet ConsultarDispositivo(string orden, string usuario, string vehiculo, string opcion)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string cadena = MyConfig.GetValue<string>("ConnectionStrings:SQL");
            DataSet ds = new DataSet();
            try
            {
                using SqlConnection connection = new(cadena);
                {
                    using SqlCommand command = new("TALLER.SP_APPTALLER_DISPOSITIVO_SERVICIO", connection);
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@OPCION", SqlDbType.Int).Value = opcion;
                        command.Parameters.Add("@ORDEN_SERVICIO", SqlDbType.VarChar).Value = orden;
                        command.Parameters.Add("@USUARIO_ID", SqlDbType.Int).Value = usuario;
                        command.Parameters.Add("@VEHICULO_ID", SqlDbType.VarChar).Value = vehiculo;
                        var result = command.ExecuteReader();
                        ds.Load(result, LoadOption.OverwriteChanges, "Table");
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }


        public static DataSet ConsultandoDatosControl(string orden, string usuario, string vehiculo, string empresa, string opcion)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string cadena = MyConfig.GetValue<string>("ConnectionStrings:SQL");
            DataSet ds = new DataSet();
            // UnidadRespDTO u = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(cadena))
                {
                    using (SqlCommand command = new SqlCommand("TALLER.SP_APPTALLER_CONTROL_CALIDAD", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@OPCION", SqlDbType.Int).Value = opcion;
                        command.Parameters.Add("@NUMERO_ORDEN_SERVICIO", SqlDbType.VarChar).Value = orden;
                        command.Parameters.Add("@USUARIO_ID", SqlDbType.Int).Value = usuario;
                        command.Parameters.Add("@CODIGO_VEHICULO", SqlDbType.VarChar).Value = vehiculo;
                        command.Parameters.Add("@CODIGO_EMPRESA", SqlDbType.VarChar).Value = empresa;
                        var result = command.ExecuteReader();
                        ds.Load(result, LoadOption.OverwriteChanges, "Table");
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


    }
}
