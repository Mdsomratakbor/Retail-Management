using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.Helper
{
    public class ConfigHelper 
    {
        // TODO: Move this form config to the API
        public static decimal GetTaxRate()
        {
            string rateTax = ConfigurationManager.AppSettings["taxRate"];
            bool isValidTaxRate = Decimal.TryParse(rateTax, out decimal output);
            if (isValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("This tax rate is not set up properly");
            }
            return output;
        }
    }
}
