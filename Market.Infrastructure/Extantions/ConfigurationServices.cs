using FluentValidation;
using Market.Application.Authentication;
using Market.Application.DTOs.Address;
using Market.Application.DTOs.CurrencyExchange;
using Market.Application.DTOs.Customer;
using Market.Application.DTOs.Measurement;
using Market.Application.DTOs.Organization;
using Market.Application.DTOs.OrganizationType;
using Market.Application.DTOs.Product;
using Market.Application.DTOs.ProductCategory;
using Market.Application.DTOs.Purchase;
using Market.Application.DTOs.ReturnCustomer;
using Market.Application.DTOs.ReturnOrganization;
using Market.Application.DTOs.Role;
using Market.Application.DTOs.Sale;
using Market.Application.DTOs.User;
using Market.Application.Interfacies;
using Market.Application.Services;
using Market.Infrastructure.DataBase;
using Market.Infrastructure.Mappers;
using Market.Infrastructure.Repositories;
using Market.Mappers;
using MarketApi.FluentValidation;
using MarketApi.Infrastructure.Interfacies;
using MarketApi.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMq;
using Swashbuckle.AspNetCore.SwaggerGen;



namespace Market.Infrastructure.Extantions
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddAuthToken();

            services.AddDbContext<ApplicationDbContext>(op => op.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                    b => b.MigrationsAssembly("Market.Api")));

            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IMeasurementRepository, MeasurementRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IOrganizationTypeRepository, OrganizationTypeRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ICurrencyExchangeRepository, CurrencyExchangeRepository>();
            services.AddScoped<IMarketRopository, MarketRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IReturnCustomerRepository, ReturnCustomerRepository>();
            services.AddScoped<IReturnOrganizationRepository, ReturnOrganizationRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IGenericService<ReturnCustomerRequest, ReturnCustomerUpdateRequest, ReturnCustomerResponse>, ReturnCustomerService>();
            services.AddScoped<IGenericService<ReturnOrganizationRequest, ReturnOrganizationUpdateRequest, ReturnOrganizationResponse>, ReturnOrganizationService>();
            services.AddScoped<IGenericService<ProductRequest, ProductUpdateRequest, ProductResponse>, ProductServise>();
            services.AddScoped<IGenericService<MeasurementRequest, MeasurementUpdateRequest, MeasurementResponse>, MeasurementService>();
            services.AddScoped<IGenericService<ProductCategoryRequest, ProductCategoryUpdateRequest, ProductCategoryResponse>, ProductCategoryService>();
            services.AddScoped<IGenericService<CustomerRequest, CustomerUpdateRequest, CustomerResponse>, CustomerService>();
            services.AddScoped<IGenericService<AddressRequest, AddressUpdateRequest, AddressResponse>, AddressService>();
            services.AddScoped<IGenericService<OrganizationRequest, OrganizationUpdateRequest, OrganizationResponse>, OrganizationService>();
            services.AddScoped<IGenericService<OrganizationTypeRequest, OrganizationTypeUpdateRequest, OrganizationTypeResponse>, OrganizationTypeService>();
            services.AddScoped<IGenericService<PurchaseRequest, PurchaseUpdateRequest, PurchaseResponse>, PurchaseService>();
            services.AddScoped<IGenericService<SaleRequest, SaleUpdateRequest, SaleResponse>, SaleService>();
            services.AddScoped<IGenericService<RoleRequest, RoleUpdateRequest, RoleResponse>, RoleService>();
            services.AddScoped<IGenericService<UserRequest, UserUpdateRequest, UserResponse>, UserService>();
            services.AddScoped<IGenericService<CurrencyExchangeRequest, CurrencyExchangeUpdateRequest, CurrencyExchangeResponse>, CurrencyExchangeService>();
            services.AddScoped<MarketService>();
            services.AddScoped<AuthService>();
            services.AddScoped<CurrencyExchangeService>();

            services.AddSingleton<IRabbitMqService, RabbitMqService>();

            services.AddAutoMapper(op =>
            {
                op.AddMaps(typeof(ProductProfile).Assembly);
                op.AddMaps(typeof(OrganizationProfile).Assembly);
                op.AddMaps(typeof(OrganizationTypeProfile).Assembly);
                op.AddMaps(typeof(ProductCategoryProfile).Assembly);
                op.AddMaps(typeof(MeasurementProfile).Assembly);
                op.AddMaps(typeof(AddressProfile).Assembly);
                op.AddMaps(typeof(CustomerProfile).Assembly);
                op.AddMaps(typeof(PuschaseProfile).Assembly);
                op.AddMaps(typeof(SaleProfile).Assembly);
                op.AddMaps(typeof(ReturnCustomerProfile).Assembly);
                op.AddMaps(typeof(ReturnOrganizationProfile).Assembly);
                op.AddMaps(typeof(CurrencyExchangeProfile).Assembly);
                op.AddMaps(typeof(MarketProfile).Assembly);
                op.AddMaps(typeof(AuthProfile).Assembly);
                op.AddMaps(typeof(RoleProfile).Assembly);
                op.AddMaps(typeof(UserProfile).Assembly);
            });
            services.AddValidatorsFromAssemblyContaining<PurchaseRequestValidator>();

            return services;
        }
    }
}
