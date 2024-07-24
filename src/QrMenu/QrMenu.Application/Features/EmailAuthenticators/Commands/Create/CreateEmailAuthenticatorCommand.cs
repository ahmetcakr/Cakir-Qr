

using AutoMapper;
using Core.Security.Entities;
using QrMenu.Application.Repositories;
using MediatR;

namespace QrMenu.Application.Features.EmailAuthenticators.Commands.Create;

public class CreateEmailAuthenticatorCommand : IRequest<CreatedEmailAuthenticatorResponse>
{
    public int UserId { get; set; }

    public CreateEmailAuthenticatorCommand()
    {
        UserId = 0;
    }

    public CreateEmailAuthenticatorCommand(int userId)
    {
        UserId = userId;
    }

    public class CreateEmailAuthenticatorCommandHandler(
        IMapper mapper,
        IEmailAuthenticatorRepository _repository) : IRequestHandler<CreateEmailAuthenticatorCommand, CreatedEmailAuthenticatorResponse>
    {
        public async Task<CreatedEmailAuthenticatorResponse> Handle(CreateEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
           if ( await  _repository
                .AnyAsync(ea => ea.UserId == request.UserId) )
            {
                throw new Exception("Email Authenticator already exists");
            }

           EmailAuthenticator emailAuthenticator = new()
           {
                UserId = request.UserId,
                ActivationKey = Guid.NewGuid().ToString(),
                IsVerified = false
           };

            await _repository.AddAsync(emailAuthenticator);

            var mappedEmailAuth = mapper.Map<EmailAuthenticator, CreatedEmailAuthenticatorResponse>(emailAuthenticator);

            return mappedEmailAuth;
        }
    }
}
