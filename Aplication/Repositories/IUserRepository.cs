using SimpleBanking.Domain;

namespace SimpleBanking.Aplication
{
    public interface IUserRepository
    {
        public bool Insert(User user);
        public int IfExistsUserEmail(string email);
        public int IfExistsUserDocument(string document);
        public List<User> GetAllUsers();
        public User GetUserById(string id);
        public bool UpdateUser(User user);
    }
}