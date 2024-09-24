using System;
using System.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=.;Database=lb1;Integrated Security=True;";

        DisplayBooks(connectionString);

        AddBook(connectionString, "New Book", 1, 9.99m, 1);

        ExecuteJoinQuery(connectionString);
        ExecuteFilteredQuery(connectionString);
        ExecuteAggregateQuery(connectionString);
    }

    static void DisplayBooks(string connectionString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Books";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("Books in the store:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Title"]}, Price: {reader["Price"]}");
            }
            reader.Close();
        }
    }

    static void AddBook(string connectionString, string title, int authorId, decimal price, int genreId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Books (Title, AuthorID, Price, GenreID) VALUES (@Title, @AuthorID, @Price, @GenreID)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@AuthorID", authorId);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@GenreID", genreId);
            command.ExecuteNonQuery();

            Console.WriteLine($"Book '{title}' added to the store.");
        }
    }

    static void ExecuteJoinQuery(string connectionString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT Books.Title, Authors.FirstName, Authors.LastName " +
                           "FROM Books " +
                           "JOIN Authors ON Books.AuthorID = Authors.AuthorID";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("\nBooks with Authors:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Title"]} by {reader["FirstName"]} {reader["LastName"]}");
            }
            reader.Close();
        }
    }

    static void ExecuteFilteredQuery(string connectionString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Books WHERE Price < 15.00";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("\nBooks under $15:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Title"]}, Price: {reader["Price"]}");
            }
            reader.Close();
        }
    }

    static void ExecuteAggregateQuery(string connectionString)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT COUNT(*) AS TotalBooks FROM Books";
            SqlCommand command = new SqlCommand(query, connection);
            int totalBooks = (int)command.ExecuteScalar();

            Console.WriteLine($"\nTotal number of books: {totalBooks}");
        }
    }
}
