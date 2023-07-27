using System.Collections.Generic;

namespace ShrtLy.DAL
{
    public class LinksRepository : ILinksRepository
    {
        private readonly ShrtLyContext _context;

        public LinksRepository(ShrtLyContext context)
        {
            _context = context;
        }

        public int CreateLink(LinkEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<LinkEntity> GetAllLinks()
        {
            return _context.Links;
        }
    }
}
