using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models.Sources
{
    public class ElectronicSource : IBaseSource
    {
        [Required]
        public string JobName { get; set; }

        public int? YearOfPublication { get; set; }

        public string Publication { get; set; }

        [Required]
        public string LinkToSource { get; set; }

        public List<Author> Authors { get; set; }

        public SourceTypes Type { get; set; } = SourceTypes.Electronic;
    }
}
