using SimpleBanking.Domain;

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
            if (_userRepository.IfExistsUserDocument(request.document)) 
                throw new Exception("Documento já existe");

            if (_userRepository.IfExistsUserEmail(request.email)) 
                throw new Exception("E-mail já existe");                

            var newUser = new User(
                request.name,
                new Document(request.document),
                new Email(request.email),
                new Password(request.password),
                (EUserType)request.type
            );

            _userRepository.Insert(newUser);

            _walletRepository.Insert(newUser.Id, request.wallet);

            return new GenericResponse(true, "Usuário criado com sucesso!");
        }
    }
}