using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04___DesAEA
{
    public partial class Persona : Form
    {
        SqlConnection conn;
        public Persona(SqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
        }

        private void Persona_Load(object sender, EventArgs e)
        {

        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    String sql = "SELECT * FROM People";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader1 = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader1);
                    dgvListado.DataSource = dt;
                    dgvListado.Refresh();
                }
                else
                {
                    MessageBox.Show("La conexión está cerrada.");
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("No se pueden leer usuarios: \n" +
                                 exception1.ToString());
            }
}

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //MÉTODO ANTERIOR

            //if (conn.State == ConnectionState.Open)
            //{
            //    String FirstName = txtNombre.Text;

            //    SqlCommand cmd = new SqlCommand();
            //    cmd.CommandText = "BuscarPersonaNombre";
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Connection = conn;

            //    SqlParameter param = new SqlParameter();
            //    param.ParameterName = "@FirstName";
            //    param.SqlDbType = SqlDbType.NVarChar;
            //    param.Value = FirstName;

            //    cmd.Parameters.Add(param);

            //    SqlDataReader reader2 = cmd.ExecuteReader();

            //    DataTable dt = new DataTable();
            //    dt.Load(reader2);
            //    dgvListado.DataSource = dt;
            //    dgvListado.Refresh();

            //}
            //else
            //{
            //    MessageBox.Show("La conexión está cerrada.");
            //}

            //-----------------------------------------------------------------------------//

            //MÉTODO SIN DATATABLE

            List<Personas> personas = new List<Personas>();

            SqlCommand command = new SqlCommand("BuscarPersonaNombre", conn);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter param1 = new SqlParameter();
            //SqlDbType Varchar 
            param1.Value = txtNombre.Text.Trim();
            param1.ParameterName = "@FirstName";
            param1.SqlDbType = SqlDbType.VarChar;

            command.Parameters.Add(param1);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                personas.Add(new Personas
                {
                    PersonId = Convert.ToInt32(reader["PersonID"]),
                    FirstName = Convert.ToString(reader["FirstName"]),
                    LastName = Convert.ToString(reader["LastName"]),
                    HireDate = Convert.ToString(reader["HireDate"]),
                    EnrollmentDate = Convert.ToString(reader["EnrollmentDate"]),
                }
                );
            }

            dgvListado.DataSource = personas;
        }
    }
}
