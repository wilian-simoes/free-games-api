using FreeGames.Application.DTOs.Requests;
using FreeGames.Application.DTOs.Responses;

namespace FreeGames.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastro);
        Task<UsuarioLoginResponse> Login(UsuarioLoginRequest usuarioLogin);
        Task<UsuarioLoginResponse> LoginSemSenha(string usuarioId);
    }
}