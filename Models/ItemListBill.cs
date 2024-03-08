namespace DotNet_E_Commerce_Glasses_Web.Models
{
    public class ItemListBill
    {
        public string NameProduct { get; set; }
        public int Quantity { get; set; }
        public ItemListBill(string nameProduct, int quantity)
        {
            this.NameProduct = nameProduct;
            this.Quantity = quantity;
        }
    }
}