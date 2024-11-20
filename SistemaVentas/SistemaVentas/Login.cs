using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaVentas
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            //inicializa el formulario de inicio
            Inicio form = new Inicio();
            form.Show();

            //oculta el formulario de login
            this.Hide();

            //cuando se cierra el form de inicio se vuelve a abrir el de login
            form.FormClosing += frm_closing;
        }

        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
            txtContra.Clear();
            txtDocumento.Clear();
        }
    }
}
