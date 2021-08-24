using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nuxiba.TestArch.Entities
{
    [Table("Usuario")]
    public partial class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public virtual short Id { get; set; }

        [JsonProperty("correo_electronico")]
        public virtual string CorreoElectronico { get; set; }

        [JsonProperty("username")]
        public virtual string Username { get; set; }

        [JsonProperty("password")]
        public virtual string Password { get; set; }

        [JsonProperty("estatus")]
        public virtual bool Estatus { get; set; }

        [JsonProperty("sexo")]
        public virtual byte Sexo { get; set; }

        [JsonProperty("fecha_creacion")]
        public virtual DateTime FechaCreacion { get; set; }

        [JsonProperty("nombre")]
        public virtual string Nombre { get; set; } = "";
    }
}