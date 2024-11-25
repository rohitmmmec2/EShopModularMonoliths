﻿using Catalog.Data;
using Catalog.Products.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Products.Features.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category)
        : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<ProductDto> ProductDtos);
    internal class GetProductByCategoryHandler(CatalogDbContext dbContext)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await dbContext.Products
                .AsNoTracking()
                .Where(p => p.Category.Contains(query.Category))
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

           var productDtos = products.Adapt<List<ProductDto>>();
            return new GetProductByCategoryResult(productDtos);
        }
    }
}
