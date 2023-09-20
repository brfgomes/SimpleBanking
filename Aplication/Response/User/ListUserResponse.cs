using SimpleBanking.Domain;

namespace SimpleBanking.Aplication.Response.User
{
    public class ListUserResponse
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string document { get; set; }
        public string email { get; set; }
        public EUserType type { get; set; }
    }
}
