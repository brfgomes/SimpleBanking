namespace SimpleBanking.Domain
{
    public class Password
    {
        public Password(string value)
        {
            Value = value;

            if(value == ""){
                throw new Exception("Senha não informado");
            }

            if(value.Length < 8 || value.Length > 50){
                throw new Exception("Senha invalida");
            }
        }

        public string Value { get; private set; }
    }
}