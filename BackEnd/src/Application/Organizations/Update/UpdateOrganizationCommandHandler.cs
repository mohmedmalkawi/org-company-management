using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Organizations.Update;

internal sealed class UpdateOrganizationCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<UpdateOrganizationCommand>
{
    public async Task<Result> Handle(UpdateOrganizationCommand command, CancellationToken cancellationToken)
    {
        Organization? organization = await context.Organizations
            .SingleOrDefaultAsync(o => o.Id == command.OrganizationId, cancellationToken);

        if (organization is null)
        {
            return Result.Failure(OrganizationErrors.NotFound(command.OrganizationId));
        }

        organization.Name = command.Name.Trim();
        organization.Code = command.Code.Trim();
        organization.Country = command.Country.Trim();
        organization.Phone = command.Phone.Trim();
        organization.FullAddress = command.FullAddress.Trim();
        organization.UpdatingDate = dateTimeProvider.UtcNow;

        return Result.Success();
    }
}
