﻿using Catalog.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Data.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data.Seed
{
    public class CatalogDataSeeder(CatalogDbContext dbContext) : IDataSeeder
    {
        public async Task SeedAllAsync()
        {
            if(!await dbContext.Products.AnyAsync())
            {
                await dbContext.Products.AddRangeAsync(InitialData.Products);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    public static class InitialData
    {
        public static IEnumerable<Product> Products =>
        [
            Product.Create(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), "IPhone X", ["category1"], "Long description", "imagefile", 500),
        Product.Create(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), "Samsung 10", ["category1"], "Long description", "imagefile", 400),
        Product.Create(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"), "Huawei Plus", ["category2"], "Long description", "imagefile", 650),
        Product.Create(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"), "Xiaomi Mi", ["category2"], "Long description", "imagefile", 450)
        ];
    }
}
