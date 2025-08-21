using Market.Infrastructure.DataBase;
using Market.Infrastructure.Extantions;
using MarketApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Market application APIs", Version = "v1" }));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.EnsureCreated();

    var productCategory = new ProductCategory
    {
        Id = Guid.NewGuid(),
        Name = "Дорувори"
    };
    var measurement = new Measurement
    {
        Id = Guid.NewGuid(),
        Name = "Кг"
    };
    var organizationType = new OrganizationType
    {
        Id = Guid.NewGuid(),
        Name = "OOO"
    };
    var address = new Address
    {
        Id = Guid.NewGuid(),
        Name = "г. Худжанд"
    };
    var product = new Product
    {
        Id = Guid.NewGuid(),
        Capacity = 1,
        Name = "Хекплан",
        ProductCategoryId = productCategory.Id,
        MeasurementId = measurement.Id
    };

    var customer = new Customer
    {
        Id = Guid.NewGuid(),
        AddressId = address.Id,
        Name = "ИП Амир",
        PhoneNumber = "1253654"
    };

    var organization = new Organization
    {
        Id = Guid.NewGuid(),
        AddressId = address.Id,
        Name = "Сомон Агро",
        OrganizationTypeId = organizationType.Id,
        PhoneNumber = "12546658"
    };

    var purchase = new Purchase
    {
        Id = Guid.NewGuid(),
        ProductId = product.Id,
        OrganizationId = organization.Id,
        Price = 100,
        PriceUSD = 10,
        Quantity = 1,
    };
    dbContext.Addresses.Add(address);
    
    dbContext.OrganizationType.Add(organizationType);
   // dbContext.SaveChanges();
    dbContext.ProductCategories.Add(productCategory);
    //dbContext.SaveChanges();
    dbContext.Measurements.Add(measurement);
    //dbContext.SaveChanges();
    dbContext.Products.Add(product);
    //dbContext.SaveChanges();
    dbContext.Customers.Add(customer);
    //dbContext.SaveChanges();
    dbContext.Organizations.Add(organization);
   // dbContext.SaveChanges();
    dbContext.Purchases.Add(purchase);
    dbContext.SaveChanges();
}
    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    //{
    app.UseSwagger();
    app.UseSwaggerUI(op => op.EnableTryItOutByDefault());
//}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
