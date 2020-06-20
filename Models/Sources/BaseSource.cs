using Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Models.Sources
{
    public class BaseSource : IBaseSource
    {
        [Required]
        public string WorkName { get; set; }

        public List<Author> Authors { get; set; }

        public virtual SourceTypes Type { get; set; }

        public string ParseAuthor()
        {
            if (Authors is null || Authors.Count == 0)
            {
                return string.Empty;
            }
            var author = Authors.First();

            return $"{author?.LastName} {author.FirstName?.Substring(0, 1).ToUpper()}. {author.FathersName?.Substring(0, 1).ToUpper()}. ";
        }

        public string ParseAllAuthors()
        {
            if(Authors is null || Authors.Count == 0)
            {
                return string.Empty;
            }

            if(Authors.Count == 1)
            {
                var author = Authors.First();
                return $" / {author?.LastName} {author?.FirstName} {author?.FathersName}";
            }

            var result = " / ";
           
            for(int i = 0; i < Authors.Count - 1; i++)
            {
                var author = Authors[i];
                result += $"{author.FirstName?.Substring(0, 1).ToUpper()}. {author.FathersName?.Substring(0, 1).ToUpper()}. {author.LastName}, ";
            }

            var lastAuthor = Authors.Last();
            result += $"{lastAuthor.FirstName?.Substring(0, 1).ToUpper()}. {lastAuthor.FathersName?.Substring(0, 1).ToUpper()}. {lastAuthor.LastName}";

            return result;
        }
    }
}
