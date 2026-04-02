namespace CompanySystem.Common
{
    public class ProductFilterParameters : BaseFilterParameters
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
