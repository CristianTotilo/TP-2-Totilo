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
        private List<Articulo> lista;
        public Catalogo()
        {
            InitializeComponent();
           
        }

        private void Catalogo_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            CatalogoNegocio Negocio = new CatalogoNegocio();
            lista = Negocio.listar();
            try
            {
                dgvArticulo.DataSource = lista;
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
                picArt.ImageLocation = art.ImagenURL;
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
            catch(System.ArgumentException)
            {
                MessageBox.Show("No se pudo visualizar la imagen del Articulo debido a un error de sintaxis en la URL asignada.");
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
            cargarDatos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo modificar;

            modificar = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            frmAltaArticulo frmmodificar = new frmAltaArticulo(modificar);
            frmmodificar.ShowDialog();
            cargarDatos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            CatalogoNegocio Negocio = new CatalogoNegocio();
            try
            {
                DialogResult val = MessageBox.Show("Esta seguro que desea eliminar el Articulo seleccionado?", "Atencion!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (val == DialogResult.Yes)
                {
                    int Id = ((Articulo)dgvArticulo.CurrentRow.DataBoundItem).ID;
                    Negocio.Eliminar(Id);
                    cargarDatos();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            try
            {
                if (txtBusqueda.Text == "")
                {
                    listaFiltrada = lista;
                }
                else
                {
                    listaFiltrada = lista.FindAll(slot => slot.Codigo.ToLower().Contains(txtBusqueda.Text.ToLower())|| slot.Nombre.ToLower().Contains(txtBusqueda.Text.ToLower())|| slot.Descripcion.ToLower().Contains(txtBusqueda.Text.ToLower()) || slot.Marca.Descripcion.ToLower().Contains(txtBusqueda.Text.ToLower()) || slot.Categoria.Descripcion.ToLower().Contains(txtBusqueda.Text.ToLower()) || slot.Precio.ToString().ToLower().Contains(txtBusqueda.Text.ToLower()));
                   
                }
                    dgvArticulo.DataSource = listaFiltrada;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString());
            }
        }

   
    }
}
