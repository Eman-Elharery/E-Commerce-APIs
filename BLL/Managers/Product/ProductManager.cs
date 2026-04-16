using CompanySystem.Common;
using CompanySystem.DAL;
using FluentValidation;

namespace CompanySystem.BLL
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ProductCreateDTO> _validator;
        private readonly IValidator<ProductEditDTO> _editValidator;
        private readonly IErrorMapper _errorMapper;
        private readonly IImageManager _imageManager;

        public ProductManager(
            IUnitOfWork unitOfWork,
            IValidator<ProductCreateDTO> validator,
            IValidator<ProductEditDTO> editValidator,
            IErrorMapper errorMapper,
            IImageManager imageManager)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _editValidator = editValidator;
            _errorMapper = errorMapper;
            _imageManager = imageManager;
        }


        public async Task<GeneralResult<PagedResult<ProductReadDTO>>> GetProductsAsync(
            ProductFilterParameters filter,
            PaginationParameters pagination)
        {
            var query = (await _unitOfWork.ProductRepository.GetAllWithCategoryAsync()).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Search))
                query = query.Where(p => p.Title.Contains(filter.Search));

            if (filter.MinPrice > 0)
                query = query.Where(p => p.Price >= filter.MinPrice);

            if (filter.MaxPrice > 0)
                query = query.Where(p => p.Price <= filter.MaxPrice);

            if (filter.IsOrganic.HasValue)
                query = query.Where(p => p.IsOrganic == filter.IsOrganic.Value);

            if (filter.IsFeatured.HasValue)
                query = query.Where(p => p.IsFeatured == filter.IsFeatured.Value);

            if (filter.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                query = filter.SortBy.ToLower() switch
                {
                    "price" => filter.SortDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                    "title" => filter.SortDescending ? query.OrderByDescending(p => p.Title) : query.OrderBy(p => p.Title),
                    "rating" => filter.SortDescending ? query.OrderByDescending(p => p.Rating) : query.OrderBy(p => p.Rating),
                    _ => query
                };
            }

            var totalCount = query.Count();

            var products = query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();

            var metadata = new PaginationMetadata
            {
                CurrentPage = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize),
                HasNext = pagination.PageNumber * pagination.PageSize < totalCount,
                HasPrevious = pagination.PageNumber > 1
            };

            return GeneralResult<PagedResult<ProductReadDTO>>.SuccessResult(new PagedResult<ProductReadDTO>
            {
                Items = products.Select(MapToDto),
                Metadata = metadata
            });
        }

        

        public async Task<GeneralResult<ProductReadDTO>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithCategoryAsync(id);

            if (product == null)
                return GeneralResult<ProductReadDTO>.NotFound();

            return GeneralResult<ProductReadDTO>.SuccessResult(MapToDto(product));
        }


        public async Task<GeneralResult<ProductReadDTO>> CreateProductAsync(ProductCreateDTO dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return GeneralResult<ProductReadDTO>.FailResult(_errorMapper.MapError(validationResult));

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(dto.CategoryId);

            if (category == null)
                return GeneralResult<ProductReadDTO>.FailResult("Category Not Found");

            var product = new Product
            {
                Title = dto.Title!,
                Description = dto.Description,
                Price = dto.Price,
                Count = dto.Count,
                CategoryId = dto.CategoryId,
                Unit = dto.Unit,
                IsOrganic = dto.IsOrganic,
                IsFeatured = dto.IsFeatured
            };

            _unitOfWork.ProductRepository.AddAsync(product);
            _unitOfWork.Save();

            return GeneralResult<ProductReadDTO>.SuccessResult(MapToDto(product));
        }


        public async Task<GeneralResult<ProductReadDTO>> UpdateProductAsync(int id, ProductEditDTO dto)
        {
            var validationResult = await _editValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return GeneralResult<ProductReadDTO>.FailResult(_errorMapper.MapError(validationResult));

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product == null)
                return GeneralResult<ProductReadDTO>.NotFound();

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(dto.CategoryId);

            if (category == null)
                return GeneralResult<ProductReadDTO>.FailResult("Category Not Found");

            product.Title = dto.Title!;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Count = dto.Count;
            product.CategoryId = dto.CategoryId;
            product.Unit = dto.Unit;
            product.IsOrganic = dto.IsOrganic;
            product.IsFeatured = dto.IsFeatured;
            product.ImageURL = dto.ImageURL;

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Save();

            return GeneralResult<ProductReadDTO>.SuccessResult(MapToDto(product));
        }


        public async Task<GeneralResult<bool>> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product == null)
                return GeneralResult<bool>.NotFound();

            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Save();

            return GeneralResult<bool>.SuccessResult(true);
        }


        public async Task<GeneralResult<ProductReadDTO>> UploadProductImageAsync(
            int id,
            ImageUploadDto dto,
            string basePath,
            string schema,
            string host)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithCategoryAsync(id);

            if (product == null)
                return GeneralResult<ProductReadDTO>.NotFound();

            var uploadResult = await _imageManager.UploadAsync(dto, basePath, schema, host);

            if (!uploadResult.Success)
                return GeneralResult<ProductReadDTO>.FailResult(uploadResult.Message ?? "Image upload failed");

            product.ImageURL = uploadResult.Data!.ImageURL;

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Save();

            return GeneralResult<ProductReadDTO>.SuccessResult(MapToDto(product));
        }


        private static ProductReadDTO MapToDto(Product p) => new()
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Price = p.Price,
            Count = p.Count,
            ImageURL = p.ImageURL,
            Unit = p.Unit,
            Rating = p.Rating,
            Reviews = p.Reviews,
            IsOrganic = p.IsOrganic,
            IsFeatured = p.IsFeatured,
            CategoryId = p.CategoryId,
            CategoryName = p.Category?.Name,
            CreatedAt = p.CreatedAt
        };
    }
}