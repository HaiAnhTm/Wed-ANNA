using System;

namespace DotNet_E_Commerce_Glasses_Web.Models
{
    public class DashBoardModel
    {
        public DateTime Date;
        public long TotalPay;
        public int Product;
        public DashBoardModel(DateTime date, long totalPay, int product)
        {
            this.Date = date;
            this.TotalPay = totalPay;
            this.Product = product;
        }
    }
}