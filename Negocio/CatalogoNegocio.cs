using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;


namespace Negocio
{
    public class CatalogoNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> Listado = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "data source=User-PC\\SQLEXPRESS; initial catalog=CATALOGO_DB; integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select A.Id,A.Codigo,A.Nombre,A.Descripcion,M.Descripcion[Marca],C.Descripcion[Categoria],A.ImagenUrl,A.Precio from ARTICULOS as A left join CATEGORIAS as C on A.IdCategoria = C.Id inner join MARCAS as M on A.IdMarca = M.Id";
                comando.Connection = conexion;
                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Articulo Articulo = new Articulo();
                    Marca marca = new Marca();
                    Categoria categoria = new Categoria();
                    Articulo.Marca = marca;
                    Articulo.Categoria = categoria;

                    
                    Articulo.ID = lector.GetInt32(0);

                    if (!DBNull.Value.Equals(lector["Codigo"])) //Tuve que hacer esta validacion porque la de !convert.IsDBNull no funciono
                        Articulo.Codigo = lector.GetString(1);
                    else
                        Articulo.Codigo = "N/A";

                    if (!DBNull.Value.Equals(lector["Nombre"]))
                        Articulo.Nombre = lector.GetString(2);//aux.Nombre = (string)lector["Nombre"]; alternativa
                    else
                        Articulo.Nombre = "N/A";

                    if (!DBNull.Value.Equals(lector["Descripcion"]))
                        Articulo.Descripcion = lector.GetString(3);
                    else
                        Articulo.Descripcion = "N/A";

                    if (!DBNull.Value.Equals(lector["Marca"]))
                        Articulo.Marca.Descripcion = lector.GetString(4); //lector["Descripcion"].ToString(); alternativa 
                    else
                        Articulo.Marca.Descripcion = "N/A";

                    if (!DBNull.Value.Equals(lector["Categoria"]))
                        Articulo.Categoria.Descripcion = lector.GetString(5);
                    else
                        Articulo.Categoria.Descripcion = "N/A";

                    if (!DBNull.Value.Equals(lector["ImagenUrl"]))
                        Articulo.ImagenURL = lector.GetString(6);
                    else
                        Articulo.ImagenURL = "N/A";

                    if (!DBNull.Value.Equals(lector["Precio"]))
                        Articulo.Precio = (decimal)lector.GetDecimal(7);
                    else
                        Articulo.Precio = 0;
                       
                    Listado.Add(Articulo);

                }

                    return Listado;
                }
          
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        public void agregar(Articulo nuevo)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            try
            {
                conexion.ConnectionString = "data source=User-PC\\SQLEXPRESS; initial catalog=CATALOGO_DB; integrated security=sspi";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "insert into ARTICULOS (Codigo, Nombre, Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio) Values (@Codigo,@Nombre,@Descripcion,@Marca,@Categoria,@ImagenUrl,@Precio)";
                // comando.Parameters.Clear();
                if (nuevo.Codigo == "" )
                    nuevo.Codigo = "N/A";
                comando.Parameters.AddWithValue("@Codigo", nuevo.Codigo);

                if (nuevo.Nombre == "")
                    nuevo.Nombre = "N/A";
                comando.Parameters.AddWithValue("@Nombre", nuevo.Nombre);

                if (nuevo.Descripcion == "")
                    nuevo.Descripcion = "N/A";
                comando.Parameters.AddWithValue("@Descripcion", nuevo.Descripcion);

                if(nuevo.Marca != null)
                comando.Parameters.AddWithValue("@Marca", nuevo.Marca.ID.ToString());
                else
                    comando.Parameters.AddWithValue("@Marca", "1");

                if (nuevo.Categoria != null)
                    comando.Parameters.AddWithValue("@Categoria", nuevo.Categoria.ID.ToString());
                else
                    comando.Parameters.AddWithValue("@Categoria", "1");

                if (nuevo.ImagenURL=="")
                {
                    nuevo.ImagenURL = "N/A";
                }
                comando.Parameters.AddWithValue("@ImagenUrl", nuevo.ImagenURL);

                comando.Parameters.AddWithValue("@Precio", nuevo.Precio);

                comando.Connection = conexion;

                conexion.Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearQuery("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion ,IdMarca = @Marca,IdCategoria = @Categoria,ImagenUrl = @ImagenUrl,Precio= @Precio where Id = @Id");
                datos.agregarParametro("@Id",articulo.ID);
                datos.agregarParametro("@Codigo",articulo.Codigo);
                datos.agregarParametro("@Nombre",articulo.Nombre);
                datos.agregarParametro("@Descripcion",articulo.Descripcion);
                datos.agregarParametro("@Marca",articulo.Marca.ID.ToString());
                datos.agregarParametro("@Categoria",articulo.Categoria.ID.ToString());
                datos.agregarParametro("@ImagenUrl",articulo.ImagenURL);
                datos.agregarParametro("@Precio",articulo.Precio.ToString());
                datos.ejecutarAccion(); 

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
