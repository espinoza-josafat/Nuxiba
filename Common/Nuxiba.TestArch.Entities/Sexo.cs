using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nuxiba.TestArch.Entities
{
    [Table("Sexo")]
    public partial class Sexo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("id")]
        public virtual byte Id { get; set; }

        [JsonProperty("descripcion")]
        public virtual string Descripcion { get; set; }
    }
}