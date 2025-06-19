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
    public class CD_Rol
    {
        public List<Rol> Listar()
        {
            //se genera una lista donde se guardan objetos de tipo usuarios
            List<Rol> lista = new List<Rol>();

            //conexion con la base de datos
            using (SqlConnection oconection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdRol,Descripcion from ROL ");
                    



                    //se encarga de hacer la query a la base de datos
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconection);
                    cmd.CommandType = CommandType.Text;

                    oconection.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            //REVISAR MAS TARDE!!!!!
                            lista.Add(new Rol()
                            {
                                IdRol = Convert.ToInt32(dr["IdRol"]),
                                Descripcion = dr["Descripcion"].ToString()

                            }) ;
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Rol>();
                }
            }

            return lista;
        }
    }
}
