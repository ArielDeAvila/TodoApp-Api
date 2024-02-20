using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Todo.Server.Data.Entities;
using Todo.Server.DTO;
using Todo.Server.Services.Interfaces;
using Todo.Server.Tools;
using Todo.Server.UnitOfWork;
using BC = BCrypt.Net.BCrypt;

namespace Todo.Server.Services;

public class LoginService : ILoginService
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly IValidator<LoginRequestDto> _validator;
    private readonly IConfiguration _configuration;

    public LoginService(
        IUnitOfWork unitOfWork, IValidator<LoginRequestDto> validator, IConfiguration configuration
    )
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<BaseResponse<string>> Login(LoginRequestDto request)
    {
        var response = new BaseResponse<string>();
        var validate = await _validator.ValidateAsync(request);

        if (!validate.IsValid)
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_VALIDATE;
            response.Errors = validate.Errors;

            return response;
        }

        var user = await _unitOfWork.UserRepository.GetUserByEmail(request.Email!);

        if(user is not null && BC.Verify(request.Password, user.Password))
        {
            response.Success = true;
            response.Data = GenerateToken(user);
            response.Message = ReplyMessage.MESSAGE_TOKEN;
        }
        else
        {
            response.Success = false;
            response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;
        }


        return response;
    }

    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.FamilyName, user.UserName),
            new(JwtRegisteredClaimNames.GivenName, user.Email),
            new(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:Expires"]!)),
                notBefore: DateTime.UtcNow,
                signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
