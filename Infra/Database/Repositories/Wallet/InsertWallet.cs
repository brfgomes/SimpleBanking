using SimpleBanking.Infra.Database.Interfaces;

namespace SimpleBanking.Infra.Database.Repositories.Wallet
{
    public class InsertWallet
    {
        public static void Execute(IDatabaseConnection databaseConnection, string id, decimal balance)
        {
            var sql = "INSERT INTO wallets (id, balance) VALUES(@Id, @Balance);";
            var parameters = new Dictionary<string, object>
            {
                { "@Id", id },
                { "@Balance", balance }
            };
            databaseConnection.Open();
            databaseConnection.Command(sql, parameters);
            databaseConnection.Close();
        }
    }
}