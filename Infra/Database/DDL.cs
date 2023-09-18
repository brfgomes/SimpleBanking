using SimpleBanking.Infra.Database.Interfaces;

namespace SimpleBanking.Infra.Database
{
    public class DDL
    {
        public static void Execute(IDatabaseConnection databaseConnection)
        {
            databaseConnection.Open();
            var sql = "";

            sql = """
            CREATE TABLE IF NOT EXISTS users(
            id uuid NOT NULL,
            name varchar(100) NOT NULL,
            document varchar(11) NOT NULL,
            email varchar(50) NOT NULL,
            password varchar(50) NOT NULL,
            type int(1) NOT NULL,
            PRIMARY KEY (id)
            );
            """;
            databaseConnection.Command(sql);

            sql = """
            CREATE TABLE IF NOT EXISTS wallets(
            userid uuid NOT NULL,
            balance decimal NOT NULL,
            lasttransactiondate datetime,
            PRIMARY KEY (userid)
            );
            """;
            databaseConnection.Command(sql);

            sql = """
            CREATE TABLE IF NOT EXISTS transactions(
            id uuid NOT NULL,
            value decimal NOT NULL,
            sender uuid NOT NULL,
            receiver uuid NOT NULL,
            date datetime NOT NULL
            );
            """;
            databaseConnection.Command(sql);

            databaseConnection.Close();
        }
    }
}