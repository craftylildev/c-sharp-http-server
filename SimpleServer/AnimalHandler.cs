using System;
using System.Data;
using System.Data.SqlClient;

namespace SimpleServer
{
	public class AnimalHandler
	{
		public AnimalHandler ()
		{
		}

		public string getAnimal(string IdAnimal) {
			string response = "";

			string query = @"
				SELECT 
				  a.IdAnimal,
				  a.Name, 
				  h.Name HabitatName,
				  ht.Name HabitatType,
				  s.CommonName,
				  s.ScientificName
				FROM Animal a
				INNER JOIN Species s ON a.IdSpecies = s.IdSpecies
				INNER JOIN Habitat h ON h.IdHabitat = a.IdHabitat
				INNER JOIN HabitatType ht on ht.IdHabitatType = h.IdHabitatType
				WHERE a.IdAnimal = 
				" + IdAnimal;

			const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimpleServer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand getAnimals = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = getAnimals.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            response += "<div class=\"animal animal-id-" + reader[0] + "\">";
                            response += "<h2>" + reader[1] + "</h2>";
                            response += "<div>" + reader[5] + "</div>";
                            response += "<div>Lives in the " + reader[2] + " (" + reader[3] + " type) habitat</div>";
                            response += "</div>";
                        }
                        Console.WriteLine(response);
                    }
                }
            connection.Close();
            return response;
            }
		}
        
		public string getAllAnimals() {
			string response = "";

			const string query = @"
				SELECT 
				  a.IdAnimal,
				  a.Name, 
				  h.Name HabitatName,
				  ht.Name HabitatType,
				  s.CommonName,
				  s.ScientificName
				FROM Animal a
				INNER JOIN Species s ON a.IdSpecies = s.IdSpecies
				INNER JOIN Habitat h ON h.IdHabitat = a.IdHabitat
				INNER JOIN HabitatType ht on ht.IdHabitatType = h.IdHabitatType
				";

            const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SimpleServer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand getAnimals = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = getAnimals.ExecuteReader())
                {
                    // Check is the reader has any rows at all before starting to read.
                    if (reader.HasRows)
                    {
                        // Read advances to the next row.
                        while (reader.Read())
                        {
                            response += "<div class=\"animal animal-id-" + reader[0] + "\">";
                            response += "<h2>" + reader[1] + "</h2>";
                            response += "<div><a href='/animals/" + reader[0] + "'>" + reader[5] + "</a></div>";
                            response += "<div>Lives in the " + reader[2] + " (" + reader[3] + " type) habitat</div>";
                            response += "</div>";
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


