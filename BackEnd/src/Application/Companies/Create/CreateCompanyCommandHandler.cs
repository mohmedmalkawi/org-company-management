using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Companies;
using Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Companies.Create;

internal sealed class CreateCompanyCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateCompanyCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        bool organizationExists = await context.Organizations
            .AnyAsync(o => o.Id == command.OrganizationId, cancellationToken);

        if (!organizationExists)
        {
            return Result.Failure<Guid>(OrganizationErrors.NotFound(command.OrganizationId));
        }

        var company = new Company
        {
            OrganizationId = command.OrganizationId,
            Name = command.Name.Trim(),
            Code = command.Code.Trim(),
            Country = command.Country.Trim(),
            Phone = command.Phone.Trim(),
            FullAddress = command.FullAddress.Trim(),
            CreationDate = dateTimeProvider.UtcNow,
            UpdatingDate = null
        };

        context.Companies.Add(company);

        return company.Id;
    }
}
