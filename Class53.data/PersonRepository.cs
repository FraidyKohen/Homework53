using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class53.data
{
    public class PersonRepository
    {
        private string _connectionString;
        public PersonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetPeople()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM PeopleTable";
            List<Person> allPeople = new List<Person>();
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                allPeople.Add(new Person
                {
                    Id = (int)reader["id"],
                    FirstName = (string)reader["firstName"],
                    LastName = (string)reader["lastName"],
                    Age = (int)reader["age"]
                });
            }
            return allPeople;
        }

        public void AddPerson(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO PeopleTable VALUES (@firstName, @lastName, @age)";
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public Person GetPersonById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM PeopleTable WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Person
                {
                    Id = (int)reader["id"],
                    FirstName = (string)reader["firstName"],
                    LastName = (string)reader["lastName"],
                    Age = (int)reader["age"]
                };
            }
            return null;
        }

        public void DeletePersonById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM PeopleTable WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void EditPerson(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE PeopleTable SET FirstName = @first, LastName = @last, Age = @age WHERE id = @id";
            command.Parameters.AddWithValue("@id", person.Id);
            command.Parameters.AddWithValue("@first", person.FirstName);
            command.Parameters.AddWithValue("@last", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
        }
    }
}
