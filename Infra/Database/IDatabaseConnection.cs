using System.Data.Common;

namespace SimpleBanking.Aplication.Database
{
    public interface IDatabaseConnection
    {
        public void Open();
        public void Close();
        public void Command(string sql, Dictionary<string, object> parameters = null);
        public DbDataReader Query(string sql, Dictionary<string, object> parameters = null);   
    }
}