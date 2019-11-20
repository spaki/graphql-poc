using GraphQL.POC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQL.POC.Repositories
{
    public class FakeRepository : IRepository
    {
        private static List<Product> products;
        private static List<Category> categories;

        public static void CreateFakeData() 
        {
            var smartphones = new Category { Id = Guid.NewGuid(), Name = "Smartphones" };
            var books = new Category { Id = Guid.NewGuid(), Name = "Books" };

            var razr = new Product { Id = Guid.NewGuid(), Name = "Motorola Razr", Description = "Foldable Smartphone", Price = 1500 };
            var s10 = new Product { Id = Guid.NewGuid(), Name = "Samsung S10", Description = "Smartphone by Samsung with 64gb", Price = 1000 };
            var xiaomi = new Product { Id = Guid.NewGuid(), Name = "Xiaomi Mi 9T", Description = "Smartphone with full frontal screen", Price = 400 };
            var iphone = new Product { Id = Guid.NewGuid(), Name = "Iphone 11", Description = "Smartphone by Apple", Price = 9999 };

            var origin = new Product { Id = Guid.NewGuid(), Name = "Origin", Description = "Novel by Dan Brown", Price = 15 };
            var darktower = new Product { Id = Guid.NewGuid(), Name = "The Dark Tower III: The Waste Lands", Description = "Stephen King's epic fantasy series, The Dark Tower", Price = 20 };
            var petsematary = new Product { Id = Guid.NewGuid(), Name = "Pet Sematary", Description = "Doubleday, 1983, 1st Edition , First Printing", Price = 5 };

            
            
            smartphones.Products = new List<Product> { razr, s10, xiaomi, iphone };
            
            razr.Category = smartphones;
            s10.Category = smartphones;
            xiaomi.Category = smartphones;
            iphone.Category = smartphones;

            books.Products = new List<Product> { origin, darktower, petsematary };

            origin.Category = books;
            darktower.Category = books;
            petsematary.Category = books;



            products = new List<Product> { razr, s10, xiaomi, iphone, origin, darktower, petsematary };
            categories = new List<Category> { smartphones, books };
        }

        public IQueryable<Category> GetCategoryQuery() => categories.AsQueryable();

        public IQueryable<Product> GetProductsQuery() => products.AsQueryable();
    }
}
