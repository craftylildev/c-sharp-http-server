using System;
using System.Data;
using System.Data.SqlClient;

namespace SimpleServer
{
    public class EmployeeHandler
    {
        public EmployeeHandler()
        {
        }

        public string getEmployee(string IdEmployee)
        {
            string response = "";

            string query = @"
                SELECT	
                  e.IdEmployee,
				  e.Name,
				  e.Age,
				  h.Name
                FROM Employee e
				INNER JOIN Habitat h ON h.IdHabitat = e.IdHabitat                
                WHERE e.IdEmployee = 
				" + IdEmployee;

            const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimpleServer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand getEmployees = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = getEmployees.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            response += "<div class=\"employee employee-id-" + reader[0] + "\">";
                            response += "<h4>Employee ID: " + reader[0] + "</h4>";
                            response += "<p>Name: " + reader[1] + "<br>Age: " + reader[2] + "<br>Assigned to: " + reader[3] + "</p>";
                            response += "</div>";
                        }
                        Console.WriteLine(response);
                    }
                }
                connection.Close();
                return response;
            }
        }


        public string getAllEmployees()
        {
            string response = "";

            const string query = @"
                SELECT	
                  IdEmployee,
				  Name
                FROM Employee
                ";

            const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimpleServer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand getEmployees = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = getEmployees.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            response += "<div class=\"employee employee-id-" + reader[0] + "\">";
                            response += "<div><a href='/employees/" + reader[0] + "'> Employee ID:" + reader[0] + "<br>Employee Name:" + reader[1] + "</a></div>";
                            response += "</div><br><br>";

                        }
                        Console.WriteLine(response);
                    }
                }
                connection.Close();
                return response;
            }

        }

    }
}