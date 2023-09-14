using SimpleBanking.Infra.Database.Interfaces;

namespace SimpleBanking.Infra.Database
{
    public class DDL
    {
        private static readonly IDatabaseConnection databaseConnection;
        public static void Execute()
        {
            databaseConnection.Open();
            var sql = "";

            sql = """
            CREATE TABLE IF NOT EXISTS users(
            id varchar NOT NULL,
            name varchar(100) NOT NULL,
            document varchar(11) NOT NULL,
            email varchar(50) NOT NULL,
            password varchar(50) NOT NULL,
            type int(1) NOT NULL,
            wallet varchar NOT NULL,
            PRIMARY KEY (id)
            );
            """;
            databaseConnection.Command(sql);

            sql = """
            CREATE TABLE IF NOT EXISTS wallets(
            id varchar NOT NULL,
            balance decimal NOT NULL,
            last_transaction_date datetime,
            PRIMARY KEY (id)
            );
            """;
            databaseConnection.Command(sql);

            sql = """
            CREATE TABLE IF NOT EXISTS transactions(
            id varchar NOT NULL,
            value decimal NOT NULL,
            sender varchar NOT NULL,
            receiver varchar NOT NULL
            );
            """;
            databaseConnection.Command(sql);

            databaseConnection.Close();
        }
    }
}