namespace SimpleBanking.Aplication
{
    public class GenericResponse
    {
        public GenericResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public GenericResponse(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
    }
}