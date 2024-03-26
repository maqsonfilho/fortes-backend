namespace Fortes.Web.Challenge.Domain.Models.Base;

public class ModelBase
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ModelBase()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public ModelBase(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }
}
