using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Application.Interfaces.Identities
{
    public interface ITokenCreator
    {
        string Create(string userName, string userRole);
    }
}
