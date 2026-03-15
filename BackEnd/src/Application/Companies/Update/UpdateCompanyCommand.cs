using Application.Abstractions.Messaging;

namespace Application.Companies.Update;

public sealed record UpdateCompanyCommand(
    Guid CompanyId,
    Guid OrganizationId,
    string Name,
    string Code,
    string Country,
    string Phone,
    string FullAddress) : ICommand;
