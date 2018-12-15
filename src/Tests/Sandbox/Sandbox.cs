using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Foody.Data;
using Foody.Data.Models;
using Foody.Data.Models.Nutrition;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox
{
    public static class Sandbox
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"{typeof(Sandbox).Namespace} ({string.Join(" ", args)}) starts working...");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;
                SandboxCode(serviceProvider);
            }
        }

        private static void SandboxCode(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<FoodyDbContext>();

            var potatoes = new FoodItem
            {
                Name = "Potatoes",
                Description = "Potato, Solanum tuberosum, is an herbaceous perennial plant in the family Solanaceae which is grown for its edible tubers. The potato plant has a branched stem and alternately arranged leaves consisting of leaflets which are both of unequal size and shape.",
            };

            potatoes.FoodItemMacroElements.Add(new FoodItemMacroElement
            {
                AmountInGrams = 21.2,
                FoodItem = potatoes,
                MacroElement = context.MacroElements.First(f => f.Name == "Carbohydrates")
            });

            potatoes.FoodItemMacroElements.Add(new FoodItemMacroElement
            {
                AmountInGrams = 2.5,
                FoodItem = potatoes,
                MacroElement = context.MacroElements.First(f => f.Name == "Proteins")
            });

            potatoes.FoodItemMacroElements.Add(new FoodItemMacroElement
            {
                AmountInGrams = 0.1,
                FoodItem = potatoes,
                MacroElement = context.MacroElements.First(f => f.Name == "Fats")
            });

            potatoes.FoodItemMicroElements.Add(new FoodItemMicroElement
            {
                AmountInMilligrams = 10,
                FoodItem = potatoes,
                MicroElement = context.MicroElements.First(f => f.Name == "Vitamin A")
            });

            potatoes.FoodItemMicroElements.Add(new FoodItemMicroElement
            {
                AmountInMilligrams = 9.6,
                FoodItem = potatoes,
                MicroElement = context.MicroElements.First(f => f.Name == "Vitamin C")
            });

            //potatoes.FoodItemMicroElements.Add(new FoodItemMicroElement
            //{
            //    AmountInMilligrams = 29.9,
            //    FoodItem = potatoes,
            //    MicroElement = context.MicroElements.First(f => f.Name == "Vitamin D")
            //});

            //potatoes.FoodItemMicroElements.Add(new FoodItemMicroElement
            //{
            //    AmountInMilligrams = 0.1,
            //    FoodItem = potatoes,
            //    MicroElement = context.MicroElements.First(f => f.Name == "Vitamin E")
            //});

            potatoes.FoodItemMicroElements.Add(new FoodItemMicroElement
            {
                AmountInMilligrams = 0.002,
                FoodItem = potatoes,
                MicroElement = context.MicroElements.First(f => f.Name == "Vitamin K")
            });

            potatoes.FoodItemMicroElements.Add(new FoodItemMicroElement
            {
                AmountInMilligrams = 0.1,
                FoodItem = potatoes,
                MicroElement = context.MicroElements.First(f => f.Name == "Vitamin B1")
            });

            //potatoes.FoodItemMicroElements.Add(new FoodItemMicroElement
            //{
            //    AmountInMilligrams = 0.1,
            //    FoodItem = potatoes,
            //    MicroElement = context.MicroElements.First(f => f.Name == "Vitamin B2")
            //});

            potatoes.FoodItemMicroElements.Add(new FoodItemMicroElement
            {
                AmountInMilligrams = 1.4,
                FoodItem = potatoes,
                MicroElement = context.MicroElements.First(f => f.Name == "Vitamin B3")
            });

            potatoes.FoodItemMicroElements.Add(new FoodItemMicroElement
            {
                AmountInMilligrams = 0.4,
                FoodItem = potatoes,
                MicroElement = context.MicroElements.First(f => f.Name == "Vitamin B5")
            });

            potatoes.FoodItemMicroElements.Add(new FoodItemMicroElement
            {
                AmountInMilligrams = 0.9,
                FoodItem = potatoes,
                MicroElement = context.MicroElements.First(f => f.Name == "Vitamin B6")
            });

            potatoes.FoodItemMicroElements.Add(new FoodItemMicroElement
            {
                AmountInMilligrams = 29.9,
                FoodItem = potatoes,
                MicroElement = context.MicroElements.First(f => f.Name == "Vitamin A")
            });

            potatoes.FoodItemMicroElements.Add(new FoodItemMicroElement
            {
                AmountInMilligrams = 29.9,
                FoodItem = potatoes,
                MicroElement = context.MicroElements.First(f => f.Name == "Vitamin A")
            });

            potatoes.FoodItemMicroElements.Add(new FoodItemMicroElement
            {
                AmountInMilligrams = 29.9,
                FoodItem = potatoes,
                MicroElement = context.MicroElements.First(f => f.Name == "Vitamin A")
            });

            context.SaveChanges();


            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddDbContext<FoodyDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
