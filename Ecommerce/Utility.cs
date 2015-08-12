using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ecommerce
{
    public static class Utility
    {
        public static decimal GetConvertedPrice(decimal amount, string fromCurrency, string toCurrency)
        {
            toCurrency = "USD";
            if (fromCurrency == "Naira")
            {
              fromCurrency  ="NGN";
            }
            else if (fromCurrency == "Kwanza")
            {
                fromCurrency="AOA";
            }
            else if (fromCurrency == "Cedi")
            {
                fromCurrency = "GHS";
            }
            WebClient web = new WebClient();
            string url = string.Format("http://finance.yahoo.com/d/quotes.csv?e=.csv&f=sl1d1t1&s={0}{1}=X", fromCurrency.ToUpper(), toCurrency.ToUpper());
            string response = web.DownloadString(url);
            string[] values = Regex.Split(response, ",");
            decimal rate = System.Convert.ToDecimal(values[1]);
            return rate * amount;
        }
    }
}
