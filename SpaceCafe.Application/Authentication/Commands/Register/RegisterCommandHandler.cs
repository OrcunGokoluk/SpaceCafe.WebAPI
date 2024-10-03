using MediatR;
using OneOf;
using SpaceCafe.Application.Authentication.Common;
using SpaceCafe.Application.Common.CustomException;
using SpaceCafe.Application.Common.Interfaces.Authentication;
using SpaceCafe.Application.Common.Interfaces.Persistance;
using SpaceCafe.Domain.Entities;

namespace SpaceCafe.Application.Authentication.Commands.Register;
public class RegisterCommandHandler(IJwtTokenGenerator _jwtTokenGenerator, IUserRepository _userRepository) :
    IRequestHandler<RegisterCommand, OneOf<AuthenticationResult, DuplicateEmailError, CustomException>>
{

    public async Task<OneOf<AuthenticationResult, DuplicateEmailError, CustomException>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        //1.Validate the user doesn't exist
        var existingUser = await _userRepository.GetUserByEmail(command.Email); // await eklenmeli
        if (existingUser is not null)
        {
            return new DuplicateEmailError();
        }
        //2.Create user (generate unique ID) & Persist to Db
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password,
        };

        await _userRepository.Add(user);


        //3.Generate JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);



        return new AuthenticationResult(

            user,
            //user.Id,
            //firstname,
            //lastName,
            //email,
            token);
    }
}
