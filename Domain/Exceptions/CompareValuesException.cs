namespace SimpleBanking.Domain.Exceptions
{
    public class CompareValuesException : Exception
    {
        public CompareValuesException(string message = "Erro ao comparar valores")
            :base(message)
        {
            
        }

        public static void IsLowerThan(decimal value1, decimal value2, string message)
        {
            if (value1 < value2)
                throw new CompareValuesException(message);
        }
        public static void IsLowerOrEqualsThan(decimal value1, decimal value2, string message)
        {
            if (value1 <= value2)
                throw new CompareValuesException(message);
        }
        public static void IsGreaterThan(decimal value1, decimal value2, string message)
        {
            if (value1 > value2)
                throw new CompareValuesException(message);
        }
        public static void IsGreaterOrEqualsThan(decimal value1, decimal value2, string message)
        {
            if (value1 >= value2)
                throw new CompareValuesException(message);
        }
        public static void IsEqualsThan(decimal value1, decimal value2, string message)
        {
            if (value1 == value2)
                throw new CompareValuesException(message);
        }
        public static void IsEqualsThan(string value1, string value2, string message)
        {
            if (value1 == value2)
                throw new CompareValuesException(message);
        }
        public static void IsEqualsThan(object value1, object value2, string message)
        {
            if (value1 == value2)
                throw new CompareValuesException(message);
        }
        public static void IsEqualsThan(Guid value1, Guid value2, string message)
        {
            if (value1 == value2)
                throw new CompareValuesException(message);
        }
        public static void IsEqualsThan(bool value1, bool value2, string message)
        {
            if (value1 == value2)
                throw new CompareValuesException(message);
        }
        public static void IsOtherThan(string value1, string value2, string message)
        {
            if(value1 != value2)
                throw new CompareValuesException(message);
        }
        public static void IsOtherThan(int value1, int value2, string message)
        {
            if(value1 != value2)
                throw new CompareValuesException(message);
        }

    }
}