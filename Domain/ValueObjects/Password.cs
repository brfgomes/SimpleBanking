using SimpleBanking.Domain.Exceptions;

namespace SimpleBanking.Domain
{
    public class Password
    {
        public Password(string value)
        {
            EmptyException.Throw(value, "Senha não informado");
            if(value.Length < 8 || value.Length > 50){
                throw new Exception("Senha invalida");
            }

            Value = value;
        }

        public string Value { get; private set; }
    }
}