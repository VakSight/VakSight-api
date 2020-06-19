using Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Sources
{
    public class BookSource : BaseSource
    {
        [Required]
        public string WorkName { get; set; }
        
        public string PlaceOfPublication { get; set; }

        public string PublishingHouse { get; set; }

        public int? YearOfPublication { get; set; }

        public int? NumberOfPages { get; set; }

        public string PublishingName { get; set; }

        public string Series { get; set; }

        public int? PeriodicSelectionNumber { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public PublicationNumberTypes PublicationNumberType { get; set; }

        public List<Author> Authors { get; set; }
    }
}
