using System.ComponentModel.DataAnnotations;

namespace CompanySystem.Common
{
    public class PaginationParameters
    {
        private const int MaxPageSize = 5;
        private int _pageSize = 3;


        [Range(1, int.MaxValue, ErrorMessage = "Page Number must be grater than 0")]
        public int PageNumber { get; set; } = 1;

        [Range(1, MaxPageSize, ErrorMessage = "Page Size must be between 1 and  50")]
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
