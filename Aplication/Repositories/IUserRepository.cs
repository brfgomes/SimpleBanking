using SimpleBanking.Domain;

namespace SimpleBanking.Aplication
{
    public interface IUserRepository
    {
        public bool Insert(User user);
        public bool IfExistsUserEmail(string email);
        public bool IfExistsUserDocument(string document);
        public List<User> GetUserAll();
        public User GetUserById(string id);
    }
}