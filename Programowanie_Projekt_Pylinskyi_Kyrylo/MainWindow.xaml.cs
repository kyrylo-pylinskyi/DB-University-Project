using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace Programowanie_Projekt_Pylinskyi_Kyrylo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Input_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.Surname = surnameBox.Text;
            employee.Name = nameBox.Text;
            employee.PESEL = peselBox.Text;
            employee.Etat = etatBox.Text;
            employee.Brutto = bruttoBox.Text;
            DBInformer insert = new DBInformer();
            insert.InsertRequest(employee);

            surnameBox.Text = string.Empty;
            nameBox.Text = string.Empty;
            etatBox.Text = string.Empty;
            peselBox.Text = string.Empty;
            bruttoBox.Text = string.Empty;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection con = new SqlConnection();
            con.ConnectionString = DBInformer.GetConectionString();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM [Employees]";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Employees");
            da.Fill(dt);

            BaseGrid.ItemsSource = dt.DefaultView;
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = DBInformer.GetConectionString();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = $"SELECT * FROM [Employees] WHERE PESEL = '{peselFind.Text}'";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Employees");
            da.Fill(dt);

            BaseGrid.ItemsSource = dt.DefaultView;
        }
    }
}
