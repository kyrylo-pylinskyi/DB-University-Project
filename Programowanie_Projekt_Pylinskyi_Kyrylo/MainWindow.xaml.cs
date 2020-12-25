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

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;


namespace Programowanie_Projekt_Pylinskyi_Kyrylo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable dt = new DataTable();

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "UMbYAjvffdIsO7H0ZmWBacF8wQ83cGfkpY02s668",
            BasePath = "https://employeedb-43adb-default-rtdb.europe-west1.firebasedatabase.app/"
        };

        IFirebaseClient client;

        public MainWindow()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);

            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Columns.Add("Suname");
            dt.Columns.Add("PESEL");
            dt.Columns.Add("Etat");
            dt.Columns.Add("Brutto");
            dt.Columns.Add("Netto");
            dt.Columns.Add("Podatek");
            dt.Columns.Add("Rentowa");
            dt.Columns.Add("Emyrytalna");
        }

        private async void Input_Click(object sender, RoutedEventArgs e)
        {
            FirebaseResponse resp = await client.GetTaskAsync("Counter/node");
            Counter get = resp.ResultAs<Counter>();

            var employee = new Employee
            {
                Id = (Convert.ToInt32(get.cnt) + 1).ToString(),
                Name = nameBox.Text,
                Surname = surnameBox.Text,
                Etat = Convert.ToDouble(etatBox.Text),
                PESEL = peselBox.Text,
                Brutto = Convert.ToDouble(bruttoBox.Text),
                Netto = Convert.ToDouble(bruttoBox.Text) - (Convert.ToDouble(bruttoBox.Text) / Convert.ToDouble(etatBox.Text)),
                Podatek = Convert.ToDouble(bruttoBox.Text) / Convert.ToDouble(etatBox.Text),
                Emerytalna = Convert.ToDouble(bruttoBox.Text) / 2 * Convert.ToDouble(etatBox.Text),
                Rentowa = Convert.ToDouble(bruttoBox.Text) / 2 * Convert.ToDouble(etatBox.Text)
            };


            SetResponse response = await client.SetTaskAsync("Information/" + employee.Id, employee);
            Employee result = response.ResultAs<Employee>();

            MessageBox.Show("Inserted \nId " + result.Id + "\nSurname " + result.Surname + "\nName " + result.Name + "\nPESEL " + result.PESEL + "\nEtat " + result.Etat + "\nBrutto " + result.Brutto + "\nNetto " + result.Netto);

            var counterObject = new Counter
            {
                cnt = employee.Id
            };

            SetResponse counterResponse = await client.SetTaskAsync("Counter/node", counterObject);

            surnameBox.Text = string.Empty;
            nameBox.Text = string.Empty;
            etatBox.Text = string.Empty;
            peselBox.Text = string.Empty;
            bruttoBox.Text = string.Empty;
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            FirebaseResponse cntResp = await client.GetTaskAsync("Counter/node");
            Counter cntObj = cntResp.ResultAs<Counter>();

            int i = 0;
            int cnt = Convert.ToInt32(cntObj.cnt);

            List<Employee> employeeList = new List<Employee>();

            while (i < cnt)
            {
                i++;

                FirebaseResponse employeeResp = await client.GetTaskAsync("Information/" + i);
                Employee employeeObj = employeeResp.ResultAs<Employee>();

                employeeList.Add(new Employee()
                {
                    Id = employeeObj.Id,
                    Name = employeeObj.Name,
                    Surname = employeeObj.Surname,
                    PESEL = employeeObj.PESEL,
                    Etat = employeeObj.Etat,
                    Brutto = employeeObj.Brutto,
                    Netto = employeeObj.Netto,
                    Podatek = employeeObj.Podatek,
                    Emerytalna = employeeObj.Emerytalna,
                    Rentowa = employeeObj.Rentowa,
                });
            }

            BaseGrid.ItemsSource = employeeList;
        }


        private async void Find_Click(object sender, RoutedEventArgs e)
        {
            FirebaseResponse cntResp = await client.GetTaskAsync("Counter/node");
            Counter cntObj = cntResp.ResultAs<Counter>();

            int i = 0;
            int cnt = Convert.ToInt32(cntObj.cnt);

            List<Employee> employeeList = new List<Employee>();

            while (i < cnt)
            {
                i++;

                FirebaseResponse employeeResp = await client.GetTaskAsync("Information/" + i);
                Employee employeeObj = employeeResp.ResultAs<Employee>();

                if(employeeObj.PESEL == peselFind.Text)
                {
                    employeeList.Add(new Employee()
                    {
                        Id = employeeObj.Id,
                        Name = employeeObj.Name,
                        Surname = employeeObj.Surname,
                        PESEL = employeeObj.PESEL,
                        Etat = employeeObj.Etat,
                        Brutto = employeeObj.Brutto,
                        Netto = employeeObj.Netto,
                        Podatek = employeeObj.Podatek,
                        Emerytalna = employeeObj.Emerytalna,
                        Rentowa = employeeObj.Rentowa,
                    });
                }            
            }

            BaseGrid.ItemsSource = employeeList;
        }
    }
}
