using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Exceptions.Usuarios
{
    public class AcessoNegadoException : Exception
    {
        public override string Message 
            => "Acesso negado. Usuário inválido.";
    }
}
