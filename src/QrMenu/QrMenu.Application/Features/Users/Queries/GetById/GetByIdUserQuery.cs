using QrMenu.Application.Features.Users.Rules;
using QrMenu.Application.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using Core.Application.Results;

namespace QrMenu.Application.Features.Users.Queries.GetById;

public class GetByIdUserQuery : IRequest<Result<GetByIdUserResponse>>
{
    public int Id { get; set; }

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, Result<GetByIdUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public GetByIdUserQueryHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<Result<GetByIdUserResponse>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            GetByIdUserResponse response = _mapper.Map<GetByIdUserResponse>(user);
            return Result<GetByIdUserResponse>.Succeed(response);
        }
    }
}
