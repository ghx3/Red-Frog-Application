using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFrogs.Helpers
{
    public static class HelperClass
    {
        public static int ConvertToInt(bool value)
        {
            return value ? 1 : 0;
        }

        public static int GreaterThanZero(int num)
        {
            if(num <= 0)
            {
                return 0;
            }
            return num;
        }

        public static int HasValue(string num)
        {
            if (String.IsNullOrWhiteSpace(num))
            {
                return 0;
            }

            return Convert.ToInt32(num);
        }
    }
}
