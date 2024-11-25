using Catalog.Products.Events;
using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Products.Models
{
    public class Product : Aggregate<Guid>
    {
        public string Name { get; private set; } = default!;
        public List<string> Category { get; private set; } = [];
        public string Description { get; private set; } = default!;
        public string ImageFile { get; private set; } = default!;
        public decimal Price { get; private set; }

        public static Product Create(Guid id, string name, List<string> category, string description, string imageFile, decimal price)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
            var product =  new Product
            {
                Id= id,
                Name= name,
                Category= category,
                Description= description,
                ImageFile= imageFile,
                Price= price
            };

            product.AddDomianEvent(new ProductCreatedEvent(product));
            return product;
        }

        public void Update(string name, List<string> category, string description, string imageFile, decimal price)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
            Name = name;
            Category = category;
            Description = description;
            ImageFile = imageFile;
            Price = price;
            // if product price changed, raise ProductPriceChanged domain event
            if(Price != price)
            {
                AddDomianEvent(new ProductPriceChangedEvent(this));
            }
        }

    }
}
