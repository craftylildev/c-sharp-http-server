using SimpleServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SimpleServer
{
    public class HabitatHandler
    {
        public HabitatHandler()
        {
        }

        public List<Animal> newAnimalList = new List<Animal>();
        public List<Employee> newEmployeeList = new List<Employee>();

        public List<Animal> populateAnimalList(string IdHabitat)
        {
            string queryAnimals = @"SELECT
                  a.IdHabitat,
                  a.Name,
                  s.CommonName
                FROM Animal a
                INNER JOIN Species s ON s.IdSpecies = a.IdSpecies 
                WHERE a.IdHabitat = " + IdHabitat;
            
            using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = SimpleServer; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            using (SqlCommand getHabitatAnimals = new SqlCommand(queryAnimals, connection))
            {
                connection.Open();
                using (SqlDataReader reader = getHabitatAnimals.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            Animal a = new Animal(reader[0] as int? ?? 0, reader[1] as string, reader[2] as string);
                            newAnimalList.Add(a);
                        }
                    }
                }
            }
            return newAnimalList;
        }

        public List<Employee> populateEmployeeList(string IdHabitat)
        {
            string queryEmployees = @"SELECT IdEmployee, Name, IdHabitat FROM Employee WHERE IdHabitat =" + IdHabitat ;
            using (SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = SimpleServer; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            using (SqlCommand getHabitatEmployees = new SqlCommand(queryEmployees, connection))
            {
                connection.Open();
                using (SqlDataReader reader = getHabitatEmployees.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            Employee e = new Employee(reader[0] as int? ?? 0, reader[1] as string, reader[2] as int? ?? 0);
                            newEmployeeList.Add(e);
                        }
                    }
                }
            }
            return newEmployeeList;
        }


        public string getHabitat(string IdHabitat)
        {
            string response = "<h3>Animal(s) & Employee(s)</h3>";

            var newAnimalList = populateAnimalList(IdHabitat);
            foreach (Animal a in newAnimalList)
            {
                response += "<p>" + a.Name + " the " + a.CommonName + "</p>";
            }

            var newEmployeeList = populateEmployeeList(IdHabitat);
            foreach (Employee e in newEmployeeList)
            {
                response += "<p>Employee Name: " + e.Name + "<br> ID: " + e.IdEmployee + "</p>";  
            }

            return response;
        }
        
        public string getAllHabitats()
        {
            string response = "";

            const string query = @"
                SELECT	
                  h.IdHabitat,
                  h.Name AS HabitatName,
                  COUNT(h.Name) AS NumOfAnimals
                FROM Habitat h
                INNER JOIN Animal a ON a.IdHabitat = h.IdHabitat
                GROUP BY h.Name, h.IdHabitat
                ";

            const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimpleServer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand getHabitats = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = getHabitats.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            response += "<div class=\"habitat habitat-id-" + reader[0] + "\">";
                            response += "<div><a href='/habitats/" + reader[0] + "'>" + reader[2] + " animal(s) in the " + reader[1] + "</a></div>";
                            response += "</div><br>";

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