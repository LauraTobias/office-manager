namespace OfficeManager.Domain.Entities.EntityBases
{
    public abstract class EntityBase : ErrorBase
    {
        public Guid Id { get; protected set; }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
