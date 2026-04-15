namespace CompanySystem.Common
{
    public class ProductFilterParameters : BaseFilterParameters
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public bool? IsOrganic { get; set; }
        public bool? IsFeatured { get; set; }
        public int? CategoryId { get; set; }
    }
}