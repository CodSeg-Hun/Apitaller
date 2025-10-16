namespace ApiTaller.Clases
{
    public class ConexionLatam
    {
        public string GetRuta(string opcion)
        {
            string ruta = "";
            // ******************************************************************************************
            // desarrollo
            // '******************************************************************************************

            if (opcion == "1")
                ruta = "APPLATAM"; //usuario obtener token
            else if (opcion == "2")
                ruta = "4PPL4T$M2024";  //clave obtener token
            else if (opcion == "3")
                ruta = "https://sys.hunterlojack.com/Auth/token";
            else if(opcion == "4")
                ruta = "https://sys.hunterlojack.com/SIT/APP/appLatamloginUsuario";

            return ruta;
        }
    }
}
