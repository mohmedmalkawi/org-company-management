using Application.Abstractions.Messaging;

namespace Application.Companies.Search;

public sealed record SearchCompaniesQuery(
    string? OrganizationName,
    string? CompanyName,
    string? Country,
    string? CompanyCode,
    int? Page,
    int? PageSize) : IQuery<List<CompanySearchResponse>>;
