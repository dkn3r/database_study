using lb1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace lb1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Зчитування конфігурацій
            var config = new ConfigurationBuilder()
            .SetBasePath(@"C:\Users\ndiac\Desktop\ТПБД\database_study2\lb1\lb1")
            .AddJsonFile("appsettings.json")
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ClassifiedsContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection")); // Заміна на вашу назву з appsettings.json

            using (var context = new ClassifiedsContext(optionsBuilder.Options))
            {
                while (true)
                {
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. Display Categories");
                    Console.WriteLine("2. Display Ads");
                    Console.WriteLine("3. Display Messages");
                    Console.WriteLine("4. Add Ad");
                    Console.WriteLine("5. Add Message");
                    Console.WriteLine("6. Exit");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            DisplayCategories(context);
                            break;
                        case "2":
                            DisplayAds(context);
                            break;
                        case "3":
                            DisplayMessages(context);
                            break;
                        case "4":
                            AddAd(context);
                            break;
                        case "5":
                            AddMessage(context);
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
        }

        static void DisplayCategories(ClassifiedsContext context)
        {
            var categories = context.Categories.ToList();
            Console.WriteLine("\nCategories in the database:");
            foreach (var category in categories)
            {
                Console.WriteLine($"ID: {category.CategoryID}, Name: {category.CategoryName}, Description: {category.Description}");
            }
        }

        static void DisplayAds(ClassifiedsContext context)
        {
            var ads = context.Ads.ToList();
            Console.WriteLine("\nAds in the database:");
            foreach (var ad in ads)
            {
                Console.WriteLine($"ID: {ad.AdID}, Title: {ad.Title}, Price: {ad.Price:C}, UserID: {ad.UserID}, CategoryID: {ad.CategoryID}");
            }
        }

        static void DisplayMessages(ClassifiedsContext context)
        {
            var messages = context.Messages.ToList();
            Console.WriteLine("\nMessages in the database:");
            foreach (var message in messages)
            {
                Console.WriteLine($"MessageID: {message.MessageID}, AdID: {message.AdID}, SenderUserID: {message.SenderUserID}, ReceiverUserID: {message.ReceiverUserID}, Content: {message.MessageContent}, SentDate: {message.SentDate}");
            }
        }

        static void AddAd(ClassifiedsContext context)
        {
            try
            {
                Console.WriteLine("Enter ad title:");
                string title = Console.ReadLine();

                Console.WriteLine("Enter ad description:");
                string description = Console.ReadLine();

                Console.WriteLine("Enter ad price:");
                decimal price;
                while (!decimal.TryParse(Console.ReadLine(), out price))
                {
                    Console.WriteLine("Invalid price. Please enter a valid decimal value.");
                }

                Console.WriteLine("Enter user ID:");
                int userId;
                while (!int.TryParse(Console.ReadLine(), out userId))
                {
                    Console.WriteLine("Invalid User ID. Please enter a valid integer.");
                }

                Console.WriteLine("Enter category ID:");
                int categoryId;
                while (!int.TryParse(Console.ReadLine(), out categoryId))
                {
                    Console.WriteLine("Invalid Category ID. Please enter a valid integer.");
                }

                var ad = new Ad
                {
                    Title = title,
                    Description = description,
                    Price = price,
                    UserID = userId,
                    CategoryID = categoryId,
                    CreatedDate = DateTime.Now
                };

                context.Ads.Add(ad);
                context.SaveChanges();
                Console.WriteLine("Ad added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the ad: {ex.Message}");
            }
        }

        static void AddMessage(ClassifiedsContext context)
        {
            try
            {
                Console.WriteLine("Enter ad ID:");
                int adId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter sender user ID:");
                int senderUserId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter receiver user ID:");
                int receiverUserId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter message content:");
                string messageContent = Console.ReadLine();

                var message = new Message
                {
                    AdID = adId,
                    SenderUserID = senderUserId,
                    ReceiverUserID = receiverUserId,
                    MessageContent = messageContent,
                    SentDate = DateTime.Now
                };

                context.Messages.Add(message);
                context.SaveChanges();
                Console.WriteLine("Message added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the message: {ex.Message}");
            }
        }
    }
}
