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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;
namespace CrudWpf
{
    /// <summary>
    /// Lógica de interacción para Prestamo.xaml
    /// </summary>
    public partial class Prestamo : Window
    {
        string cad = "server=localhost;database=bdbiblioteca;Uid=root;pwd=;port=3306";
        MySqlConnection connection;
        MySqlCommand command;
        string query = "";

        public Prestamo()
        {
            InitializeComponent();
            listar();
        }
        public void listar()
        {
            try
            {
                query = "SELECT tautor.Nombres,tautor.Apellidos,tautor.Nacionalidad,tlibro.Titulo,tlibro.Editorial,FechaPrestamo FROM tprestamo INNER JOIN tautor ON tprestamo.CodAutor=tautor.CodAutor INNER JOIN tlibro ON tprestamo.CodLibro=tlibro.CodLibro";
                connection = new MySqlConnection(cad);
                command = new MySqlCommand(query);
                command.Connection = connection;

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgPrestamo.ItemsSource = table.DefaultView;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
       

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                query = @"INSERT INTO tprestamo(CodAutor, CodLibro,FechaPrestamo) VALUES (@CodAutor, @CodLibro,@FechaPrestamo)";
                connection = new MySqlConnection(cad);
                command = new MySqlCommand(query);
                command.Connection = connection;

                //obteniedo fecha
                //string fecha = DateTime.Now.ToString();
                //MessageBox.Show(hora);

                command.Parameters.AddWithValue("@CodAutor", txtAutor.Text);
                command.Parameters.AddWithValue("@CodLibro", txtLibro.Text);
                command.Parameters.AddWithValue("@FechaPrestamo", DateTime.Now);
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgPrestamo.ItemsSource = table.DefaultView;
                listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show(); // Returns immediately
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Libro window = new Libro();
            window.Show(); // Returns immediately
            this.Close();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
           
            
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                query = @"DELETE from tprestamo WHERE CodLibro=@CodLibro and CodAutor=@CodAutor"; 
                 connection = new MySqlConnection(cad);
                command = new MySqlCommand(query);
                command.Connection = connection;
                command.Parameters.AddWithValue("@CodAutor", txtAutor.Text);
                command.Parameters.AddWithValue("@CodLibro", txtLibro.Text);
                connection.Open();
                command.ExecuteNonQuery();
                listar();
                MessageBox.Show("Eliminado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
