using SimpleBanking.Aplication.Database;

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
            id varchar NOT NULL,
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
            userid varchar NOT NULL,
            balance decimal NOT NULL,
            lasttransactiondate datetime,
            PRIMARY KEY (userid)
            );
            """;
            databaseConnection.Command(sql);

            sql = """
            CREATE TABLE IF NOT EXISTS transactions(
            id varchar NOT NULL,
            value decimal NOT NULL,
            sender varchar NOT NULL,
            receiver varchar NOT NULL,
            date datetime NOT NULL
            );
            """;
            databaseConnection.Command(sql);

            databaseConnection.Close();
        }
    }
}