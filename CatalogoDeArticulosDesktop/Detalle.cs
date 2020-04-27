using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace CatalogoDeArticulosDesktop
{
    
    public partial class Detalle : Form
    {
        private Articulo articulo;
        public Detalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void Detalle_Load(object sender, EventArgs e)
        {
            try
            {
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtMarca.Text = articulo.Marca.Descripcion;
                txtCategoria.Text = articulo.Categoria.Descripcion;
                txtImagenUrl.Text = articulo.ImagenURL;
                txtPrecio.Text = articulo.Precio.ToString();

                picArticulo.ImageLocation = articulo.ImagenURL;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

   
    }
}
