using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Exceptions.Usuarios;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Models;

namespace UsuariosApp.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        private readonly IUnitOfWork? _unitOfWork;

        public UsuarioDomainService(IUnitOfWork? unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CriarConta(Usuario usuario)
        {
            if (_unitOfWork?.UsuarioRepository?.Get(u => u.Email.Equals(usuario.Email)) != null)
                throw new EmailJaCadastradoException();

            _unitOfWork?.UsuarioRepository?.Add(usuario);
            _unitOfWork?.SaveChanges();
        }        

        public Usuario Autenticar(string email, string senha)
        {
            var usuario = _unitOfWork?.UsuarioRepository?.Get
                (u => u.Email.Equals(email) && u.Senha.Equals(senha));

            if (usuario == null)
                throw new AcessoNegadoException();

            //TODO
            return usuario;
        }

        public Usuario RecuperarSenha(string email)
        {
            var usuario = _unitOfWork?.UsuarioRepository?.Get(u => u.Email.Equals(email));

            if (usuario == null)
                throw new UsuarioNaoEncontradoException();

            //TODO
            return usuario;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
