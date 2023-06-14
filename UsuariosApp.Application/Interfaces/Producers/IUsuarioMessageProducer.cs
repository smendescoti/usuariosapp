using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Application.Models.Producers;

namespace UsuariosApp.Application.Interfaces.Producers
{
    public interface IUsuarioMessageProducer
    {
        void Send(UsuarioMessageDTO dto);
    }
}
