namespace OnlineStore.Interfaces.Entities
{
    public interface INamedEntity : IEntity
    {
        string? Name { get; set; }
    }
}
