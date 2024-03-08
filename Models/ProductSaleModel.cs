using DotNet_E_Commerce_Glasses_Web.Utils;

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

        public string getTotalProduct() => CurrencyUtils.CurrencyConvertToString(this.QuanitySale * this.Price);
    }
}