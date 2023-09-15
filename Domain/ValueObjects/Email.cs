namespace SimpleBanking.Domain
{
    public class Email
    {
        public Email(string address)
        {
            Address = address;

            if(address == "")
            {
                throw new Exception("E-mail não informado");
            }

            if (!address.Contains('@') || !address.Contains('.') || address.Length < 8){
                throw new Exception("E-mail inválido");
            }
        }

        public string Address { get; private set; }
    }
}