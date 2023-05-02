using SharedKernel.Events;

namespace SharedKernel.Model
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            Events = new List<DomainEvent>();
        }
        public List<DomainEvent> Events { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}