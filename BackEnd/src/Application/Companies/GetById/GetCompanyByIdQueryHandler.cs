using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Companies;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Companies.GetById;

internal sealed class GetCompanyByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetCompanyByIdQuery, CompanyByIdResponse>
{
    public async Task<Result<CompanyByIdResponse>> Handle(
        GetCompanyByIdQuery query,
        CancellationToken cancellationToken)
    {
        CompanyByIdResponse? company = await context.Companies
            .AsNoTracking()
            .Where(c => c.Id == query.CompanyId)
            .Select(c => new CompanyByIdResponse
            {
                Id = c.Id,
                OrganizationId = c.OrganizationId,
                Name = c.Name,
                Code = c.Code,
                Country = c.Country,
                Phone = c.Phone,
                FullAddress = c.FullAddress,
                CreationDate = c.CreationDate,
                UpdatingDate = c.UpdatingDate
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (company is null)
        {
            return Result.Failure<CompanyByIdResponse>(CompanyErrors.NotFound(query.CompanyId));
        }

        return company;
    }
}
