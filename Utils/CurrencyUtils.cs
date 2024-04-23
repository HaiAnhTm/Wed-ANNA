namespace DotNet_E_Commerce_Glasses_Web.Utils
{
    public class CurrencyUtils
    {
        public static string CurrencyConvertToString(long? donGia) => $"{donGia:N0} VND" ?? string.Empty;
        public static string CurrencyConvertToStringno(long? donGia) => $"{donGia:N0}" ?? string.Empty;
    }
}