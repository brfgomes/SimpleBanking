using SimpleBanking.Domain;
using Newtonsoft.Json;
using System.Text;
using Flunt.Notifications;
using SimpleBanking.Aplication.Response.User;

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

            if (_userRepository.GetUserByDocument(request.document) != null)
                return new GenericResponse(false, "Documento já existe");

            if (_userRepository.GetUserByEmail(request.email)  != null)
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
                    Id = user.Id,
                    Name = user.Name,
                    Document = user.Document.Code,
                    Email = user.Email.Address,
                    Type = user.Type,
                    Wallet = _walletRepository.GetWalletByUserId(user.Id).Balance
                });
            }
            if (listUsers == null)
                return new GenericResponse(false, "Erro ao listar usuários");

            return new GenericResponse(true, "OK", list);
        }

        public GenericResponse Change(ChangeUserRequest request)
        {
            #region Validar user e criar entidade
            var existUserByDocument = _userRepository.GetUserByDocument(request.document);
            var existUserByEmail = _userRepository.GetUserByEmail(request.email);

            if(string.IsNullOrEmpty(request.id))
                return new GenericResponse(false, "Id do usuario não informado");

                if(existUserByDocument != null || existUserByEmail != null)
                {
                    if(request.id != existUserByDocument.Id.ToString())
                        return new GenericResponse(false, "Já existe um usuario utilizando o mesmo documento");

                    if(request.id != existUserByEmail.Id.ToString())
                        return new GenericResponse(false, "Já existe um usuario utilizando o mesmo e-mail");
                }

            var changedUser = new User(
                request.name,
                new Document(request.document),
                new Email(request.email),
                new Password(request.password),
                request.type
            );

            changedUser.SetId(new Guid(request.id));
            
            #endregion
            var user = _userRepository.GetUserById(request.id);

            _userRepository.UpdateUser(changedUser);

            return new GenericResponse(true, "Usuário alterado com sucesso!");
        }
    }
}