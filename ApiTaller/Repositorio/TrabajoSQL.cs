using ApiTaller.Repositorio.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System;
using System.Data.SqlClient;

namespace ApiTaller.Repositorio
{
    public class TrabajoSQL : ITrabajo
    {

        private string CadenaConexion;
        private readonly ILogger<TrabajoSQL> log;
        public TrabajoSQL(AccesoDatos cadenaConexion, ILogger<TrabajoSQL> l)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
            this.log = l;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }


        public static DataSet ConsultarGenerales(string orden, string trabajo, string dispositivo, string tipoDispositivo, string usuario, string opcion)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string cadena = MyConfig.GetValue<string>("ConnectionStrings:SQL");
            DataSet ds = new DataSet();
            try
            {
                using SqlConnection connection = new SqlConnection(cadena);
                {
                    using SqlCommand command = new SqlCommand("TALLER.SP_APPTALLER_TALLER", connection);
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@OPCION", SqlDbType.Int).Value = opcion;
                        command.Parameters.Add("@ORDEN_SERVICIO", SqlDbType.Int).Value = orden;
                        command.Parameters.Add("@ORDEN_TRABAJO", SqlDbType.Int).Value = trabajo;
                        command.Parameters.Add("@TIPO_DISPOSITIVO", SqlDbType.VarChar).Value = tipoDispositivo;
                        command.Parameters.Add("@DISPOSITIVO", SqlDbType.VarChar).Value = dispositivo;
                        command.Parameters.Add("@INCIDENCIA", SqlDbType.Int).Value = usuario;
                        command.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = usuario;
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


        public static DataSet ConsultaServicios(string orden,  string opcion)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string cadena = MyConfig.GetValue<string>("ConnectionStrings:SQL");
            DataSet ds = new DataSet();
            try
            {
                using SqlConnection connection = new SqlConnection(cadena);
                {
                    using SqlCommand command = new SqlCommand("TALLER.SP_APPTALLER_CONSULTA_AMI", connection);
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@OPCION", SqlDbType.Int).Value = opcion;
                        command.Parameters.Add("@ORDEN_SERVICIO", SqlDbType.VarChar).Value = orden;
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
