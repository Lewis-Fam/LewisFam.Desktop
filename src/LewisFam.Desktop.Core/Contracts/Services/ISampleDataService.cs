using System.Collections.Generic;
using System.Threading.Tasks;
using LewisFam.Desktop.Core.Models;

namespace LewisFam.Desktop.Core.Contracts.Services
{
    public interface ISampleDataService
    {
        Task<IEnumerable<SampleOrder>> GetMasterDetailDataAsync();
    }
}
