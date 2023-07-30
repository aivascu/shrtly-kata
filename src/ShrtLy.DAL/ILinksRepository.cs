using System.Collections.Generic;

namespace ShrtLy.DAL
{
    public interface ILinksRepository
    {
        int CreateLink(Link entity);
        IEnumerable<Link> GetAllLinks();
    }
}