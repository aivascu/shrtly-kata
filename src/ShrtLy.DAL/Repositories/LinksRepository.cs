using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShrtLy.DAL.Entities;

namespace ShrtLy.DAL.Repositories
{
    public class LinksRepository : ILinksRepository
    {
        private readonly ShrtLyContext context;

        public LinksRepository(ShrtLyContext context)
        {
            this.context = context;
        }

        public int CreateLink(Link entity)
        {
            context.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<Link> GetAllLinks()
        {
            return context.Links;
        }
    }
}
