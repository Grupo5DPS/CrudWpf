using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data.MySqlClient;

namespace CrudWpf
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string cad = "server=localhost;database=bdbiblioteca;Uid=root;pwd=;port=3306";
        MySqlConnection connection;
        MySqlCommand command;
        string query = "";
        public MainWindow()
        {
            InitializeComponent();
            listar();
        }
        public void listar()
        {
            try
            {
                query = "SELECT * FROM tautor WHERE 1";
                connection = new MySqlConnection(cad);
                command = new MySqlCommand(query);
                command.Connection = connection;

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgvAutor.ItemsSource = table.DefaultView;
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                query = @"INSERT INTO tautor(CodAutor, Apellidos, Nombres, Nacionalidad) VALUES (@CodAutor, @Apellidos, @Nombres, @Nacionalidad)";
                connection = new MySqlConnection(cad);
                command = new MySqlCommand(query);
                command.Connection = connection;

                command.Parameters.AddWithValue("@CodAutor", txtCodAutor.Text);
                command.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
                command.Parameters.AddWithValue("@Nombres", txtNombres.Text);
                command.Parameters.AddWithValue("@Nacionalidad", txtNacionalidad.Text);
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgvAutor.ItemsSource = table.DefaultView;
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

        private void btnActualizar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                query = @"UPDATE tautor SET Apellidos=@Apellidos, Nombres=@Nombres, Nacionalidad=@Nacionalidad WHERE CodAutor=@CodAutor";
                connection = new MySqlConnection(cad);
                command = new MySqlCommand(query);
                command.Connection = connection;

                command.Parameters.AddWithValue("@CodAutor", txtCodAutor.Text);
                command.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
                command.Parameters.AddWithValue("@Nombres", txtNombres.Text);
                command.Parameters.AddWithValue("@Nacionalidad", txtNacionalidad.Text);
                connection.Open();
                command.ExecuteNonQuery();
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

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                query = @"DELETE from tautor WHERE CodAutor=@CodAutor";
                connection = new MySqlConnection(cad);
                command = new MySqlCommand(query);
                command.Connection = connection;
                command.Parameters.AddWithValue("@CodAutor", txtCodAutor.Text);
                connection.Open();
                command.ExecuteNonQuery();
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

        private void txtCodAutor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            
        }

        private void EventoLibro(object sender, RoutedEventArgs e)
        {
            Libro window = new Libro();
            window.Show(); // Returns immediately
            this.Close();
        }

        private void EventoPrestamo(object sender, RoutedEventArgs e)
        {
            Prestamo window = new Prestamo();
            window.Show(); // Returns immediately
            this.Close();
        }
    }
    }

