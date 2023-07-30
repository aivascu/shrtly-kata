using System.Collections.Generic;
using ShrtLy.DAL.Entities;

namespace ShrtLy.DAL.Repositories
{
    public interface ILinksRepository
    {
        int CreateLink(Link entity);
        IEnumerable<Link> GetAllLinks();
    }
}