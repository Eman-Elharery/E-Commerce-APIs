using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CompanySystem.BLL
{
    public static class BLLServicesExtention
    {
        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IImageManager, ImageManager>();
            services.AddScoped<IValidator<ProductCreateDTO>, ProductCreateDtoValidator>();
            services.AddScoped<IValidator<CategoryCreateDTO>, CategoryCreateDtoValidator>();
            services.AddScoped<IValidator<ProductEditDTO>, ProductEditDtoValidator>();
            services.AddScoped<IValidator<CategoryEditDTO>, CategoryEditDtoValidator>();
            services.AddScoped<IValidator<ImageUploadDto>, ImageUploadDtoValidator>();
            services.AddScoped<IErrorMapper, ErrorMapper>();

        }
    }
}
