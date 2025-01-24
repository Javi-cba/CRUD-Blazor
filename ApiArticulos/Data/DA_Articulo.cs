using ApiArticulos.Models;
using Microsoft.Data.SqlClient;

namespace ApiArticulos.Data
{
    public class DA_Articulo
    {
        public static List<MArticulo> GetArticulos()
        {
            ConexionBD conexion = new ConexionBD();
            var con = new SqlConnection(conexion.GetCadenaSQL());

            try
            {
                con.Open();
                List<MArticulo> listaArticulos = new List<MArticulo>();

                var cmd = new SqlCommand($"SELECT art_codnum,art_descri,art_codbarra,CAST(art_precUnit AS DECIMAL(10, 2))as art_precUnit " +
                    $"FROM Articulo WHERE art_vigencia=1 ORDER BY art_codnum DESC",con);
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        while (dr.Read())
                        {
                            MArticulo MArticulo = new MArticulo()
                            {
                                art_codnum = Convert.ToInt32(dr["art_codnum"]),
                                art_descri = dr["art_descri"].ToString().ToUpper(),
                                art_codbarra = dr["art_codbarra"].ToString(),
                               art_precUnit = Convert.ToDecimal(dr["art_precUnit"]),

                            };
                            listaArticulos.Add(MArticulo);
                        }
                    }
                return listaArticulos;

            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"SQL Exc. {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Catch Exc. {ex.Message}", ex);
            }
            finally 
            { 
                con.Close();
            }

        }

        public static MArticulo GetArticuloPorId(int codnum)
        {
            ConexionBD conexion = new ConexionBD();
            var con = new SqlConnection(conexion.GetCadenaSQL());

            try
            {
                con.Open();
                MArticulo MArticulo = null;

                var cmd = new SqlCommand($"SELECT art_codnum, art_descri, art_codbarra, CAST(art_precUnit AS DECIMAL(10, 2)) AS art_precUnit " +
                    $"FROM Articulo WHERE art_vigencia=1 AND art_codnum={codnum}", con);
                var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    MArticulo = new MArticulo()
                    {
                        art_codnum = Convert.ToInt32(dr["art_codnum"]),
                        art_descri = dr["art_descri"].ToString().ToUpper(),
                        art_codbarra = dr["art_codbarra"].ToString(),
                        art_precUnit = Convert.ToDecimal(dr["art_precUnit"]),
                    };
                }
       
                return MArticulo;

            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"SQL Exc. {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Catch Exc. {ex.Message}", ex);
            }
            finally
            {
                con.Close();
            }

        }


        public static void AggArt(MArticulo obj)
        {
            ConexionBD conexion = new ConexionBD();
            var con = new SqlConnection(conexion.GetCadenaSQL());
            try
            {
                con.Open();

                string precio = obj.art_precUnit.ToString().Replace(",", ".");
                SqlCommand cmd = new SqlCommand($"INSERT INTO Articulo (art_codnum,art_descri,art_precUnit,art_codbarra,art_vigencia) " +
                    $"VALUES((SELECT MAX(art_codnum+1) FROM Articulo),'{obj.art_descri}',{precio},'{obj.art_codbarra}',1);", con);
                cmd.ExecuteScalar();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"SQL Exc. {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Catch Exc. {ex.Message}", ex);
            }
            finally
            {
                con.Close();
            }          
        }

        public static void ModArt(MArticulo obj)
        {
            ConexionBD conexion = new ConexionBD();
            var con = new SqlConnection(conexion.GetCadenaSQL());
            try
            {
                con.Open();
                string precio = obj.art_precUnit.ToString().Replace(",", ".");
                SqlCommand cmd = new SqlCommand($"UPDATE Articulo SET art_descri= '{obj.art_descri}',art_precUnit={precio},art_codbarra= '{obj.art_codbarra}' " +
                    $" WHERE art_codnum={obj.art_codnum};", con);
                cmd.ExecuteScalar();

            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"SQL Exc. {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Catch Exc. {ex.Message}", ex);
            }
            finally
            {
                con.Close();
            }

        }

        public static void DeshabilitarArt(int art_codnum)
        {
            ConexionBD conexion = new ConexionBD();
            var con = new SqlConnection(conexion.GetCadenaSQL());
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE Articulo SET art_vigencia = 0 " +
                    $"WHERE art_codnum={art_codnum};", con);
                cmd.ExecuteScalar();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"SQL Exc. {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Catch Exc. {ex.Message}", ex);
            }
            finally
            {
                con.Close();
            }          
        }


    }
}
