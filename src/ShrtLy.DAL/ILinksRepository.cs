using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShrtLy.DAL
{
    public interface ILinksRepository
    {
        Task<int> CreateLink(LinkEntity entity);
        Task<IEnumerable<LinkEntity>> GetAllLinks();
        Task<LinkEntity> GetLink(string url);
    }
}