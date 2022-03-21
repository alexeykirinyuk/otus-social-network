using Otus.SocialNetwork.Application.Exceptions;
using Otus.SocialNetwork.Application.Libs;
using Otus.SocialNetwork.Domain;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Application.Features.Users.RegisterUser;

public sealed class RegisterUserUseCase : IUseCase<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task Handle(RegisterUserCommand request, CancellationToken ct)
    {
        if (await _userRepository.Exists(request.Username, ct))
        {
            throw new UsernameAlreadyExistsException(request.Username);
        }
        
        var user = User.RegisterNew(
            request.Username,
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Sex,
            request.Interests,
            request.City);

        await _userRepository.Save(user, ct);
    }
}
