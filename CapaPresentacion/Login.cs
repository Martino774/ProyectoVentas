using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidad;

namespace CapaPresentacion
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
            List<Usuario> TEST = new CN_Usuario().Listar();
            //MessageBox.Show("Cantidad de usuarios encontrados: " + TEST.Count);

            Usuario oUsuario = new CN_Usuario().Listar().Where(u => u.Documento == txtDocumento.Text && u.Clave == txtContra.Text).FirstOrDefault();


            //valida si encuentra el usuario
            if(oUsuario != null)
            {
                //inicializa el formulario de inicio
                Inicio form = new Inicio(oUsuario);
                form.Show();

                //oculta el formulario de login
                this.Hide();

                //cuando se cierra el form de inicio se vuelve a abrir el de login
                form.FormClosing += frm_closing;
            }
            else
            {
                //si no encuentra el usuario muestra el mensaje 
                MessageBox.Show("No se encontro el usuario","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

            
        }

        //metodo para que cuando se cierre el fomulario inicio, vuelva a abrise el formilario login
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
            txtContra.Clear();
            txtDocumento.Clear();
        }
    }
}