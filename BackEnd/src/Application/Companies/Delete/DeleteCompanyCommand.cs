using Application.Abstractions.Messaging;

namespace Application.Companies.Delete;

public sealed record DeleteCompanyCommand(Guid CompanyId) : ICommand;
