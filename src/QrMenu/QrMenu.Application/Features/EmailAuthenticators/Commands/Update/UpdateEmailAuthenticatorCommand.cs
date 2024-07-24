using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using QrMenu.Application.Repositories;
using MediatR;

namespace QrMenu.Application.Features.EmailAuthenticators.Commands.Update;

public class UpdateEmailAuthenticatorCommand : IRequest<UpdatedEmailAuthenticatorResponse>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ActivationKey { get; set; }

    public UpdateEmailAuthenticatorCommand()
    {
        Id = 0;
        UserId = 0;
        ActivationKey = string.Empty;
    }

    public UpdateEmailAuthenticatorCommand(int id, int userId, string activationKey)
    {
        Id = id;
        UserId = userId;
        ActivationKey = activationKey;
    }
}

public class UpdateEmailAuthenticatorCommandHandler(
    IMapper mapper,
    IEmailAuthenticatorRepository _repository) : IRequestHandler<UpdateEmailAuthenticatorCommand, UpdatedEmailAuthenticatorResponse>
{
    public async Task<UpdatedEmailAuthenticatorResponse> Handle(UpdateEmailAuthenticatorCommand request, CancellationToken cancellationToken)
    {
        var emailAuthenticator = await _repository.GetByUserIdAsync(request.UserId);

        if (emailAuthenticator == null)
            throw new NotFoundException(nameof(EmailAuthenticator));

        if (emailAuthenticator.ActivationKey != request.ActivationKey)
            throw new Exception("Activation key is not valid.");

        emailAuthenticator.ActivationKey = request.ActivationKey;
        emailAuthenticator.IsVerified = true;

        await _repository.UpdateAsync(emailAuthenticator);

        return mapper.Map<UpdatedEmailAuthenticatorResponse>(emailAuthenticator);
    }
}
