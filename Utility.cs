using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERM_TestTask
{
    public class Utility
    {
        public static string ObjToStr(Object obj)
        {
            if (obj == null)
                return "";

            return obj.ToString();
        }

        public static DateTime? ObjToDateTime(Object obj)
        {
            if (obj == null)
                return null;

            return DateTime.Parse(obj.ToString());
        }

        public static decimal ObjToDecimal(Object obj)
        {
            if (obj == null)
                return 0M;
            return (decimal.Parse(obj.ToString(),
                 System.Globalization.NumberStyles.AllowParentheses |
                 System.Globalization.NumberStyles.AllowLeadingWhite |
                 System.Globalization.NumberStyles.AllowTrailingWhite |
                 System.Globalization.NumberStyles.AllowThousands |
                 System.Globalization.NumberStyles.AllowDecimalPoint |
                 System.Globalization.NumberStyles.AllowLeadingSign));
        }

        public static int ObjToInt(Object obj)
        {
            if (obj == null)
                return 0;

            return Convert.ToInt32(obj.ToString());
        }

        public static bool ObjToBool(Object obj)
        {
            if (obj == null)
                return false;

            var val = obj.ToString();
            if (val.Equals("false", StringComparison.InvariantCultureIgnoreCase) || val == "0")
                return false;
            if (val.Equals("true", StringComparison.InvariantCultureIgnoreCase) || val == "1")
                return true;

            return false;
        }

        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
    }
}
