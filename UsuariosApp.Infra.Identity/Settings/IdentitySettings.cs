using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Infra.Identity.Settings
{
    public class IdentitySettings
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? SecretKey { get; set; }
    }
}
