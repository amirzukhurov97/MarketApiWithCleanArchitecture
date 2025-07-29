namespace Market.Domain.Abstract.Entity
{
    public abstract record EntityBaseUpdateRequest
    {
        public Guid Id { get; set; }
    }
}
