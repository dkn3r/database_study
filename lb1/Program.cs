using lb1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace lb1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Зчитування конфігурацій
            var config = new ConfigurationBuilder()
            .SetBasePath(@"C:\Users\ndiac\Desktop\ТПБД\database_study2\lb2\lb1")
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
                    Console.WriteLine("6. Update Ad");
                    Console.WriteLine("7. Delete Ad");
                    Console.WriteLine("8. Delete Message");
                    Console.WriteLine("9. Exit");

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
                            UpdateAd(context);
                            break;
                        case "7":
                            DeleteAd(context);
                            break;
                        case "8":
                            DeleteMessage(context);
                            break;
                        case "9":
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
        }

        // Відображення категорій
        static void DisplayCategories(ClassifiedsContext context)
        {
            var categories = context.Categories.ToList();
            Console.WriteLine("\nCategories in the database:");
            foreach (var category in categories)
            {
                Console.WriteLine($"ID: {category.CategoryID}, Name: {category.CategoryName}, Description: {category.Description}");
            }
        }

        // Відображення оголошень
        static void DisplayAds(ClassifiedsContext context)
        {
            var ads = context.Ads.ToList();
            Console.WriteLine("\nAds in the database:");
            foreach (var ad in ads)
            {
                Console.WriteLine($"ID: {ad.AdID}, Title: {ad.Title}, Price: {ad.Price:C}, UserID: {ad.UserID}, CategoryID: {ad.CategoryID}");
            }
        }

        // Відображення повідомлень
        static void DisplayMessages(ClassifiedsContext context)
        {
            var messages = context.Messages.ToList();
            Console.WriteLine("\nMessages in the database:");
            foreach (var message in messages)
            {
                Console.WriteLine($"MessageID: {message.MessageID}, AdID: {message.AdID}, SenderUserID: {message.SenderUserID}, ReceiverUserID: {message.ReceiverUserID}, Content: {message.MessageContent}, SentDate: {message.SentDate}");
            }
        }

        // Додавання оголошення
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
                DisplayAds(context); // Відображення оновленого списку
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the ad: {ex.Message}");
            }
        }

        // Додавання повідомлення
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
                DisplayMessages(context); // Відображення оновленого списку
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the message: {ex.Message}");
            }
        }

        // Оновлення оголошення
        static void UpdateAd(ClassifiedsContext context)
        {
            try
            {
                Console.WriteLine("Enter the ID of the ad you want to update:");
                int adId = Convert.ToInt32(Console.ReadLine());
                var ad = context.Ads.Find(adId);

                if (ad != null)
                {
                    Console.WriteLine("Enter new title (leave empty to keep current):");
                    string newTitle = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newTitle))
                    {
                        ad.Title = newTitle;
                    }

                    Console.WriteLine("Enter new description (leave empty to keep current):");
                    string newDescription = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newDescription))
                    {
                        ad.Description = newDescription;
                    }

                    Console.WriteLine("Enter new price (leave empty to keep current):");
                    string priceInput = Console.ReadLine();
                    if (decimal.TryParse(priceInput, out decimal newPrice))
                    {
                        ad.Price = newPrice;
                    }

                    context.SaveChanges();
                    Console.WriteLine("Ad updated successfully.");
                    DisplayAds(context); // Відображення оновленого списку
                }
                else
                {
                    Console.WriteLine("Ad not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the ad: {ex.Message}");
            }
        }

        // Видалення оголошення
        static void DeleteAd(ClassifiedsContext context)
        {
            try
            {
                Console.WriteLine("Enter the ID of the ad you want to delete:");
                int adId = Convert.ToInt32(Console.ReadLine());
                var ad = context.Ads.Find(adId);

                if (ad != null)
                {
                    context.Ads.Remove(ad);
                    context.SaveChanges();
                    Console.WriteLine("Ad deleted successfully.");
                    DisplayAds(context); // Відображення оновленого списку
                }
                else
                {
                    Console.WriteLine("Ad not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the ad: {ex.Message}");
            }
        }

        // Видалення повідомлення
        static void DeleteMessage(ClassifiedsContext context)
        {
            try
            {
                Console.WriteLine("Enter the ID of the message you want to delete:");
                int messageId = Convert.ToInt32(Console.ReadLine());
                var message = context.Messages.Find(messageId);

                if (message != null)
                {
                    context.Messages.Remove(message);
                    context.SaveChanges();
                    Console.WriteLine("Message deleted successfully.");
                    DisplayMessages(context); // Відображення оновленого списку
                }
                else
                {
                    Console.WriteLine("Message not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the message: {ex.Message}");
            }
        }
    }
}
