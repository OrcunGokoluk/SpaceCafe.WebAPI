using MediatR;
using SpaceCafe.Application.Authentication.Common;
using SpaceCafe.Application.Common.CustomExceptions;
using SpaceCafe.Application.Common.Interfaces.Authentication;
using SpaceCafe.Application.Common.Interfaces.Persistance;

namespace SpaceCafe.Application.Authentication.Queries.Login;
public class LoginQueryHandler(IJwtTokenGenerator _jwtTokenGenerator, IUserRepository _userRepository) :
    IRequestHandler<LoginQuery, AuthenticationResult>
{
    public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(query.Email);

        if (user is null)
        {
            //"User with given email does not exist."
            throw new CustomException("Invalid Email");
        }
        //2.Validate the password is correnct
        if (user?.Password != query.Password)
        {
            //"Invalid password."
            throw new CustomException("Invalid Password");
        }
        //3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            //user.Id,
            //user.FirstName,
            //user.LastName,
            //email,
            token);
    }
}
