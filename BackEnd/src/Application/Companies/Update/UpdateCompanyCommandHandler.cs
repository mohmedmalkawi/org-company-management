using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Companies;
using Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Companies.Update;

internal sealed class UpdateCompanyCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<UpdateCompanyCommand>
{
    public async Task<Result> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        Company? company = await context.Companies
            .SingleOrDefaultAsync(c => c.Id == command.CompanyId, cancellationToken);

        if (company is null)
        {
            return Result.Failure(CompanyErrors.NotFound(command.CompanyId));
        }

        bool organizationExists = await context.Organizations
            .AnyAsync(o => o.Id == command.OrganizationId, cancellationToken);

        if (!organizationExists)
        {
            return Result.Failure(OrganizationErrors.NotFound(command.OrganizationId));
        }

        company.OrganizationId = command.OrganizationId;
        company.Name = command.Name.Trim();
        company.Code = command.Code.Trim();
        company.Country = command.Country.Trim();
        company.Phone = command.Phone.Trim();
        company.FullAddress = command.FullAddress.Trim();
        company.UpdatingDate = dateTimeProvider.UtcNow;

        return Result.Success();
    }
}
