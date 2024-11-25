using Catalog.Data;
using Microsoft.EntityFrameworkCore;
using Shared.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId)
        : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);

    internal class DeleteProductHandler(CatalogDbContext catalogDb)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await catalogDb.Products.FindAsync([command.ProductId], cancellationToken: cancellationToken) ?? throw new Exception($"product did not found: {command.ProductId}");
            catalogDb.Products.Remove(product);
            await catalogDb.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}
