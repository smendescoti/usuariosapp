using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.Application.Interfaces.Services;
using UsuariosApp.Application.Models.Requests;
using UsuariosApp.Application.Models.Responses;

namespace UsuariosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioAppService? _usuarioAppService;

        public UsuariosController(IUsuarioAppService? usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        /// <summary>
        /// Autenticação de usuários
        /// </summary>
        [HttpPost]
        [Route("autenticar")]
        [ProducesResponseType(typeof(AutenticarResponseDTO), StatusCodes.Status200OK)]
        public IActionResult Autenticar(AutenticarRequestDTO dto)
        {
            return StatusCode(200, _usuarioAppService?.Autenticar(dto));
        }

        /// <summary>
        /// Criação de conta de usuários
        /// </summary>
        [HttpPost]
        [Route("criar-conta")]
        [ProducesResponseType(typeof(CriarContaResponseDTO), StatusCodes.Status201Created)]
        public IActionResult CriarConta(CriarContaRequestDTO dto) 
        {
            return StatusCode(201, _usuarioAppService?.CriarConta(dto));
        }

        /// <summary>
        /// Recuperação de senha do usuário
        /// </summary>
        [HttpPost]
        [Route("recuperar-senha")]
        [ProducesResponseType(typeof(RecuperarSenhaResponseDTO), StatusCodes.Status200OK)]
        public IActionResult RecuperarSenha(RecuperarSenhaRequestDTO dto)
        {
            return StatusCode(200, _usuarioAppService?.RecuperarSenha(dto));
        }
    }
}
