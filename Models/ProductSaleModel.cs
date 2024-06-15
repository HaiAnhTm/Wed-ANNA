namespace DotNet_E_Commerce_Glasses_Web.Models
{
    public class ProductSaleModel : Product
    {
        public int QuanitySale { get; set; }
        public ProductSaleModel(int quanitySale, Product product) : base(product)
        {
            this.QuanitySale = quanitySale;
        }
        public ProductSaleModel(int quanitySale)
        {
            this.QuanitySale = quanitySale;
        }
        public string getTotalProduct() => (this.QuanitySale * (this.Price ?? 0) * (100 - this.Discount ?? 0) / 100).ToString("#,##0") + " VND";
    }
}