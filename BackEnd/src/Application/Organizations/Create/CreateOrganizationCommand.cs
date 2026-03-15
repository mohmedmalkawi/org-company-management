using Application.Abstractions.Messaging;

namespace Application.Organizations.Create;

public sealed record CreateOrganizationCommand(
    string Name,
    string Code,
    string Country,
    string Phone,
    string FullAddress) : ICommand<Guid>;
