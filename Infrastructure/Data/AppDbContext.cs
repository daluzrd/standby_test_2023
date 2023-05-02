using Microsoft.EntityFrameworkCore;
using SharedKernel.Commands;
using SharedKernel.Events;
using SharedKernel.MediatR;
using SharedKernel.Model;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IMediatorHandler _mediator;
        public AppDbContext(DbContextOptions<AppDbContext> options, IMediatorHandler mediator)
         : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<CommandResult>();
            modelBuilder.Ignore<DomainEvent>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetMaxLength(256);
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                      .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            var entries = ChangeTracker
              .Entries()
              .Where(e => e.Entity is Entity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((Entity)entityEntry.Entity).Updated = DateTime.Now;


                if (entityEntry.State == EntityState.Added)
                {
                    ((Entity)entityEntry.Entity).Created = DateTime.Now;
                }
            }



            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            var sucess = result > 0;

            if (_mediator == null) return result;

            if (sucess) await PublishEvents().ConfigureAwait(false);

            return result;
        }


        private async Task PublishEvents()
        {
            var entitiesWithEvents = ChangeTracker.Entries<Entity>()
               .Select(e => e.Entity.Events)
               .Where(e => e.Any())
               .ToArray();

            foreach (var entityevents in entitiesWithEvents)
            {
                var events = entityevents.ToArray();
                entityevents.Clear();
                foreach (var domainEvent in events)
                {
                    await _mediator.PublishEvent(domainEvent).ConfigureAwait(false);
                }
            }
        }

    }
}