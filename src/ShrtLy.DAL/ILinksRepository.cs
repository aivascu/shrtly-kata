using System.Collections.Generic;
using ShrtLy.DAL.Entities;

namespace ShrtLy.DAL
{
    public interface ILinksRepository
    {
        int CreateLink(Link entity);
        IEnumerable<Link> GetAllLinks();
    }
}