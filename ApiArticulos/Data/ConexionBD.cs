        namespace ApiArticulos.Data
        {
            public class ConexionBD
            {
                private readonly string cadenaSQL;
                public ConexionBD()
                {
                    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                    cadenaSQL = builder.GetSection("ConnectionStrings:Conexion").Value;
                }

                public string GetCadenaSQL()
                {
                    return cadenaSQL;
                }
            }
        }
