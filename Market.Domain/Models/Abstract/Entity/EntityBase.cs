namespace Market.Domain.Abstract.Entity
{
    public abstract class EntityBase
    {        
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
