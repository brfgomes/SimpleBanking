using System.Text;
using SimpleBanking.Domain;
using SimpleBanking.Infra.Database;
using SimpleBanking.Infra.Database.Repositories.User;

namespace SimpleBanking.Aplication
{
    public class ListUsers
    {
        public static string Execute()
        {
            var databaseConnection = new SQLiteAdapter();
            var databaseUsers = GetUser.Execute(databaseConnection);

            var listUsers = new StringBuilder();
            while(databaseUsers.Read())
            {
                listUsers.Append(databaseUsers["id"].ToString());
                listUsers.Append(databaseUsers["name"].ToString());
                listUsers.Append(databaseUsers["email"].ToString());
                listUsers.Append(databaseUsers["password"].ToString());
                listUsers.Append(databaseUsers["type"].ToString());
                listUsers.Append(databaseUsers["wallet"].ToString());

            }

            return listUsers.ToString();
        }
    }
}