using SimpleBanking.Aplication;
using SimpleBanking.Domain;

namespace SimpleBanking.Infra.Database.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IDatabaseConnection _databaseConnection;

        public WalletRepository (IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public Wallet GetWalletByUserId(Guid userId)
        {
            try
            {
                _databaseConnection.Open();

                var sql = "SELECT * FROM wallets WHERE userid = @UserId";
                var parameters = new Dictionary<string, object>{
                    {"@UserId", userId.ToString()}
                };
                var dbWallets = _databaseConnection.Query(sql, parameters);
                Wallet wallet = null;
                while (dbWallets.Read())
                {
                    wallet = new Wallet(
                        (decimal)dbWallets["balance"]
                    );

                    wallet.SetId(userId);
                    var lastTransactionDateDB = dbWallets["lasttransactiondate"].ToString();
                    if (lastTransactionDateDB != "")
                    {
                        wallet.SetLastTransactionDate((DateTime)dbWallets["lasttransactiondate"]);
                    }

                }
                _databaseConnection.Close();
                return wallet!;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Insert(Guid userId, decimal balance)
        {
            var sql = "INSERT INTO wallets (userid, balance) VALUES(@UserId, @Balance);";
            var parameters = new Dictionary<string, object>
            {
                { "@UserId", userId.ToString() },
                { "@Balance", balance }
            };
            try
            {
                _databaseConnection.Open();
                _databaseConnection.Command(sql, parameters);
                _databaseConnection.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {                   
            }
        }

        public void UpdateBalance(Guid userId, decimal balance)
        {
            var sql = "UPDATE wallets SET balance= @Balance, lasttransactiondate= CURRENT_TIMESTAMP WHERE userid= @UserId;";
            var parameters = new Dictionary<string, object>
            {
                { "@UserId", userId.ToString() },
                { "@Balance", balance }
            };
            try
            {
                _databaseConnection.Open();
                _databaseConnection.Command(sql, parameters);
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                _databaseConnection.Close();
            }
        }
        public List<Wallet> GetAllWallets()
        {
            _databaseConnection.Open();
            var sql = "SELECT * FROM wallets";
            var dbWallets = _databaseConnection.Query(sql);

            List<Wallet> wallets = new List<Wallet>();
            while (dbWallets.Read())
                {
                    Wallet wallet = null;
                    wallet = new Wallet(
                        (decimal)dbWallets["balance"]
                    );

                    wallet.SetId(new Guid(dbWallets["userid"].ToString()));
                    var lastTransactionDateDB = dbWallets["lasttransactiondate"].ToString();
                    if (lastTransactionDateDB != "")
                    {
                        wallet.SetLastTransactionDate((DateTime)dbWallets["lasttransactiondate"]);
                    }

                    wallets.Add(wallet);

                }
            _databaseConnection.Close();
            return wallets!;

        }
    }
}