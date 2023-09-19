using Flunt.Validations;

namespace SimpleBanking.Domain.Contracts
{
    public class CreateUserContract : Contract<User>
    {
        public CreateUserContract(User user)
        {
            Requires()
                .IsNotNull(user, "Usuario não pode ser nulo")
                .IsNotEmail(user.Name, "Nome de usuario nao pode ser vazio")
                .IsGreaterThan(user.Name.Length, 3, "Nome do usuario precisa ter mais que 3 caracteres")
                .IsLowerThan(user.Name.Length, 100, "Nome do usuario pode ter no maximo 100 caracteres")
                .IsFalse(user.Type != EUserType.Seller && user.Type != EUserType.Common, "Tipo inválido");
        }
    }
}
