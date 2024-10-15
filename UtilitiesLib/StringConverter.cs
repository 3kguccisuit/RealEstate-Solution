namespace UtilitiesLib
{
    public static class StringConverter
    {
        // 3.11.1 Convert a string to an integer using TryParse
        public static bool ConvertStringToInteger(string strNum, out int result)
        {
            return int.TryParse(strNum, out result);
        }

        // 3.11.2 Convert a string to a decimal using TryParse
        public static bool ConvertStringToDecimal(string strNum, out decimal result)
        {
            return decimal.TryParse(strNum, out result);
        }

        // 3.11.3 Overloaded method to convert a string to an integer with range check
        public static bool ConvertStringToInteger(string strNum, int lowLimit, int highLimit, out int result)
        {
            if (int.TryParse(strNum, out result))
            {
                return result >= lowLimit && result <= highLimit;
            }

            // Set the result to default (0) if parsing fails
            result = default;
            return false;
        }

        // 3.11.3 Overloaded method to convert a string to a decimal with range check
        public static bool ConvertStringToDecimal(string strNum, decimal lowLimit, decimal highLimit, out decimal result)
        {
            if (decimal.TryParse(strNum, out result))
            {
                return result >= lowLimit && result <= highLimit;
            }

            // Set the result to default (0) if parsing fails
            result = default;
            return false;
        }
    }
}
