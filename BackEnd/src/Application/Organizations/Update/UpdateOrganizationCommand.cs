using Application.Abstractions.Messaging;

namespace Application.Organizations.Update;

public sealed record UpdateOrganizationCommand(
    Guid OrganizationId,
    string Name,
    string Code,
    string Country,
    string Phone,
    string FullAddress) : ICommand;
