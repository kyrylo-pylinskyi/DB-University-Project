using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_Projekt_Pylinskyi_Kyrylo
{
    public class DBInformer
    {

        public static string GetConectionString()
        {
            return "Data Source = DESKTOP-7QBCFRG\\SQLEXPRESS;Initial Catalog = Employee;User ID = sa; Password = 123456";
        }

        public void InsertRequest(Employee employee)
        {
            using (var connection = new SqlConnection(GetConectionString()))
            {
                employee.Podatek = employee.Brutto / 10;
                employee.Netto = employee.Brutto - employee.Podatek;
                employee.Emerytalna = employee.Podatek / 2;
                employee.Rentowa = employee.Podatek / 2;

                connection.Open();
                using (var cmd = new SqlCommand($"INSERT INTO Employees(Id, Surname, PESEL, Etat, Netto, Podatek, Emerytalna, Rentowa)" +
                    $"VALUES('{Employee.Id}','{employee.Surname}','{employee.Name}','{employee.PESEL}','{employee.Etat}','{employee.Brutto}','{employee.Netto}','{employee.Podatek}','{employee.Emerytalna}','{employee.Rentowa}')", connection))
                {
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

    }
}
