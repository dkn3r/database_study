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
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

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
                    Console.WriteLine("10. Display Ads with Messages (Lazy Loading)");
                    Console.WriteLine("11. Display Ads with Messages (Eager Loading)");
                    Console.WriteLine("12. Display Ads with Messages (Explicit Loading)");
                    Console.WriteLine("13. Display Ads with Aggregation");

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
                        case "10":
                            DisplayAdWithMessagesLazy(context);
                            break;
                        case "11":
                            DisplayAdWithMessagesEager(context);
                            break;
                        case "12":
                            DisplayAdWithMessagesExplicit(context);
                            break;
                        case "13":
                            DisplayAdsWithAggregation(context);
                            break;

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
            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            Console.Write("Enter price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter user ID: ");
            int userId = int.Parse(Console.ReadLine());
            Console.Write("Enter category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            var ad = new Ad
            {
                Title = title,
                Price = price,
                UserID = userId,
                CategoryID = categoryId,
                CreatedDate = DateTime.Now
            };
            context.Ads.Add(ad);
            context.SaveChanges();

            Console.WriteLine("Ad added successfully.");
        }

        // Додавання повідомлення
        static void AddMessage(ClassifiedsContext context)
        {
            Console.Write("Enter ad ID: ");
            int adId = int.Parse(Console.ReadLine());
            Console.Write("Enter sender user ID: ");
            int senderUserId = int.Parse(Console.ReadLine());
            Console.Write("Enter receiver user ID: ");
            int receiverUserId = int.Parse(Console.ReadLine());
            Console.Write("Enter message content: ");
            string content = Console.ReadLine();

            var message = new Message
            {
                AdID = adId,
                SenderUserID = senderUserId,
                ReceiverUserID = receiverUserId,
                MessageContent = content,
                SentDate = DateTime.Now
            };
            context.Messages.Add(message);
            context.SaveChanges();

            Console.WriteLine("Message added successfully.");
        }

        // Оновлення оголошення
        static void UpdateAd(ClassifiedsContext context)
        {
            Console.Write("Enter ad ID to update: ");
            int adId = int.Parse(Console.ReadLine());
            var ad = context.Ads.Find(adId);

            if (ad != null)
            {
                Console.Write("Enter new title: ");
                ad.Title = Console.ReadLine();
                Console.Write("Enter new price: ");
                ad.Price = decimal.Parse(Console.ReadLine());
                context.SaveChanges();

                Console.WriteLine("Ad updated successfully.");
            }
            else
            {
                Console.WriteLine("Ad not found.");
            }
        }

        // Видалення оголошення
        static void DeleteAd(ClassifiedsContext context)
        {
            Console.Write("Enter ad ID to delete: ");
            int adId = int.Parse(Console.ReadLine());
            var ad = context.Ads.Find(adId);

            if (ad != null)
            {
                context.Ads.Remove(ad);
                context.SaveChanges();
                Console.WriteLine("Ad deleted successfully.");
            }
            else
            {
                Console.WriteLine("Ad not found.");
            }
        }

        // Видалення повідомлення
        static void DeleteMessage(ClassifiedsContext context)
        {
            Console.Write("Enter message ID to delete: ");
            int messageId = int.Parse(Console.ReadLine());
            var message = context.Messages.Find(messageId);

            if (message != null)
            {
                context.Messages.Remove(message);
                context.SaveChanges();
                Console.WriteLine("Message deleted successfully.");
            }
            else
            {
                Console.WriteLine("Message not found.");
            }
        }

        static void DisplayAdWithMessagesLazy(ClassifiedsContext context)
        {
            try
            {
                var ads = context.Ads.ToList();
                Console.WriteLine("\nAds with Messages (Lazy Loading):");

                foreach (var ad in ads)
                {
                    Console.WriteLine($"Ad ID: {ad.AdID}, Title: {ad.Title}");

                    // Перевіряємо чи є повідомлення
                    if (ad.Messages != null)
                    {
                        var messages = ad.Messages.ToList(); // Це викликає лінивий запит

                        foreach (var message in messages)
                        {
                            Console.WriteLine($" - Message ID: {message.MessageID}, Content: {message.MessageContent}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No messages for this ad.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Жадібне завантаження оголошень з повідомленнями
        static void DisplayAdWithMessagesEager(ClassifiedsContext context)
        {
            var ads = context.Ads.Include(ad => ad.Messages).ToList();
            Console.WriteLine("\nAds with Messages (Eager Loading):");
            foreach (var ad in ads)
            {
                Console.WriteLine($"Ad ID: {ad.AdID}, Title: {ad.Title}");
                foreach (var message in ad.Messages)
                {
                    Console.WriteLine($" - Message ID: {message.MessageID}, Content: {message.MessageContent}");
                }
            }
        }

        // Явне завантаження оголошень з повідомленнями
        static void DisplayAdWithMessagesExplicit(ClassifiedsContext context)
        {
            var ad = context.Ads.First();
            context.Entry(ad).Collection(a => a.Messages).Load();
            Console.WriteLine("\nAd with Messages (Explicit Loading):");
            Console.WriteLine($"Ad ID: {ad.AdID}, Title: {ad.Title}");
            foreach (var message in ad.Messages)
            {
                Console.WriteLine($" - Message ID: {message.MessageID}, Content: {message.MessageContent}");
            }
        }

        // Агресія з фільтрацією та сортуванням
        static void DisplayAdsWithAggregation(ClassifiedsContext context)
        {
            var ads = context.Ads
                .Where(ad => ad.Price > 100) // Фільтр
                .OrderBy(ad => ad.CreatedDate) // Сортування
                .ToList();

            Console.WriteLine("\nAds with Aggregation:");
            foreach (var ad in ads)
            {
                Console.WriteLine($"Ad ID: {ad.AdID}, Title: {ad.Title}, Price: {ad.Price:C}, Created Date: {ad.CreatedDate}");
            }
        }
    }
}
