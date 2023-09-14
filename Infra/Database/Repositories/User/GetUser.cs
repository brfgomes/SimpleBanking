using System.Data.Common;
using SimpleBanking.Infra.Database.Interfaces;

namespace SimpleBanking.Infra.Database.Repositories.User
{
    public static class GetUser
    {
        private static readonly IDatabaseConnection databaseConnection;
        public static DbDataReader Execute()
        {
            databaseConnection.Open();
            return databaseConnection.Query("select * from users");
        }
    }
}