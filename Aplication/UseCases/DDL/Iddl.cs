using SimpleBanking.Infra.Database;

namespace SimpleBanking.Aplication
{
    public class Iddl : IDDL
    {
        private readonly DDL _ddl;

        public Iddl(DDL ddl)
        {
            _ddl = ddl;
        }

        public void Execute()
        {
            _ddl.Execute();
        }
    }
}