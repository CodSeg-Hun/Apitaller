using ApiTaller.Repositorio.IRepository;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using Microsoft.Extensions.Logging;
using System.Text;

namespace ApiTaller.Repositorio
{
    public class ConsultarSQLServer : IConsultarEnMemoria
    {
        private string CadenaConexion;
        private readonly ILogger<ConsultarSQLServer> log;
        public ConsultarSQLServer(AccesoDatos cadenaConexion, ILogger<ConsultarSQLServer> l)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
            this.log = l;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public static DataSet LoginUsuarios(string usuario, string password, int opcion)
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string cadena = MyConfig.GetValue<string>("ConnectionStrings:SQL");
            DataSet ds = new DataSet();
            try
            {
                using SqlConnection connection = new SqlConnection(cadena);
                {
                    using SqlCommand command = new SqlCommand("TALLER.SP_APPTALLER_INICIO", connection);
                    {
                        connection.Open();
                        command.Parameters.Add("@OPCION", SqlDbType.Int).Value = opcion;
                        command.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = usuario;
                        command.Parameters.Add("@PASSWORD", SqlDbType.VarBinary).Value = Utilidades.Encriptar3S(password);
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
