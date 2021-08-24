using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nuxiba.TestArch.Entities
{
    [Table("Tarea")]
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("fecha_creacion")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [JsonProperty("estatus")]
        public bool Estatus { get; set; }


        [NotMapped]
        [JsonProperty("estatus_descripcion")]
        public string EstatusDescripcion 
        { 
            get
            {
                return Estatus ? "Completado" : "Pendiente";
            }
        }
    }
}
