using Catalog;
using Basket;
using Ordering;
namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // add services to container

            builder.Services
                .AddCatalogModule(builder.Configuration)
                .AddBasketModule(builder.Configuration)
                .AddOrderingModule(builder.Configuration);

            var app = builder.Build();

            // configure teh HTTP request pipeline
            app
                .UseCatalogModule()
                .UseBasketModule()
                .UseOrderingModule();
            app.Run();
        }
    }
}
