using Newtonsoft.Json;

namespace Nuxiba.TestArch.Entities.Views
{
    public class VWUsuario
    {
        [JsonProperty("id")]
        public short Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("correo_electronico")]
        public string CorreoElectronico { get; set; }

        [JsonProperty("sexo")]
        public string Sexo { get; set; }

        [JsonProperty("estatus")]
        public string Estatus { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }
    }
}