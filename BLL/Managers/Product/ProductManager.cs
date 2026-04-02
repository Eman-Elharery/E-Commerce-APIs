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

        public ProductManager(
            IUnitOfWork unitOfWork,
            IValidator<ProductCreateDTO> validator,
            IValidator<ProductEditDTO> editValidator,
            IErrorMapper errorMapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _editValidator = editValidator;
            _errorMapper = errorMapper;
        }

        public async Task<GeneralResult<PagedResult<ProductReadDTO>>> GetProductsAsync(
            ProductFilterParameters filter,
            PaginationParameters pagination)
        {
            var query = (await _unitOfWork.ProductRepository.GetAllWithCategoryAsync()).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(p => p.Title.Contains(filter.Search));
            }

            if (filter.MinPrice > 0)
            {
                query = query.Where(p => p.Price >= filter.MinPrice);
            }

            if (filter.MaxPrice > 0)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice);
            }

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                switch (filter.SortBy.ToLower())
                {
                    case "price":
                        query = filter.SortDescending
                            ? query.OrderByDescending(p => p.Price)
                            : query.OrderBy(p => p.Price);
                        break;

                    case "title":
                        query = filter.SortDescending
                            ? query.OrderByDescending(p => p.Title)
                            : query.OrderBy(p => p.Title);
                        break;
                }
            }

            var totalCount = query.Count();

            var products = query
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToList();

            var items = products.Select(p => new ProductReadDTO
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Count = p.Count
            });

            var metadata = new PaginationMetadata
            {
                CurrentPage = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize),
                HasNext = pagination.PageNumber * pagination.PageSize < totalCount,
                HasPrevious = pagination.PageNumber > 1
            };

            var result = new PagedResult<ProductReadDTO>
            {
                Items = items,
                Metadata = metadata
            };

            return GeneralResult<PagedResult<ProductReadDTO>>.SuccessResult(result);
        }
        public async Task<GeneralResult<ProductReadDTO>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithCategoryAsync(id);

            if (product == null)
                return GeneralResult<ProductReadDTO>.NotFound();

            var dto = new ProductReadDTO
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Count = product.Count,
            };

            return GeneralResult<ProductReadDTO>.SuccessResult(dto);
        }

        public async Task<GeneralResult<ProductReadDTO>> CreateProductAsync(ProductCreateDTO dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = _errorMapper.MapError(validationResult);
                return GeneralResult<ProductReadDTO>.FailResult(errors);
            }

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(dto.CategoryId);

            if (category == null)
                return GeneralResult<ProductReadDTO>.FailResult("Category Not Found");

            var product = new Product
            {
                Title = dto.Title!,
                Price = dto.Price,
                Count = dto.Count,
                CategoryId = dto.CategoryId
            };

            _unitOfWork.ProductRepository.AddAsync(product);

             _unitOfWork.Save();

            var result = new ProductReadDTO
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Count = product.Count,
            };

            return GeneralResult<ProductReadDTO>.SuccessResult(result);
        }

        public async Task<GeneralResult<ProductReadDTO>> UpdateProductAsync(int id, ProductEditDTO dto)
        {
            var validationResult = await _editValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = _errorMapper.MapError(validationResult);
                return GeneralResult<ProductReadDTO>.FailResult(errors);
            }

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product == null)
                return GeneralResult<ProductReadDTO>.NotFound();

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(dto.CategoryId);

            if (category == null)
                return GeneralResult<ProductReadDTO>.FailResult("Category Not Found");

            product.Title = dto.Title!;
            product.Price = dto.Price;
            product.Count = dto.Count;
            product.CategoryId = dto.CategoryId;

            _unitOfWork.ProductRepository.Update(product);

             _unitOfWork.Save();

            var result = new ProductReadDTO
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Count = product.Count
            };

            return GeneralResult<ProductReadDTO>.SuccessResult(result);
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
    }
}
