using Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Models.Sources
{
    public interface IBaseSource
    {
        SourceTypes Type { get; set; }
    }
}
