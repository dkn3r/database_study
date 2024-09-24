using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    private static string connectionString = "Server=DESKTOP-OR45QOH;Database=ClassifiedsDB;Trusted_Connection=True;";

    static void Main(string[] args)
    {
        
        DisplayCategories();
        
        DisplayAds();
        AddAd("Laptop", "A powerful laptop.", 1000, 1, 1);
        
        DisplayAds();
        
        AddMessage(1, 1, 2, "Is the laptop available?");
        
        DisplayMessages();
    }

    static void DisplayCategories()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Categories", connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("\nCategories in the database:");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["CategoryID"]}, Name: {reader["CategoryName"]}, Description: {reader["Description"]}");
            }
            reader.Close();
        }
    }

    static void DisplayAds()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Ads", connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("\nAds in the database:");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["AdID"]}, Title: {reader["Title"]}, Price: {reader["Price"]}, UserID: {reader["UserID"]}, CategoryID: {reader["CategoryID"]}");
            }
            reader.Close();
        }
    }

    static void DisplayMessages()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Messages", connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("\nMessages in the database:");
            while (reader.Read())
            {
                Console.WriteLine($"MessageID: {reader["MessageID"]}, AdID: {reader["AdID"]}, SenderUserID: {reader["SenderUserID"]}, ReceiverUserID: {reader["ReceiverUserID"]}, Content: {reader["MessageContent"]}, SentDate: {reader["SentDate"]}");
            }
            reader.Close();
        }
    }

    static void AddAd(string title, string description, decimal price, int userId, int categoryId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Ads (Title, Description, Price, UserID, CategoryID) VALUES (@Title, @Description, @Price, @UserID, @CategoryID)", connection);
            command.Parameters.AddWithValue("@Title", title);
            command.Parameters.AddWithValue("@Description", description);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@UserID", userId);
            command.Parameters.AddWithValue("@CategoryID", categoryId);

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} ad(s) added.");
        }
    }

    static void AddMessage(int adId, int senderUserId, int receiverUserId, string messageContent)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Перевірка на існування дубліката
            SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Messages WHERE AdID = @AdID AND SenderUserID = @SenderUserID AND ReceiverUserID = @ReceiverUserID AND MessageContent = @MessageContent", connection);
            checkCommand.Parameters.AddWithValue("@AdID", adId);
            checkCommand.Parameters.AddWithValue("@SenderUserID", senderUserId);
            checkCommand.Parameters.AddWithValue("@ReceiverUserID", receiverUserId);
            checkCommand.Parameters.AddWithValue("@MessageContent", messageContent);

            int count = (int)checkCommand.ExecuteScalar();

            if (count > 0)
            {
                Console.WriteLine("This message already exists in the database.");
                return;
            }

            // Додавання нового повідомлення
            SqlCommand command = new SqlCommand("INSERT INTO Messages (AdID, SenderUserID, ReceiverUserID, MessageContent) VALUES (@AdID, @SenderUserID, @ReceiverUserID, @MessageContent)", connection);
            command.Parameters.AddWithValue("@AdID", adId);
            command.Parameters.AddWithValue("@SenderUserID", senderUserId);
            command.Parameters.AddWithValue("@ReceiverUserID", receiverUserId);
            command.Parameters.AddWithValue("@MessageContent", messageContent);

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"{rowsAffected} message(s) added.");
        }
    }

}
