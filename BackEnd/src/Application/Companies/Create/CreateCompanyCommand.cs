using Application.Abstractions.Messaging;

namespace Application.Companies.Create;

public sealed record CreateCompanyCommand(
    Guid OrganizationId,
    string Name,
    string Code,
    string Country,
    string Phone,
    string FullAddress) : ICommand<Guid>;
