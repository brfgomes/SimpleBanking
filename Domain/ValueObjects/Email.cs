using SimpleBanking.Domain.Exceptions;

namespace SimpleBanking.Domain
{
    public class Email
    {
        public Email(string address)
        {
            EmptyException.Throw(address, "E-mail não informado");

            if (!address.Contains('@') || !address.Contains('.') || address.Length < 8){
                throw new Exception("E-mail inválido");
            }

            Address = address;
        }

        public string Address { get; private set; }
    }
}