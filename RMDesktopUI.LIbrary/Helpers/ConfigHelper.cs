using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.LIbrary.Helpers
{
    public class ConfigHelper
    {
        public double GetTaxRate()
        {
            string rateTax = ConfigurationManager.AppSettings["taxRate"];
            bool isValidTaxRate = Double.TryParse(rateTax, out double output);
            if(isValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("This tax rate is not set up properly");
            }
            return output;
        }
    }
}
