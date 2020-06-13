using Models.Enums;
using Models.Sources;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.DatabaseModels
{
    public class SourceRecord
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public SourceTypes Type { get; set; }

        private string _authors { get; set; }

        [NotMapped]
        public List<Author> Authors
        { 
            get 
            {
                return JsonConvert.DeserializeObject<List<Author>>(_authors);
            }
            set
            {
                _authors = JsonConvert.SerializeObject(value);
            }
        }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
