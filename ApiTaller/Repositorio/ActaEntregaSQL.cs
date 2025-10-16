using ApiTaller.Repositorio.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System;
using System.Data.SqlClient;

namespace ApiTaller.Repositorio
{
    public class ActaEntregaSQL : IActaEntrega
    {

        private string CadenaConexion;
        private readonly ILogger<ActaEntregaSQL> log;
        public ActaEntregaSQL(AccesoDatos cadenaConexion, ILogger<ActaEntregaSQL> l)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
            this.log = l;
        }

        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public static DataSet ConsultarActaEntrega(string ordenServicio, string vehiculo, string idcliente, string usuario, string opcion)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string cadena = MyConfig.GetValue<string>("ConnectionStrings:SQL");
            DataSet ds = new DataSet();
            try
            {
                using SqlConnection connection = new(cadena);
                {
                    using SqlCommand command = new("TALLER.SP_APPTALLER_ACTA_ENTREGA", connection);
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@OPCION", SqlDbType.Int).Value = opcion;
                        command.Parameters.Add("@USUARIO_ID", SqlDbType.Int).Value = usuario;
                        command.Parameters.Add("@VEHICULO_ID", SqlDbType.VarChar).Value = vehiculo;
                        command.Parameters.Add("@ORDEN_SERVICIO", SqlDbType.VarChar).Value = ordenServicio;
                        command.Parameters.Add("@CLIENTE", SqlDbType.VarChar).Value = idcliente;
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
