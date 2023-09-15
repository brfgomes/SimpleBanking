namespace SimpleBanking.Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set;}

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}