using Models.Enums;
using Models.Sources;
using Repository.DatabaseModels;
using Repository.UnitOfWork;
using Services.Interfaceses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class SourceService : ISourceService
    {
        private IUnitOfWork _unitOfWork;

        private readonly Dictionary<SourceTypes, Func<IBaseSource, Task<string>>> sourceFormaters;
        private readonly Dictionary<PublicationNumberTypes, string> shortPublicationTypes;

        public SourceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
            sourceFormaters = new Dictionary<SourceTypes, Func<IBaseSource, Task<string>>>
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

        public async Task<string> CreateSourceAsync(IBaseSource source)
        {
            return await sourceFormaters[source.Type](source);
        }

        private async Task<string> CreateElectronicSourceAsync(IBaseSource source)
        {
            var electronicSource = source as ElectronicSource;
            var publication = electronicSource.Publication == null ? string.Empty : $" // electronicSource.Publication";
            var yearOfPulication = electronicSource.YearOfPublication == null ? string.Empty : $". – {electronicSource.YearOfPublication}.";

            var content = $"{electronicSource.ParseAuthor()}{electronicSource.WorkName} [Електронний ресурс]{electronicSource.ParseAllAuthors()}{publication}{yearOfPulication}" +
                $" – Режим доступу до ресурсу: {electronicSource.LinkToSource}";

            var newSource = new SourceRecord { Content = content, Type = electronicSource.Type, Authors = electronicSource.Authors };
            //await _unitOfWork.Sources.CreateSourceAsync(newSource);

            return content;
        }

        private async Task<string> CreateBookSourceAsync(IBaseSource source)
        {
            var bookSource = source as BookSource;

            var content = $"{bookSource.ParseAuthor()} {bookSource.WorkName} /" + 
                $" {bookSource.ParseAllAuthors()}. – {bookSource.PlaceOfPublication}: " +
                $"{bookSource.PublishingHouse}, {bookSource.YearOfPublication}." +
                $" – {bookSource.NumberOfPages} c. – ({bookSource.PublishingName}). – " +
                $"({bookSource.Series}; {shortPublicationTypes[bookSource.PublicationNumberType]} {bookSource.PeriodicSelectionNumber})";

            return content;
        }

    }
}
