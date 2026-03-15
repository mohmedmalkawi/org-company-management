using Application.Abstractions.Data;
using Domain.Organizations;
using Microsoft.EntityFrameworkCore;

namespace Application.Organizations.Search;

internal sealed class SearchOrganizationQueryBuilder(IApplicationDbContext context)
{
    private IQueryable<Organization> _query = context.Organizations.AsNoTracking();

    public IQueryable<Organization> Build() => _query;

    public SearchOrganizationQueryBuilder FilterByName(string? name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            name = Normalize(name);
            _query = _query.Where(o => o.Name.Contains(name!, StringComparison.OrdinalIgnoreCase));
        }
        return this;
    }

    public SearchOrganizationQueryBuilder FilterByCode(string? code)
    {
        if (!string.IsNullOrWhiteSpace(code))
        {
            code = Normalize(code);
            _query = _query.Where(o => o.Code.Contains(code!, StringComparison.OrdinalIgnoreCase));
        }
        return this;
    }

    public SearchOrganizationQueryBuilder FilterByCountry(string? country)
    {
        if (!string.IsNullOrWhiteSpace(country))
        {
            country = Normalize(country);
            _query = _query.Where(o => o.Country.Contains(country!, StringComparison.OrdinalIgnoreCase));
        }
        return this;
    }

    public SearchOrganizationQueryBuilder ApplyPaging(int? page, int? pageSize)
    {
        if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
        {
            _query = _query.Skip((page.Value - 1) * pageSize.Value)
                         .Take(pageSize.Value);
        }
        return this;
    }

    private static string? Normalize(string? value) => value?.Trim();
}
