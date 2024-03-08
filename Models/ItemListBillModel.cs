namespace DotNet_E_Commerce_Glasses_Web.Models
{
    public class ItemListBillModel
    {
        public string NameProduct { get; set; }
        public int Quantity { get; set; }
        public ItemListBillModel(string nameProduct, int quantity)
        {
            this.NameProduct = nameProduct;
            this.Quantity = quantity;
        }
    }
}