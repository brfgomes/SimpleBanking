using SimpleBanking.Domain;
using SimpleBanking.Infra.Database;
using SimpleBanking.Infra.Database.Repositories.User;

namespace SimpleBanking.Aplication
{
    public class CreateUser
    {
        public static (bool Success, string Message) Execute(CreateUserRequest request)
        {
            var databaseConnection = new SQLiteAdapter();
            
            if(request == null)
                return (false, "Usuário não pode ser nulo");
            

            var user = new User(
                request.name,
                new Document(request.document),
                new Email(request.email),
                new Password(request.password),
                (EUserType)request.type,
                new Wallet(request.wallet)
            );

            var result = InsertUser.Execute(
                databaseConnection, 
                user.Id.ToString(), 
                user.Name, 
                user.Document.Code, 
                user.Email.Addres, 
                user.Password.Value, 
                Convert.ToInt16(user.Type), 
                user.Wallet.Id.ToString(), 
                user.Wallet.Balance
            );
            return (result.Success, result.Message);
        }
    }
}