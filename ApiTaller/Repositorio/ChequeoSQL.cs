using ApiTaller.Repositorio.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System;
using System.Data.SqlClient;

namespace ApiTaller.Repositorio
{
    public class ChequeoSQL : IChequeo
    {

        private string CadenaConexion;
        private readonly ILogger<ChequeoSQL> log;
        public ChequeoSQL(AccesoDatos cadenaConexion, ILogger<ChequeoSQL> l)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
            this.log = l;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public static DataSet ConsultarTecnicoAsignar(string opcion, string usuario)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string cadena = MyConfig.GetValue<string>("ConnectionStrings:SQL");
            DataSet ds = new DataSet();
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


        public static DataSet ConsultarDatos(string fechaini, string fechafin, string tecnico, string taller, 
                                             string ordenServicio, string usuario, string convenio, string origen, string opcion)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string cadena = MyConfig.GetValue<string>("ConnectionStrings:SQL");
            DataSet ds = new DataSet();
            try
            {
                using SqlConnection connection = new (cadena);
                {
                    using SqlCommand command = new ("TALLER.SP_APPTALLER_CONSULTA", connection);
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@OPCION", SqlDbType.Int).Value = opcion;
                        command.Parameters.Add("@TECNICO_ID", SqlDbType.Int).Value = tecnico;
                        command.Parameters.Add("@FECHAINI", SqlDbType.VarChar).Value = fechaini;
                        command.Parameters.Add("@FECHAFIN", SqlDbType.VarChar).Value = fechafin;
                        command.Parameters.Add("@ORDEN_SERVICIO", SqlDbType.VarChar).Value = ordenServicio;
                        command.Parameters.Add("@USUARIO_ID", SqlDbType.Int).Value = usuario;
                        command.Parameters.Add("@TALLER_ID", SqlDbType.Int).Value = taller;
                        command.Parameters.Add("@CODIGO_CONVENIO", SqlDbType.VarChar).Value = convenio;
                        command.Parameters.Add("@ORIGEN_IMA", SqlDbType.VarChar).Value = origen;
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



        public static DataSet ConsultarDatosReasignacion(string fechaini, string fechafin, string ordenServicio, string vehiculo, string taller, 
                                                         string tecnico, string usuario, string opcion)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string cadena = MyConfig.GetValue<string>("ConnectionStrings:SQL");
            DataSet ds = new DataSet();
            try
            {
                using SqlConnection connection = new(cadena);
                {
                    using SqlCommand command = new("TALLER.SP_APPTALLER_CONSULTA", connection);
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@OPCION", SqlDbType.Int).Value = opcion;
                        command.Parameters.Add("@TECNICO_ID", SqlDbType.Int).Value = tecnico;
                        command.Parameters.Add("@FECHAINI", SqlDbType.VarChar).Value = fechaini;
                        command.Parameters.Add("@FECHAFIN", SqlDbType.VarChar).Value = fechafin;
                        command.Parameters.Add("@ORDEN_SERVICIO", SqlDbType.VarChar).Value = ordenServicio;
                        command.Parameters.Add("@USUARIO_ID", SqlDbType.Int).Value = usuario;
                        command.Parameters.Add("@TALLER_ID", SqlDbType.Int).Value = taller;
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

    }
}
