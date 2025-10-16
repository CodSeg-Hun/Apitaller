using ApiTaller.Repositorio.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System;
using System.Data.SqlClient;

namespace ApiTaller.Repositorio
{
    public class RecepcionSQL : IRecepcion
    {

        private string CadenaConexion;
        private readonly ILogger<RecepcionSQL> log;
        public RecepcionSQL(AccesoDatos cadenaConexion, ILogger<RecepcionSQL> l)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
            this.log = l;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public static DataSet ConsultarRecepcion(string usuario, string identificacion, string placa, string taller, string orden, string vehiculo, string opcion)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string cadena = MyConfig.GetValue<string>("ConnectionStrings:SQL");
            DataSet ds = new DataSet();
            // UnidadRespDTO u = null;
            try
            {
                using SqlConnection connection = new (cadena);
                {
                    using SqlCommand command = new ("TALLER.SP_APPTALLER_CONSULTA", connection);
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@OPCION", SqlDbType.Int).Value = opcion;
                        command.Parameters.Add("@USUARIO_ID", SqlDbType.Int).Value = usuario;
                        command.Parameters.Add("@TALLER_ID", SqlDbType.Int).Value = taller;
                        command.Parameters.Add("@ORDEN_SERVICIO", SqlDbType.VarChar).Value = orden;
                        command.Parameters.Add("@PLACA", SqlDbType.VarChar).Value = placa;
                        command.Parameters.Add("@CLIENTE_ID", SqlDbType.VarChar).Value = identificacion;
                        command.Parameters.Add("@VEHICULO", SqlDbType.Int).Value = vehiculo;
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


        public static DataSet ConsultarDatosRecepcionGeneral(string fechaini, string fechafin,  string usuario, string opcion)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string cadena = MyConfig.GetValue<string>("ConnectionStrings:SQL");
            DataSet ds = new DataSet();
            try
            {
                using SqlConnection connection = new SqlConnection(cadena);
                {
                    using SqlCommand command = new SqlCommand("TALLER.SP_APPTALLER_RECEPCION", connection);
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@OPCION", SqlDbType.Int).Value = opcion;
                        command.Parameters.Add("@USUARIO_ID", SqlDbType.Int).Value = usuario;
                        command.Parameters.Add("@FECHAINI", SqlDbType.VarChar).Value = fechaini;
                        command.Parameters.Add("@FECHAFIN", SqlDbType.VarChar).Value = fechafin;
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


        public static DataSet ConsultarRecepcionTecnico(string usuario, string vehiculo, string orden, string taller, string cliente, string comentario, string opcion)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string cadena = MyConfig.GetValue<string>("ConnectionStrings:SQL");
            DataSet ds = new DataSet();
            try
            {
                using SqlConnection connection = new (cadena);
                {
                    using SqlCommand command = new ("TALLER.SP_APPTALLER_RECEPCION", connection);
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@OPCION", SqlDbType.Int).Value = opcion;
                        command.Parameters.Add("@USUARIO_ID", SqlDbType.Int).Value = usuario;
                        command.Parameters.Add("@VEHICULO_ID", SqlDbType.VarChar).Value = vehiculo;
                        command.Parameters.Add("@ORDEN_SERVICIO", SqlDbType.VarChar).Value = orden;
                        command.Parameters.Add("@TALLER", SqlDbType.VarChar).Value = taller;
                        command.Parameters.Add("@COMENTARIO_TECNICO", SqlDbType.VarChar).Value = comentario;
                        command.Parameters.Add("@CLIENTE", SqlDbType.VarChar).Value = cliente;
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



    }
}
