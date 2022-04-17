using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data;
using System.Data.SqlClient;

namespace WPFSemana04_DesAEA
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection(@"data source=DESKTOP-87FCA55\SQLEXPRESS;initial catalog = lab04martes_db; Integrated Security = True;");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Person> people = new List<Person>();

            connection.Open();
            SqlCommand command = new SqlCommand("BuscarPersonaNombre", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.SqlDbType = SqlDbType.VarChar;
            parameter1.Size = 50;

            parameter1.Value = "";
            parameter1.ParameterName = "@FirstName";

            command.Parameters.Add(parameter1);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                people.Add(new Person
                {
                    PersonId = dataReader[0].ToString(),
                    LastName = dataReader[1].ToString(),
                    FirstName = dataReader[2].ToString(),
                    HireDate = dataReader[3].ToString(),
                    EnrollmentDate = dataReader[4].ToString(),
                });

            }
            connection.Close();
            dgvPeople.ItemsSource = people;
        }

    }
}
