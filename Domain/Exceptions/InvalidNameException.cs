namespace SimpleBanking.Domain.Exceptions
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message = "Nome Inválido") : base(message)
        {
        }

        public static void ThrowInvalidName(string name, string message)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidNameException(message);
        }
    }
}
