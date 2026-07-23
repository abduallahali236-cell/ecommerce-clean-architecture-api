using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ECommerce.Infrastructure.Persistence.Interceptors;

public sealed class AuditableEntityInterceptor
    : SaveChangesInterceptor
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public AuditableEntityInterceptor(
        IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateAuditableEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities(eventData.Context);

        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }

    private void UpdateAuditableEntities(DbContext? context)
    {
        if (context is null)
            return;

        var utcNow = _dateTimeProvider.UtcNow;

        foreach (var entry in context.ChangeTracker
                     .Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetCreated(utcNow);
                    break;

                case EntityState.Modified:
                    entry.Entity.SetUpdated(utcNow);
                    break;
            }
        }
    }
}