using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using QrMenu.Application.Repositories;

namespace QrMenu.Application.Features.OperationClaims.Rules;

public class OperationClaimBusinessRules : BaseBusinessRules
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }

    public Task OperationClaimShouldExistWhenSelected(OperationClaim? operationClaim)
    {
        if (operationClaim == null)
            throw new BusinessException("Claims doesnt exist.");
        return Task.CompletedTask;
    }

    public async Task OperationClaimIdShouldExistWhenSelected(int id)
    {
        bool doesExist = await _operationClaimRepository.AnyAsync(predicate: b => b.Id == id, enableTracking: false);
        if (doesExist)
            throw new BusinessException("Claims doesnt exist.");
    }

    public async Task OperationClaimNameShouldNotExistWhenCreating(string name)
    {
        bool doesExist = await _operationClaimRepository.AnyAsync(predicate: b => b.Name == name, enableTracking: false);
        if (doesExist)
            throw new BusinessException(message: "Claims already exists.");
    }

    public async Task OperationClaimNameShouldNotExistWhenUpdating(int id, string name)
    {
        bool doesExist = await _operationClaimRepository.AnyAsync(predicate: b => b.Id != id && b.Name == name, enableTracking: false);
        if (doesExist)
            throw new BusinessException(message: "Claims already exists.");
    }
}
