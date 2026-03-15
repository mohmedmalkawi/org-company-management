using Application.Abstractions.Data;
using Domain.Companies;
using Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Organizations.Delete;

internal sealed class OrganizationDeletedDomainEventHandler(IApplicationDbContext context)
    : IDomainEventHandler<OrganizationDeletedDomainEvent>
{
    public async Task Handle(OrganizationDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        List<Company> relatedCompanies = await context.Companies
            .Where(c => c.OrganizationId == domainEvent.OrganizationId)
            .ToListAsync(cancellationToken);

        context.Companies.RemoveRange(relatedCompanies);
    }
}
