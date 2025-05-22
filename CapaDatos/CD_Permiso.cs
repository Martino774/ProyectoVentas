using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Permiso
    {
        public List<Permiso> Listar(int idUsuario)
        {
            //se genera una lista donde se guardan objetos de tipo usuarios
            List<Permiso> lista = new List<Permiso>();

            //conexion con la base de datos
            using (SqlConnection oconection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.IdRol,p.NobreMenu from PERIMSO p");
                    query.AppendLine("inner join ROL r on r.IdRol = p.IdRol");
                    query.AppendLine("inner join USUARIO u on u.IdRol = r.IdRol");
                    query.AppendLine("where u.IdUsuario = @idUsuario");

                   

                    //se encarga de hacer la query a la base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconection);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.CommandType = CommandType.Text;

                    oconection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            //REVISAR MAS TARDE!!!!!
                            lista.Add(new Permiso()
                            {
                                oRol = new Rol() { IdRol = Convert.ToInt32(dr["IdRol"]) },
                                NombreMenu = dr["NobreMenu"].ToString(),
                               

                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Permiso>();
                }
            }

            return lista;
        }
    }
}
