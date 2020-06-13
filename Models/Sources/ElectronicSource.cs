using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Sources
{
    public class ElectronicSource : BaseSource
    {
        [Required]
        public string JobName { get; set; }

        public int? YearOfPublication { get; set; }

        public string Publication { get; set; }

        [Required]
        public string LinkToSource { get; set; }

        public List<Author> Authors { get; set; }
    }
}
