using Models.Consts;
using Models.Enums;
using Models.Exceptions;
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

        private readonly Dictionary<SourceTypes, Func<IBaseSource, Task<string>>> sourceFormaters;
        private readonly Dictionary<PublicationNumberTypes, string> shortPublicationTypes;

        public SourceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
            sourceFormaters = new Dictionary<SourceTypes, Func<IBaseSource, Task<string>>>
            {
                { SourceTypes.Electronic,  async x => await CreateElectronicSourceAsync(x) },
                { SourceTypes.Book, async x => await CreateBookSourceAsync(x) },
                { SourceTypes.Periodical, async x => await CreatePeriodicalSourceAsync(x) },
                { SourceTypes.Dissertation, async x => await CreateDissertationSourceAsync(x as DissertationSource) },
                { SourceTypes.AbstractOfDissertation, async x => await CreateDissertationSourceAsync(x as AbstractDissertationSource) }
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
            var publication = electronicSource.Publication == null ? string.Empty : $" // {electronicSource.Publication}";
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

        private async Task<string> CreatePeriodicalSourceAsync(IBaseSource source)
        {
            var periodicalSource = source as PeriodicalSource;

            var publication = periodicalSource.Publication == null ? string.Empty : $" // electronicSource.Publication";
            var yearOfPulication = periodicalSource.YearOfPublication == null ? string.Empty : $". – {periodicalSource.YearOfPublication}.";
            var periodicSelectionNumber = periodicalSource.PeriodicSelectionNumber == null ? string.Empty : $" – №{periodicalSource.PeriodicSelectionNumber}.";
            var pages = periodicalSource.Pages == null ? string.Empty : $" – C. {periodicalSource.Pages}.";

            var content = $"{periodicalSource.ParseAuthor()}{periodicalSource.WorkName}{periodicalSource.ParseAllAuthors()}{publication}{yearOfPulication}" +
                $"{periodicSelectionNumber}{pages}";

            var newSource = new SourceRecord { Content = content, Type = periodicalSource.Type, Authors = periodicalSource.Authors };
            //await _unitOfWork.Sources.CreateSourceAsync(newSource);

            return content;
        }

        private async Task<string> CreateDissertationSourceAsync(DissertationSource source)
        {
            var dissertationSource = source as DissertationSource;

            ValidateDissertationSource(dissertationSource);

            var placeOfPublication = dissertationSource == null ? string.Empty : $" - {dissertationSource.PlaceOfPublication}";
            var yearOfPublication = dissertationSource == null ? string.Empty : $", {dissertationSource.YearOfPublication}";
            var numberOfPages = dissertationSource.NumberOfPages == null ? string.Empty : $". - {dissertationSource.NumberOfPages} с";

            var content = $"{dissertationSource.ParseAuthor()}{dissertationSource.WorkName}{dissertationSource.GetScientificDegree()}{dissertationSource.GetSpecialty()}" +
                $"{dissertationSource.ParseAllAuthors()}{placeOfPublication}{yearOfPublication}{numberOfPages}.";

            var newSource = new SourceRecord { Content = content, Type = dissertationSource.Type, Authors = dissertationSource.Authors };
            //await _unitOfWork.Sources.CreateSourceAsync(newSource);

            return content;
        }

        private void ValidateDissertationSource(DissertationSource source)
        {
            if(!string.IsNullOrEmpty(source.ScientificDegreeName) && !ScientificDegrees.ScientificDegreeNames.Keys.Contains(source.ScientificDegreeName))
            {
                throw new BadRequestException("Invalid scientific degree name!");
            }
            if (!string.IsNullOrEmpty(source.ScientificDegreeSpecialty) && !ScientificDegrees.ScientificDegreeSpecialties.Keys.Contains(source.ScientificDegreeSpecialty))
            {
                throw new BadRequestException("Invalid scientific degree specialty!");
            }
        }
    }
}
