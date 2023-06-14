using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace UsuariosApp.API.Models
{
    public class ErrorViewModel
    {
        public int? StatusCode { get; set; }
        public string? Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
