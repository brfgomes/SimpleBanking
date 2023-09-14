namespace SimpleBanking.Domain
{
    public class Email
    {
        public Email(string addres)
        {
            Addres = addres;

            if(addres == "")
            {
                throw new Exception("E-mail não informado");
            }

            if (!addres.Contains('@') || !addres.Contains('.') || addres.Length < 8){
                throw new Exception("E-mail inválido");
            }
        }

        public string Addres { get; private set; }
    }
}