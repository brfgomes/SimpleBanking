namespace SimpleBanking.Domain.Exceptions
{
    public class EmptyException : Exception
    {
        public EmptyException(string message = "Não pode ser vazio")
            :base(message)
        {
            
        }
        public static void Throw(string value, string message)
        {
            if (string.IsNullOrEmpty(value))
                throw new EmptyException(message);
        }

        public static void Throw(object value, string message)
        {
            if (value == null)
                throw new EmptyException(message);
        }


    }
}



