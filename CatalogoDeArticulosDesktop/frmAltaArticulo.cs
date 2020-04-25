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
        private Articulo articulo = null;
        public frmAltaArticulo()
        {
            InitializeComponent();
            ttAlta.SetToolTip(txtCodigo, "Ingrese el codigo del Articulo");
            ttAlta.SetToolTip(txtDescripcion, "Ingrese una descripcion");
            ttAlta.SetToolTip(txtNombre , "Ingrese el Nombre del Articulo");
            ttAlta.SetToolTip(txtImagenUrl, "Ingresa una URL de una imagen del Articulo");
            ttAlta.SetToolTip(txtPrecio, "Ingrese un valor numerico de hasta 4 decimales despues de la coma");
        }

        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            ttAlta.SetToolTip(txtCodigo, "Ingrese el codigo del Articulo");
            ttAlta.SetToolTip(txtDescripcion, "Ingrese una descripcion");
            ttAlta.SetToolTip(txtNombre, "Ingrese el Nombre del Articulo");
            ttAlta.SetToolTip(txtImagenUrl, "Ingresa una URL de una imagen del Articulo");
            ttAlta.SetToolTip(txtPrecio, "Ingrese un valor numerico de hasta 4 decimales despues de la coma");
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marca = new MarcaNegocio();
            CategoriaNegocio categoria = new CategoriaNegocio();
            try
            {

                cboCategoria.DataSource = categoria.listar();
                cboCategoria.ValueMember = "Descripcion"; //mi value member es descripcion porque en el listar de articulo me traigo la descripcion de categoria y no el ID
                //cboCategoria.DisplayMember = "Descripcion";
                cboCategoria.SelectedIndex = -1;

                cboMarca.DataSource = marca.listar();
                cboMarca.ValueMember = "Descripcion";//mi value member es descripcion porque en el listar de articulo me traigo la descripcion de Marca y no el ID
                //cboMarca.DisplayMember = "Descripcion";
                cboMarca.SelectedIndex = -1;

                if ( articulo != null)
                {
                    Text = "Modificar Articulo";
                    lblTitulo.Text = "Formulario de Modificacion";
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtImagenUrl.Text = articulo.ImagenURL;
                    txtPrecio.Text = Convert.ToString(articulo.Precio);
                    if (articulo.Categoria != null)
                        cboMarca.SelectedValue = articulo.Marca.Descripcion;
                    if (articulo.Categoria != null)
                        cboCategoria.SelectedValue = articulo.Categoria.Descripcion;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
            
            CatalogoNegocio CatalogoNegocio = new CatalogoNegocio();

            try
            {

                DialogResult val = MessageBox.Show("Esta seguro que desea continuar?", "Atencion!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (val == DialogResult.Yes)
                {

                    if (articulo == null)
                        articulo = new Articulo();

                    articulo.Codigo = txtCodigo.Text.Trim();
                    articulo.Nombre = txtNombre.Text.Trim();
                    articulo.Descripcion = txtDescripcion.Text.Trim();
                    articulo.Marca = (Marca)cboMarca.SelectedItem;
                    articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                    articulo.ImagenURL = txtImagenUrl.Text.Trim();

                    if (txtPrecio.TextLength.Equals(0))
                            articulo.Precio = 0;
                    else
                            articulo.Precio = Convert.ToDecimal(txtPrecio.Text.Trim());

                if (articulo.ID != 0)
                    CatalogoNegocio.modificar(articulo);
                else
                    CatalogoNegocio.agregar(articulo);
                }

                Dispose();
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
