using Mapster;
using SpaceCafe.Application.Authentication.Commands.Register;
using SpaceCafe.Application.Authentication.Common;
using SpaceCafe.Application.Authentication.Queries.Login;
using SpaceCafe.Contracts.Authentication;

namespace SpaceCafe.WebAPI.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.user);
    }
}
