using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Application.Helpers;
using UsuariosApp.Application.Interfaces.Identities;
using UsuariosApp.Application.Interfaces.Producers;
using UsuariosApp.Application.Interfaces.Services;
using UsuariosApp.Application.Models.Producers;
using UsuariosApp.Application.Models.Requests;
using UsuariosApp.Application.Models.Responses;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Models;

namespace UsuariosApp.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioDomainService? _usuarioDomainService;
        private readonly IUsuarioMessageProducer? _usuarioMessageProducer;
        private readonly ITokenCreator? _tokenCreator;
        private readonly IMapper? _mapper;

        public UsuarioAppService(IUsuarioDomainService? usuarioDomainService, IUsuarioMessageProducer? usuarioMessageProducer, ITokenCreator? tokenCreator, IMapper? mapper)
        {
            _usuarioDomainService = usuarioDomainService;
            _usuarioMessageProducer = usuarioMessageProducer;
            _tokenCreator = tokenCreator;
            _mapper = mapper;
        }

        public AutenticarResponseDTO Autenticar(AutenticarRequestDTO dto)
        {
            var usuario = _usuarioDomainService?.Autenticar(dto.Email, Sha1Helper.Encrypt(dto.Senha));
            var response = _mapper.Map<AutenticarResponseDTO>(usuario);

            response.AccessToken = _tokenCreator.Create(usuario.Email, "USER_ROLE");
            response.DataHoraExpiracao = DateTime.UtcNow.AddHours(1);

            return response;
        }

        public CriarContaResponseDTO CriarConta(CriarContaRequestDTO dto)
        {   
            var usuario = _mapper?.Map<Usuario>(dto);
            _usuarioDomainService?.CriarConta(usuario);

            #region Gerar notificação para envio de mensagem de boas vindas 

            var usuarioMessageDTO = new UsuarioMessageDTO
            {
                Tipo = TipoMensagem.CriacaoDeConta,
                DataHora = DateTime.UtcNow,
                IdUsuario = usuario.Id,
                NomeUsuario = usuario.Nome,
                EmailUsuario = usuario.Email
            };

            _usuarioMessageProducer?.Send(usuarioMessageDTO);

            #endregion

            return _mapper.Map<CriarContaResponseDTO>(usuario);
        }        

        public RecuperarSenhaResponseDTO RecuperarSenha(RecuperarSenhaRequestDTO dto)
        {
            var usuario = _usuarioDomainService.RecuperarSenha(dto.Email);
            return _mapper.Map<RecuperarSenhaResponseDTO>(usuario);
        }

        public void Dispose()
        {
            _usuarioDomainService?.Dispose();
        }
    }
}
