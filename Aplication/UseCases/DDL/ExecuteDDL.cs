using SimpleBanking.Infra.Database;

namespace SimpleBanking.Aplication
{
    public class ExecuteDDL
    {
        public static void Execute(){
            var databaseConnection = new SQLiteAdapter();
            DDL.Execute(databaseConnection);
        }
    }
}