using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShrtLy.DAL
{
    public class LinksRepository : ILinksRepository
    {
        private readonly ShrtLyContext _context;

        public LinksRepository(ShrtLyContext context)
        {
            _context = context;
        }

        public async Task<int> CreateLink(LinkEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<LinkEntity>> GetAllLinks()
        {
            return await _context.Links.ToListAsync();
        }

        public async Task<LinkEntity> GetLink(string url)
        {
            return await _context.Links.FirstOrDefaultAsync(l => l.ShortUrl == url);
        }
    }
}
