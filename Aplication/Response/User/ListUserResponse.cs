using SimpleBanking.Domain;

namespace SimpleBanking.Aplication.Response.User
{
    public class ListUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public decimal Wallet { get; set; }
        public EUserType Type { get; set; }
    }
}
