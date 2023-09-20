using SimpleBanking.Domain;
using Newtonsoft.Json;
using System.Text;
using Flunt.Notifications;
using SimpleBanking.Aplication.Response.User;
using SimpleBanking.Aplication.UseCases.User;

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
                request.type
            );

            if (!newUser.IsValid)
            {
                return new GenericResponse(false, "Usuario invalido", newUser.Notifications);
            }

            #endregion

            #region Criar user e sua carteira no banco
            _userRepository.Insert(newUser);

            _walletRepository.Insert(newUser.Id, request.wallet);
            #endregion

            return new GenericResponse(true, "Usuário criado com sucesso!", new CreateUserResponse(newUser.Id));
        }

        public GenericResponse GetAll()
        {
            var listUsers = _userRepository.GetAllUsers();
            List<ListUserResponse> list = new List<ListUserResponse>();

            foreach (var user in listUsers)
            {
                list.Add(new ListUserResponse()
                {
                    id = user.Id,
                    name = user.Name,
                    document = user.Document.Code,
                    email = user.Email.Address,
                    type = user.Type
                });
            }


            if (listUsers == null)
                return new GenericResponse(false, "Erro ao listar usuarios");

            return new GenericResponse(true, "OK", list);
        }

        public GenericResponse Change(ChangeUserRequest request)
        {
            #region Validar user e criar entidade

            var changedUser = new User(
                request.name,
                new Document(request.document),
                new Email(request.email),
                new Password(request.password),
                request.type
            );

            changedUser.SetId(new Guid(request.id));
            #endregion

            _userRepository.UpdateUser(changedUser);
            _walletRepository.UpadateBalance(changedUser.Id, request.wallet);

            return new GenericResponse(true, "Usuário alterado com sucesso!");
        }
    }
}