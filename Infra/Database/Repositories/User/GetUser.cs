using System.Data.Common;
using System.Text;
using SimpleBanking.Infra.Database.Interfaces;

namespace SimpleBanking.Infra.Database.Repositories.User
{
    public static class GetUser
    {
        public static DbDataReader Execute(IDatabaseConnection databaseConnection, string document = null, string email = null)
        {
            databaseConnection.Open();
            var parameters = new Dictionary<string, object>();
            var sql = new StringBuilder();
            sql.Append("SELECT * FROM users");

            if(document != null && email != null)
            {
                sql.Append(" WHERE document = @Document or email = @Email");
                parameters.Add("@Document", document);
                parameters.Add("@Email", email);
            }
            else if(document != null)
            {
                sql.Append(" WHERE document = @Document");
                parameters.Add("@Document", document);
            }
            else if(email != null)
            {
                sql.Append(" WHERE email = @Email");
                parameters.Add("@Email", email);
            }
            var users = databaseConnection.Query(sql.ToString(), parameters);
            databaseConnection.Close();
            return users;
        }
    }
}