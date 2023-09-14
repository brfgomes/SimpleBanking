using SimpleBanking.Domain;
using SimpleBanking.Infra.Database.Repositories.User;

namespace SimpleBanking.Aplication
{
    public class CreateUser
    {
        public static bool Execute(CreateUserRequest request)
        {

            //inicio teste
            GetUser.Execute();
            //fim teste

            if(request == null)
                return false;
            
            var user = new User(
                request.name,
                new Document(request.document),
                new Email(request.email),
                new Password(request.password),
                request.type,
                new Wallet(request.wallet)
            );
            return true;
        }
    }
}