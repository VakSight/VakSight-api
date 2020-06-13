using Models.Sources;
using System.Threading.Tasks;

namespace Services.Interfaceses
{
    public interface ISourceService
    {
        Task<string> CreateSourceAsync(BaseSource source);
    }
}
