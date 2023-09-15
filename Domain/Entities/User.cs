namespace SimpleBanking.Domain
{
    public class User : Entity
    {
        public User(string name, Document document, Email email, Password password, EUserType type)
        {
            Name = name;
            Document = document;
            Email = email;
            Password = password;
            Type = type;

            if(name == "")
            {
                throw new Exception("Nome não informado");
            }

            if(name.Length < 3 || name.Length > 100)
                throw new Exception("Nome inválido");

        }

        public string Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public EUserType Type { get; private set; }
    }
}