using ShrtLy.DAL.Entities;
using System.Collections.Generic;

namespace ShrtLy.DAL.Repositories
{
    public class LinksRepository : IRepository<Link>
    {
        private readonly ShrtLyContext context;

        public LinksRepository(ShrtLyContext context)
        {
            this.context = context;
        }

        public int Create(Link entity)
        {
            context.Add(entity);
            context.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<Link> GetAll()
        {
            return context.Links;
        }
    }
}
