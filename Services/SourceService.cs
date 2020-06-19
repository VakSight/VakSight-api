﻿using Models.Enums;
using Models.Sources;
using Repository.DatabaseModels;
using Repository.UnitOfWork;
using Services.Interfaceses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class SourceService : ISourceService
    {
        private IUnitOfWork _unitOfWork;

        private readonly Dictionary<SourceTypes, Func<BaseSource, Task<string>>> sourceFormaters;
        private readonly Dictionary<PublicationNumberTypes, string> shortPublicationTypes;

        

        public SourceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
            sourceFormaters = new Dictionary<SourceTypes, Func<BaseSource, Task<string>>>
            {
                { SourceTypes.Electronic,  async x => await CreateElectronicSourceAsync(x) },
                {SourceTypes.Book, async x => await CreateBookSourceAsync(x) }
            };

            shortPublicationTypes = new Dictionary<PublicationNumberTypes, string>
            {
                {PublicationNumberTypes.Book, "кн." },
                {PublicationNumberTypes.Edition, "вип." },
                {PublicationNumberTypes.Number, "вип." },
                {PublicationNumberTypes.Volume, "№" }
            };
        }

        public async Task<string> CreateSourceAsync(BaseSource source)
        {
            return await sourceFormaters[source.Type](source);
        }

        private async Task<string> CreateElectronicSourceAsync(BaseSource source)
        {
            var electronicSource = source as ElectronicSource;
            var publication = electronicSource.Publication == null ? string.Empty : $" // electronicSource.Publication";
            var yearOfPulication = electronicSource.YearOfPublication == null ? string.Empty : $". – {electronicSource.YearOfPublication}.";
            string firstAuthor = string.Empty;
            string authors = string.Empty;

            if (electronicSource.Authors != null && electronicSource.Authors.Any())
            {
                var author = electronicSource.Authors.First();
                firstAuthor = $"{author.FirstName} {author.LastName.Substring(0, 1).ToUpper()}. {author.Surname.Substring(0, 1).ToUpper()}. ";
            }
            if(electronicSource.Authors?.Count > 1)
            {
                authors = ParseAuthors(electronicSource.Authors);
            }

            var content = $"{firstAuthor}{electronicSource.JobName} [Електронний ресурс]{authors}{publication}{yearOfPulication}" +
                $" – Режим доступу до ресурсу: {electronicSource.LinkToSource}";

            var newSource = new SourceRecord { Content = content, Type = electronicSource.Type, Authors = electronicSource.Authors };
            //await _unitOfWork.Sources.CreateSourceAsync(newSource);

            return content;
        }
        private async Task<string> CreateBookSourceAsync(BaseSource source)
        {
            var bookSource = source as BookSource;
            string firstAuthor = string.Empty;
            string authors = string.Empty;
            
            
            
            if (bookSource.Authors != null && bookSource.Authors.Any())
            {
                
                var author = bookSource.Authors.First();
                authors = $" {author.FirstName} {author.LastName} {author.Surname}";

                firstAuthor = $"{author.FirstName} {author.LastName.Substring(0, 1).ToUpper()}. {author.Surname.Substring(0, 1).ToUpper()}. ";
            
            }
            if (bookSource.Authors?.Count > 1)
            {
                authors = ParseAuthors(bookSource.Authors);
                
            }
            var content = $"{firstAuthor} {bookSource.WorkName} /" + 
                $" {authors}. – {bookSource.PlaceOfPublication}: " +
                $"{bookSource.PublishingHouse}, {bookSource.YearOfPublication}." +
                $" – {bookSource.NumberOfPages} c. – ({bookSource.PublishingName}). – " +
                $"({bookSource.Series}; {shortPublicationTypes[bookSource.PublicationNumberType]} {bookSource.PeriodicSelectionNumber})";

            return content;
        }

        private string ParseAuthors(List<Author> authors)
        {
            var result = " / ";

            foreach(var author in authors)
            {
                result += $"{author.Surname.Substring(0, 1).ToUpper()}. {author.LastName.Substring(0, 1).ToUpper()}. {author.FirstName}, ";
            }

            return result;
        }
    }
}
