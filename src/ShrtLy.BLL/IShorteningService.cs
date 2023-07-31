using System.Collections.Generic;
using System.Threading.Tasks;
using ShrtLy.BLL.Dtos;

namespace ShrtLy.BLL
{
    public interface IShorteningService
    {
        Task<IEnumerable<LinkDto>> GetLinksAsync();

        Task<LinkDto> GenerateLinkAsync(string url);
    }
}