using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DentistaApi.Services;

public class AuthService : IAuthService{
    public AuthService( IConfiguration configuration)
    {

        this.configuration = configuration;
    }

    public async Task<IAuthService.IReturn<string>> Login(UserInfo user)
    {
        User? usuario = FindByUser(user);        

        if (usuario == null)
            return new Return<string>(EReturnStatus.Error, null,
                "Login não existe.");

        if (!ValidaSenha(usuario, user))
            return new Return<string>(EReturnStatus.Error, null,
                "Senha inválida.");

        string token = GenerateToken(usuario);

        return new Return<string>(EReturnStatus.Success, usuario, token);
    }

    private  User FindByUser(UserInfo user)
    {
        User? usuario = db.Pacientes.FirstOrDefault(x => x.Login == user.Login);

        if (usuario == null)
        {
            usuario =  db.Dentistas.FirstOrDefault(x => x.Login == user.Login);
        }
        if (usuario == null)
        {
            usuario =  db.Administrador.FirstOrDefault(x => x.Login == user.Login);
        }        
        return usuario;
    }
    private bool ValidaSenha(User user, UserInfo userInfo)
    {
        if (user.Senha == userInfo.GerarHash())
            return true;
        return false;

    }

    private string GenerateToken(User usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new[]
        {
            new Claim("Nome", usuario.Nome),
            new Claim("Id", usuario.Id.ToString()),
            new Claim("Role", usuario.Role),
            
        };
        var token = tokenHandler.CreateToken(GetTokenDescriptor(claims));
        return tokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor GetTokenDescriptor(IEnumerable<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["JWT:Secret"] ?? "")
        );
        return new SecurityTokenDescriptor
        {
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"],
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials = new SigningCredentials(authSigningKey,
                                            SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims)
        };
    }

    private readonly AppDbContext db = new();
    private readonly IConfiguration configuration;
    public class Return<T> : IAuthService.IReturn<T>
    {
        public Return(EReturnStatus status, User usuario, T result)
        {
            Status = status;
            Result = result;
            Usuario = usuario;
        }

        public EReturnStatus Status { get; private set; }
        public T Result { get; private set; }
        public User Usuario { get; private set; }

        
    }
}
