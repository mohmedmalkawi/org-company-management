using Application.Abstractions.Data;
using Domain.Companies;
using Domain.Organizations;
using Microsoft.EntityFrameworkCore;

namespace Application.Companies.Search;

public class SearchCompanyQueryBuilder
{
    private IQueryable<CompanyWithOrganization> _query;

    public IQueryable<CompanyWithOrganization> Build() => _query;

    public SearchCompanyQueryBuilder(IApplicationDbContext context)
    {
        _query = context.Companies.AsNoTracking()
            .Join(context.Organizations.AsNoTracking(),
                  c => c.OrganizationId,
                  o => o.Id,
                  (c, o) => new CompanyWithOrganization { Company = c, Organization = o });
    }

    public SearchCompanyQueryBuilder FilterByOrganizationName(string? name)
    {
        name = Normalize(name);

        if (!string.IsNullOrWhiteSpace(name))
        {
            _query = _query.Where(x => x.Organization.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        return this;
    }

    public SearchCompanyQueryBuilder FilterByCompanyName(string? name)
    {
        name = Normalize(name);

        if (!string.IsNullOrWhiteSpace(name))
        {
            _query = _query.Where(x => x.Company.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        return this;
    }

    public SearchCompanyQueryBuilder FilterByCountry(string? country)
    {
        country = Normalize(country);

        if (!string.IsNullOrWhiteSpace(country))
        {
            _query = _query.Where(x => x.Company.Country.Equals(country, StringComparison.OrdinalIgnoreCase));
        }

        return this;
    }

    public SearchCompanyQueryBuilder FilterByCompanyCode(string? code)
    {
        code = Normalize(code);

        if (!string.IsNullOrWhiteSpace(code))
        {
            _query = _query.Where(x => x.Company.Code.Contains(code, StringComparison.OrdinalIgnoreCase));
        }

        return this;
    }

    public SearchCompanyQueryBuilder Paginate(int? page, int? pageSize)
    {
        if (page > 0 && pageSize > 0)
        {
            _query = _query.Skip((page.Value - 1) * pageSize.Value)
                           .Take(pageSize.Value);
        }

        return this;
    }

    private static string? Normalize(string? value) => value?.Trim();
}

public class CompanyWithOrganization
{
    public Company Company { get; set; } = default!;
    public Organization Organization { get; set; } = default!;
}
