using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Organizations;
using SharedKernel;

namespace Application.Organizations.Create;

internal sealed class CreateOrganizationCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateOrganizationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateOrganizationCommand command, CancellationToken cancellationToken)
    {
        var organization = new Organization
        {
            Name = command.Name.Trim(),
            Code = command.Code.Trim(),
            Country = command.Country.Trim(),
            Phone = command.Phone.Trim(),
            FullAddress = command.FullAddress.Trim(),
            CreationDate = dateTimeProvider.UtcNow,
            UpdatingDate = null
        };

        context.Organizations.Add(organization);

        return organization.Id;
    }
}
