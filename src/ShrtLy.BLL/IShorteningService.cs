using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShrtLy.BLL
{
    public interface IShorteningService
    {
        Task<IEnumerable<LinkDto>> GetShortLinks();
        Task<string> ProcessLink(string url);
    }
}