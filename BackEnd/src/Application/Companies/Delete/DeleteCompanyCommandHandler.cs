using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Companies;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Companies.Delete;

internal sealed class DeleteCompanyCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteCompanyCommand>
{
    public async Task<Result> Handle(DeleteCompanyCommand command, CancellationToken cancellationToken)
    {
        Company? company = await context.Companies
            .SingleOrDefaultAsync(c => c.Id == command.CompanyId, cancellationToken);

        if (company is null)
        {
            return Result.Failure(CompanyErrors.NotFound(command.CompanyId));
        }

        context.Companies.Remove(company);

        return Result.Success();
    }
}
