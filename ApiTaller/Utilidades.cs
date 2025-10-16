using ApiTaller.DTO;
using ApiTaller.Modelo;
using Nancy.Json;
using System.Collections.Generic;
using System.Data;
using System;
using Microsoft.VisualBasic;
using XSystem.Security.Cryptography;

namespace ApiTaller
{
    public static class Utilidades
    {

        public static UsuarioDTO ConvertirDTO(this UsuarioAPI u)
        {
            if (u != null)
            {
                return new UsuarioDTO
                {
                    Token = u.Token,
                    Usuario = u.Usuario
                };
            }
            return null;
        }

        public static object DataTableToJSON(DataTable table)
        {
            var list = new List<Dictionary<string, object>>();
            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = (Convert.ToString(row[col]));
                }
                list.Add(dict);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(list);
        }

        public static string RutaDocumentoFirma(string ruta, string codigo)
        {
            try
            {
                string data = "";
                data = "[{" + Constants.vbLf + "  \"codigo\":" + "\"" + codigo + "\"" + "," + Constants.vbLf + "  \"descripcion\": " + "\"" + ruta + "\""  + Constants.vbLf + " } ]";
                string resultado = data ;
                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static byte[] Encriptar3S(string password)
        {
            SHA256Managed objSha256 = new SHA256Managed();
            byte[] objTemporal;
            try
            {
                objTemporal = System.Text.Encoding.UTF8.GetBytes(password);
                objTemporal = objSha256.ComputeHash(objTemporal);
                return objTemporal;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objSha256.Clear();
            }
        }


    }
}
