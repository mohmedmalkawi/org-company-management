using Application.Abstractions.Messaging;

namespace Application.Companies.GetById;

public sealed record GetCompanyByIdQuery(Guid CompanyId) : IQuery<CompanyByIdResponse>;
