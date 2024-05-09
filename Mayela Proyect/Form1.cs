using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mayela_Proyect
{
    public partial class Form1 : Form
    {

        List<Persona> personas = new List<Persona>();
        string filePath = "personas.dat"; // Nombre del archivo para almacenar los datos

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Cargar los datos desde el archivo al iniciar la aplicación
            CargarDatos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                errorProvider1.SetError(txtID, "Debe ingresar tu ID");
                txtID.Focus();
                return;
            }
            errorProvider1.SetError(txtID, "");

            if (Existe(txtID.Text))
            {
                errorProvider1.SetError(txtID, "ID ya ha sido utilizado");
                txtID.Focus();
                return;
            }
            errorProvider1.SetError(txtID, "");

            if (txtNombre.Text == "")
            {
                errorProvider1.SetError(txtNombre, "Debe ingresar su Nombre");
                txtNombre.Focus();
                return;
            }
            errorProvider1.SetError(txtNombre, "");

            if (txtApellido.Text == "")
            {
                errorProvider1.SetError(txtApellido, "Debe ingresar su Apellido");
                txtApellido.Focus();
                return;
            }
            errorProvider1.SetError(txtApellido, "");

            Persona mipersona = new Persona();
            mipersona.Id = txtID.Text;
            mipersona.Nombres = txtNombre.Text;
            mipersona.Apellidos = txtApellido.Text;
            mipersona.FechaNcimiento = dtpFechaNacimiento.Value;
            mipersona.FechaRegistro = DateTime.Today; // Fecha de registro actual

            personas.Add(mipersona);

            dgvDatos.DataSource = null;
            dgvDatos.DataSource = personas;

            txtID.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtID.Focus();

            // Guardar los datos después de agregar una persona
            GuardarDatos();
        }

        private bool Existe(string Id)
        {
            foreach (Persona persona in personas)
            {
                if (persona.Id == Id) return true;
            }
            return false;
        }

        private void GuardarDatos()
        {
            try
            {
                // Crear un FileStream para guardar los datos en el archivo
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // Crear un formateador binario
                    BinaryFormatter formatter = new BinaryFormatter();
                    // Serializar y escribir la lista de personas en el archivo
                    formatter.Serialize(fileStream, personas);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos: " + ex.Message);
            }
        }

        private void CargarDatos()
        {
            try
            {
                // Verificar si el archivo existe antes de intentar cargar los datos
                if (File.Exists(filePath))
                {
                    // Crear un FileStream para leer los datos del archivo
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        // Crear un formateador binario
                        BinaryFormatter formatter = new BinaryFormatter();
                        // Deserializar los datos y cargar la lista de personas
                        personas = (List<Persona>)formatter.Deserialize(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }

            // Asignar la lista de personas al DataSource del DataGridView
            dgvDatos.DataSource = personas;
        }
    }

   
   
}


