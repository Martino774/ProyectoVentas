using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using FontAwesome.Sharp;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        //objeto de tipo usuario para recibir el objeto que traemos de la DB en el form Login
        private static Usuario usuarioActual;

        private static IconMenuItem menuActivo = null;

        private static Form FormularioActivo = null;    

        //Se pide como parametro en el constructor
        public Inicio(Usuario objUsuario)
        {
            //Se almacena en el obj creado anteriormente
            usuarioActual = objUsuario;

            InitializeComponent();
        }

        //Dar doble clic al formulario para obtener este metodo
        private void Inicio_Load(object sender, EventArgs e)
        {
            List<Permiso> ListaPermisos = new CN_Permiso().Listar(usuarioActual.IdUsuario);

            foreach (IconMenuItem iconMenu in menu.Items)
            {
                bool encontrado = ListaPermisos.Any(m => m.NombreMenu == iconMenu.Name);

                if (encontrado == false) {
                    iconMenu.Visible = false;
                }
            }

            //muestra en el lbl el nombre del usuario que se logueo
            lblUsuario.Text = usuarioActual.NombreCompleto;
        }



        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.White;
            }

            menu.BackColor = Color.Silver;
            menuActivo = menu;

            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }

            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.SteelBlue;

            contenedor.Controls.Add(formulario);
            formulario.Show();

        }

        private void menuUsuario_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmUsuarios());
        }

        private void submenuCategoria_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menuMantenedor), new frmCategoria());
        }

        private void submenuProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menuMantenedor), new frmProducto());
        }

        private void submenuRegistrarVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menuVentas), new frmVentas());
        }

        private void submenuVerDetalleVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menuVentas), new frmDetalleVenta());
        }

        private void submenuRegistrarCompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menuCompras), new frmCompras());
        }

        private void submenuVerDetalleCompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario((menuCompras), new frmDetalleCompra());
        }

        private void menuClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmClientes());
        }

        private void menuProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProveedores());
        }

        private void menuReportes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmReportes());
        }
    }
}
