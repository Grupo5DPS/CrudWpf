using System;
using System.Data;
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

namespace CrudWpf
{
    /// <summary>
    /// Lógica de interacción para Libro.xaml
    /// </summary>
    public partial class Libro : Window
    {
        string cad = "server=localhost;database=bdbiblioteca;Uid=root;pwd=;port=3306";
        MySqlConnection connection;
        MySqlCommand command;
        string query = "";
        public Libro()
        {
            InitializeComponent();
            listar();
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {

        }
        public void listar()
        {
            try
            {
                query = "SELECT * FROM tlibro";
                connection = new MySqlConnection(cad);
                command = new MySqlCommand(query);
                command.Connection = connection;

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgLibro.ItemsSource = table.DefaultView;


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

        private void Button_Click(object sender, RoutedEventArgs e)
        {//evento que agregar libros
            try
            {
                query = @"INSERT INTO tlibro(codLibro, Titulo, Editorial) VALUES (@codLibro, @Titulo, @Editorial)";
                connection          = new MySqlConnection(cad);
                command             = new MySqlCommand(query);
                command.Connection  = connection;

                command.Parameters.AddWithValue("@codLibro" , txtCodigo.Text);
                command.Parameters.AddWithValue("@Titulo"   , txtTitulo.Text);
                command.Parameters.AddWithValue("@Editorial", txtEditorial.Text);
                connection.Open();
                MySqlDataAdapter adapter    = new MySqlDataAdapter(command);
                DataTable table             = new DataTable();
                adapter.Fill(table);
                dgLibro.ItemsSource         = table.DefaultView;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //metodo que actualiza libro
            //MessageBox.Show("Actilizar");
            try
            {
                query = @"UPDATE tLibro SET Titulo=@Titulo, Editorial=@Editorial WHERE codLibro=@codLibro";
                connection = new MySqlConnection(cad);
                command = new MySqlCommand(query);
                command.Connection = connection;

                command.Parameters.AddWithValue("@codLibro" , txtCodigo.Text);
                command.Parameters.AddWithValue("@Titulo"   , txtTitulo.Text);
                command.Parameters.AddWithValue("@Editorial", txtEditorial.Text);
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //metodo que Elimina Libro
            try
            {
                query = @"DELETE from tLibro WHERE codLibro=@codLibro";
                connection = new MySqlConnection(cad);
                command = new MySqlCommand(query);
                command.Connection = connection;
                command.Parameters.AddWithValue("@codLibro", txtCodigo.Text);
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {//alterna ventana  para Autor
            MainWindow window = new MainWindow();
            window.Show(); // Returns immediately
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Prestamo window = new Prestamo();
            window.Show(); // Returns immediately
            this.Close();
        }
    }
}
