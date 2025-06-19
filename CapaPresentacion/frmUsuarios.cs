using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion.Utilidades;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }


        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            comboBoxEstado.Items.Add(new OpcionCombo() {Valor = 1, Texto = "Activo" });
            comboBoxEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            comboBoxEstado.DisplayMember = "Texto";
            comboBoxEstado.ValueMember = "Valor";
            comboBoxEstado.SelectedIndex = 0;


            List<Rol> listaRol = new CN_Rol().Listar();

            foreach (Rol item in listaRol)
            {
                comboBoxRol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            }
            comboBoxRol.DisplayMember = "Texto";
            comboBoxRol.ValueMember = "Valor";
            comboBoxRol.SelectedIndex = 0;




            //Recorre el datagridview
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible && columna.Name != "btnSeleccionar")
                {
                    comboBoxBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            comboBoxBusqueda.DisplayMember = "Texto";
            comboBoxBusqueda.ValueMember = "Valor";
            comboBoxBusqueda.SelectedIndex = 0;


            //Mostrar todos los usuarios
            List<Usuario> listaUsuario = new CN_Usuario().Listar();

            foreach (Usuario item in listaUsuario)
            {
                dgvData.Rows.Add(new object[] {"", item.IdUsuario, item.Documento, item.NombreCompleto, item.Correo, item.Clave,
                item.oRol.IdRol,
                item.oRol.Descripcion,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo":"No Activo"
                });
            }
            


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            Usuario objUsuario = new Usuario() {
                IdUsuario = Convert.ToInt32(txtId.Text),
                Documento = textDocumento.Text,
                NombreCompleto = textNombreCompleto.Text,
                Correo = textCorreo.Text,
                Clave = textClave.Text,
                oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)comboBoxRol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)comboBoxRol.SelectedItem).Valor) == 1 ? true : false,
            };

            //si el idUsuario va a registrar el usuario
            if(objUsuario.IdUsuario == 0)
            {
                int idUsuarioGenerado = new CN_Usuario().Registrar(objUsuario, out mensaje);

                if (idUsuarioGenerado != 0)
                {
                    dgvData.Rows.Add(new object[] {"", idUsuarioGenerado, textDocumento.Text, textNombreCompleto.Text, textCorreo.Text, textClave.Text,
                    ((OpcionCombo)comboBoxRol.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)comboBoxRol.SelectedItem).Texto.ToString(),
                    ((OpcionCombo)comboBoxEstado.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)comboBoxEstado.SelectedItem).Texto.ToString(),

                    });

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else //sino se ejecuta el metodo registrar de la clase CN_Usuario que edita en vez de crear un nuevo usuario
            {
                bool resultado = new CN_Usuario().Editar(objUsuario, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["Documento"].Value = textDocumento.Text;
                    row.Cells["NombreCompleto"].Value = textNombreCompleto.Text;
                    row.Cells["Correo"].Value = textCorreo.Text;
                    row.Cells["IdRol"].Value = ((OpcionCombo)comboBoxEstado.SelectedItem).Texto.ToString();
                    row.Cells["Rol"].Value = ((OpcionCombo)comboBoxRol.SelectedItem).Texto.ToString();
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)comboBoxEstado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)comboBoxEstado.SelectedItem).Texto.ToString();

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }




            
        }

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            textDocumento.Text = "";
            textNombreCompleto.Text = "";
            textCorreo.Text = "";
            textClave.Text = "";
            textConfrimarClave.Text = "";
            comboBoxRol.SelectedIndex = 0;
            comboBoxEstado.SelectedIndex = 0;

            //al terminar de limpiar las casillas de texto y combo se queda seleccionada la casilla de documento
            textDocumento.Select();
        }




        //evento para poner la imagen check20 en la fila 0
        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                return;
            }

            if(e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var w = Properties.Resources.check20.Width;
                var h = Properties.Resources.check20.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        //evento para compartir los datos del dgv a los campos de al lado
        /*private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    textDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    textNombreCompleto.Text = dgvData.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    textCorreo.Text = dgvData.Rows[indice].Cells["Correo"].Value.ToString();
                    textClave.Text = dgvData.Rows[indice].Cells["Clave"].Value.ToString();
                    textConfrimarClave.Text = dgvData.Rows[indice].Cells["Clave"].Value.ToString();


                    //recorre las opciones dentro del combobox Rol
                    foreach (OpcionCombo oc in comboBoxRol.Items)
                    {
                        
                        if(Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indiceCombo = comboBoxRol.Items.IndexOf(oc);
                            comboBoxRol.SelectedIndex = indiceCombo;
                            break;
                        }
                    }

                    //recorre las opciones dentro del combobox Estado
                    foreach (OpcionCombo oc in comboBoxEstado.Items)
                    {
                        
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indiceCombo = comboBoxEstado.Items.IndexOf(oc);
                            comboBoxEstado.SelectedIndex = indiceCombo;
                            break;
                        }
                    }
                }
            }


        }*/
        //evento para compartir los datos del dgv a los campos de al lado
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value?.ToString();
                    textDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value?.ToString();
                    textNombreCompleto.Text = dgvData.Rows[indice].Cells["NombreCompleto"].Value?.ToString();
                    textCorreo.Text = dgvData.Rows[indice].Cells["Correo"].Value?.ToString();
                    textClave.Text = dgvData.Rows[indice].Cells["Clave"].Value?.ToString();
                    textConfrimarClave.Text = dgvData.Rows[indice].Cells["Clave"].Value?.ToString();

                    // Intentar parsear el valor de IdRol
                    string idRolStr = dgvData.Rows[indice].Cells["IdRol"].Value?.ToString();
                    if (int.TryParse(idRolStr, out int idRol))
                    {
                        foreach (OpcionCombo oc in comboBoxRol.Items)
                        {
                            if (Convert.ToInt32(oc.Valor) == idRol)
                            {
                                comboBoxRol.SelectedIndex = comboBoxRol.Items.IndexOf(oc);
                                break;
                            }
                        }
                    }

                    // Intentar parsear el valor de EstadoValor
                    string estadoValorStr = dgvData.Rows[indice].Cells["EstadoValor"].Value?.ToString();
                    if (int.TryParse(estadoValorStr, out int estadoValor))
                    {
                        foreach (OpcionCombo oc in comboBoxEstado.Items)
                        {
                            if (Convert.ToInt32(oc.Valor) == estadoValor)
                            {
                                comboBoxEstado.SelectedIndex = comboBoxEstado.Items.IndexOf(oc);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(txtId.Text) != 0)
            {
                if(MessageBox.Show("Desea Eliminar el Usuario?","Mensaje",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    Usuario objUsuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtId.Text)
                    };

                    bool respuesta = new CN_Usuario().Eliminar(objUsuario, out mensaje);

                    if(respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)comboBoxBusqueda.SelectedItem).Valor.ToString();

            if(dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))                   
                        row.Visible = true;
                    else
                        row.Visible=false;
                    
                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
        }
    }
}
