using Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Models.Sources
{
    public class BaseSource
    {
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public SourceTypes Type { get; set; }
    }
}
