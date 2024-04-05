using DotNet_E_Commerce_Glasses_Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DotNet_E_Commerce_Glasses_Web.Utils
{
    public class JsonUtils
    {
        public static string ConvertToJson(Dictionary<string, int> lists) => JsonConvert.SerializeObject(lists);

        public static string ConvertDicToJson(Dictionary<int, int> dic) => JsonConvert.SerializeObject(dic);
        public static Dictionary<int, int> ConvertStrToDic(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;
            return JsonConvert.DeserializeObject<Dictionary<int, int>>(json);
        }

        public static Dictionary<string, int> ConvertToDic(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;
            return JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
        }


        public static Bill convertJsonToBill(string json) => JsonConvert.DeserializeObject<Bill>(json);












        public static Dictionary<int, int> convertJsonCartToDic(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;
            return JsonConvert.DeserializeObject<Dictionary<int, int>>(json);
        }
        public static string convertDicToCartJson(Dictionary<int, int> dic) => JsonConvert.SerializeObject(dic);

    }
}