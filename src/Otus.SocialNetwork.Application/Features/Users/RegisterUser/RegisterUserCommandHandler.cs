using MediatR;
using Otus.SocialNetwork.Application.Exceptions;
using Otus.SocialNetwork.Domain;
using Otus.SocialNetwork.Persistence.Abstranctions;

namespace Otus.SocialNetwork.Application.Features.Users.RegisterUser;

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IUserExistsQueryObject _userExistsQueryObject;
    private readonly ISaveUserQueryObject _saveUserQueryObject;
    private readonly IPasswordHashCalculator _passwordHashCalculator;

    public RegisterUserCommandHandler(
        IUserExistsQueryObject userExistsQueryObject,
        ISaveUserQueryObject saveUserQueryObject,
        IPasswordHashCalculator passwordHashCalculator)
    {
        _userExistsQueryObject = userExistsQueryObject;
        _saveUserQueryObject = saveUserQueryObject;
        _passwordHashCalculator = passwordHashCalculator;
    }

    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        if (await _userExistsQueryObject.ExistsByUsernameAsync(request.Username, ct))
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
            request.City,
            _passwordHashCalculator.CalculateHash(request.Password));

        await _saveUserQueryObject.SaveAsync(user, ct);

        return Unit.Value;
    }
}