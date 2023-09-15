namespace SimpleBanking.Aplication
{
    public class GenericResponse
    {
        public GenericResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; private set; }
        public string Message { get; private set; }
    }
}