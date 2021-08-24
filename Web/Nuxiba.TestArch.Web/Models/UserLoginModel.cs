using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Nuxiba.TestArch.Web.Models
{
    public class UserLoginModel
    {
        [Display(Name = "Usuario")]
        [JsonProperty(PropertyName = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "Contraseña")]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}