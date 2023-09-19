using SimpleBanking.Domain;
using Newtonsoft.Json;
using System.Text;
using Flunt.Notifications;

namespace SimpleBanking.Aplication
{
    public class UserUseCase : IUser    
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;


        public UserUseCase(IUserRepository userRepository, IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
        }
        
        public GenericResponse Create(CreateUserRequest request)
        {
            #region Validar user e criar entidade
            if (_userRepository.IfExistsUserDocument(request.document) != 0)
                return new GenericResponse(false, "Documento já existe");

            if (_userRepository.IfExistsUserEmail(request.email) != 0) 
                return new GenericResponse(false, "E-mail já existe");                


            var newUser = new User(
                request.name,
                new Document(request.document),
                new Email(request.email),
                new Password(request.password),
                (EUserType)request.type
            );

            if(!newUser.IsValid)
            {
                var notifications = new StringBuilder();
                notifications.AppendLine("[");
                foreach(Notification notification in newUser.Notifications)
                {
                    notifications.AppendLine($@"""{notification.Key}"", ");
                }
                notifications.AppendLine("]");

                return new GenericResponse(false, notifications.ToString());
            }
            #endregion

            #region Criar user e sua carteira no banco
            _userRepository.Insert(newUser);

            _walletRepository.Insert(newUser.Id, request.wallet);
            #endregion

            return new GenericResponse(true, "Usuário criado com sucesso!");
        }

        public GenericResponse GetAll()
        {
            var listUsers = _userRepository.GetAllUsers();

            var json = new StringBuilder();

            json.AppendLine("[");
            foreach (var user in listUsers)
            {
                json.AppendLine("{");
                json.AppendLine(@$"""id"": ""{user.Id}"",");
                json.AppendLine(@$"""name"": ""{user.Name}"",");
                json.AppendLine(@$"""document"": ""{user.Document.Code}"",");
                json.AppendLine(@$"""email"": ""{user.Email.Address}"",");
                json.AppendLine(@$"""password"": ""{user.Password.Value}"",");
                json.AppendLine(@$"""type"": ""{user.Type}""");
                json.AppendLine("}, ");
            }
            json.AppendLine("]");

            if (listUsers == null)
            {
                return new GenericResponse(false, "Erro ao listar usuarios");
            }
            return new GenericResponse(true, json.ToString());
        }

        public GenericResponse Change(ChangeUserRequest request)
        {
            #region Validar user e criar entidade

            var changedUser = new User(
                request.name,
                new Document(request.document),
                new Email(request.email),
                new Password(request.password),
                (EUserType)request.type
            );

            changedUser.SetId(new Guid(request.id));
            #endregion

            _userRepository.UpdateUser(changedUser);
            _walletRepository.UpadateBalance(changedUser.Id, request.wallet);

            return new GenericResponse(true, "Usuário alterado com sucesso!");
        }
    }
}