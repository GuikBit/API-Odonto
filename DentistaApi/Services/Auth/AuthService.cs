using DentistaApi.Data;
using DentistaApi.Models;
using Microsoft.EntityFrameworkCore;
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
       User usuario = FindByUser(user);

       // int orgId = usuario.IdOrganizacao.Id;

        int orgId2 = usuario.OrganizacaoId;

        if (usuario == null)
            return new Return<string>(EReturnStatus.Error, null,
                "Login não existe.", null);

        if (!ValidaSenha(usuario, user))
            return new Return<string>(EReturnStatus.Error, null,
                "Senha inválida.", null);

        string token = GenerateToken(usuario);

        var organizacao = db.Organizacao.FirstOrDefault(x => x.Id == orgId2);

        return new Return<string>(EReturnStatus.Success, usuario, token, organizacao);
    }

    private dynamic FindByUser(UserInfo user)
    {
        var paciente = db.Pacientes.FirstOrDefault(x => x.Login == user.Login);
        var dentista = db.Dentistas.FirstOrDefault(x => x.Login == user.Login);
        var administrador = db.Administrador.FirstOrDefault(x => x.Login == user.Login);

        if (paciente != null)
            return paciente;
        else if (dentista != null)
            return dentista;
        else if (administrador != null)
            return administrador;
        else
            return null;
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
        public Return(EReturnStatus status, User usuario, T result, Organizacao org)
        {
            Status = status;
            Result = result;
            Usuario = usuario;
            Organizacao = org;
        }

        public EReturnStatus Status { get; private set; }
        public T Result { get; private set; }
        public User Usuario { get; private set; }
        public Organizacao Organizacao { get; private set; }

}
}
