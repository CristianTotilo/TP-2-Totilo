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
    public partial class Catalogo : Form
    {
        public Catalogo()
        {
            InitializeComponent();
        }

        private void Catalogo_Load(object sender, EventArgs e)
        {
            cargarTabla();

        }

        private void cargarTabla()
        {
            try
            {
                CatalogoNegocio Negocio = new CatalogoNegocio();
                dgvArticulo.DataSource = Negocio.listar();
                dgvArticulo.Columns[0].Visible = false;
                dgvArticulo.Columns[6].Visible = false;
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
        private void dgvArticulo_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Articulo art;
                art = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                picArt.Load(art.ImagenURL);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                MessageBox.Show("No hay una URL de imagen asignada a este Articulo.");
                
            }

            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("No se pudo visualizar la imagen del Articulo debido a un error de sintaxis en la URL asignada.");
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("No se pudo establecer una conexion segura con la URL de la imagen del articulo. \n\n Ayuda  \n->Intente con una URL diferente a la actual.");
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }
           
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();
            cargarTabla();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo modificar;

            modificar = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            frmAltaArticulo frmmodificar = new frmAltaArticulo(modificar);
            frmmodificar.ShowDialog();
        }
    }
}
