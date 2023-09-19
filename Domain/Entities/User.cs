using SimpleBanking.Domain.Exceptions;
using Flunt.Notifications;
using Flunt.Validations;
using SimpleBanking.Domain.Contracts;

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

            AddNotifications(new CreateUserContract(this));
        }

        public string Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public EUserType Type { get; private set; }
    }
}