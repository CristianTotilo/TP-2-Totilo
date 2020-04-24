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
    public partial class frmAltaArticulo : Form
    {
        public frmAltaArticulo()
        {
            InitializeComponent();
            ttAlta.SetToolTip(txtCodigo, "Ingrese el codigo del Articulo");
            ttAlta.SetToolTip(txtDescripcion, "Ingrese una descripcion");
            ttAlta.SetToolTip(txtNombre , "Ingrese el Nombre del Articulo");
            ttAlta.SetToolTip(txtImagenUrl, "Ingresa una URL de una imagen del Articulo");
            ttAlta.SetToolTip(txtPrecio, "Ingrese un valor numerico de hasta 4 decimales despues de la coma");
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch == 44 && txtPrecio.Text.IndexOf(',') != -1)
            {
                e.Handled = true;

            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 44)
            {
                e.Handled = true;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            

            Articulo nuevo = new Articulo();
            CatalogoNegocio negocio = new CatalogoNegocio();
            try
            {

                DialogResult val = MessageBox.Show("Esta seguro que desea agregar el articulo?", "Atencion!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (val == DialogResult.Yes)
                {
                    nuevo.Codigo = txtCodigo.Text.Trim();
                    nuevo.Nombre = txtNombre.Text.Trim();
                    nuevo.Descripcion = txtDescripcion.Text.Trim();
                    nuevo.Marca = (Marca)cboMarca.SelectedItem;
                    nuevo.Categoria = (Categoria)cboCategoria.SelectedItem;
                    nuevo.ImagenURL = txtImagenUrl.Text.Trim();
                    if (txtPrecio.TextLength.Equals(0))
                    {
                        nuevo.Precio = 0;
                    }
                    else
                    {
                        nuevo.Precio = Convert.ToDecimal(txtPrecio.Text.Trim());
                    }


                    negocio.agregar(nuevo);
                }

                Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marca = new MarcaNegocio();
            CategoriaNegocio categoria = new CategoriaNegocio();
            try
            {
                cboCategoria.DataSource = categoria.listar();
                cboMarca.DataSource = marca.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
        }

      
    }
}
