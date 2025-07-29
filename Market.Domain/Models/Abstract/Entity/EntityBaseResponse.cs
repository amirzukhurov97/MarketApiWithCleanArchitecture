namespace Market.Domain.Abstract.Entity
{
    public abstract record EntityBaseResponse
    {
        public Guid Id { get; set; }
    }
}
