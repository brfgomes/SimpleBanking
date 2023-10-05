using SimpleBanking.Aplication;

namespace SimpleBanking.Infra.Database
{
    public class DDL : IDDL
    {
        private readonly IDatabaseConnection _databaseConnection;

        public DDL(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        
        private static readonly List<string> textos;
        public void Execute()
        {
            var databaseAdapterName  = _databaseConnection.ToString().Split(".")[3];
            
            switch (databaseAdapterName)
            {
                case "PostgresSQLAdapter":
                {
                    _databaseConnection.Open();
                    var sql = "";
                    
                    sql = """
                    CREATE TABLE IF NOT EXISTS users(
                    id varchar NOT NULL,
                    name varchar(100) NOT NULL,
                    document varchar(11) NOT NULL,
                    email varchar(50) NOT NULL,
                    password varchar(50) NOT NULL,
                    type int NOT NULL,
                    PRIMARY KEY (id)
                    );
                    """;
                    _databaseConnection.Command(sql);
                    
                    sql = """
                    CREATE TABLE IF NOT EXISTS wallets(
                    userid varchar NOT NULL,
                    balance decimal NOT NULL,
                    lasttransactiondate timestamp,
                    PRIMARY KEY (userid)
                    );
                    """;
                    _databaseConnection.Command(sql);
                    
                    sql = """
                    CREATE TABLE IF NOT EXISTS transactions(
                    id varchar NOT NULL,
                    value decimal NOT NULL,
                    sender varchar NOT NULL,
                    receiver varchar NOT NULL,
                    date timestamp NOT NULL
                    );
                    """;
                    _databaseConnection.Command(sql);
                    
                    _databaseConnection.Close();
                    break;
                }
                case "SQLiteAdapter":
                {
                    _databaseConnection.Open();
                    var sql = "";

                    sql = """
                          CREATE TABLE IF NOT EXISTS users(
                          id varchar NOT NULL,
                          name varchar(100) NOT NULL,
                          document varchar(11) NOT NULL,
                          email varchar(50) NOT NULL,
                          password varchar(50) NOT NULL,
                          type int NOT NULL,
                          PRIMARY KEY (id)
                          );
                          """;
                    _databaseConnection.Command(sql);

                    sql = """
                          CREATE TABLE IF NOT EXISTS wallets(
                          userid varchar NOT NULL,
                          balance decimal NOT NULL,
                          lasttransactiondate timestamp,
                          PRIMARY KEY (userid)
                          );
                          """;
                    _databaseConnection.Command(sql);

                    sql = """
                          CREATE TABLE IF NOT EXISTS transactions(
                          id varchar NOT NULL,
                          value decimal NOT NULL,
                          sender varchar NOT NULL,
                          receiver varchar NOT NULL,
                          date timestamp NOT NULL
                          );
                          """;
                    _databaseConnection.Command(sql);

                    _databaseConnection.Close();
                    
                    break;
                }
                default:
                {
                    throw new Exception("Erro ao tentar executar o DDL do banco");
                    break;
                }
            }
        }
    }
}