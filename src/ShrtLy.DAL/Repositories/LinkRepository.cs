using Microsoft.EntityFrameworkCore;
using ShrtLy.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShrtLy.DAL.Repositories
{
    public class LinkRepository : IRepository<Link>
    {
        private readonly ShrtLyContext context;

        public LinkRepository(ShrtLyContext context)
        {
            this.context = context;
        }

        public async Task<int> CreateAsync(Link link)
        {
            await context.AddAsync(link);
            await context.SaveChangesAsync();

            return link.Id;
        }

        public async Task<IEnumerable<Link>> GetAllAsync()
        {
            return await context.Links.ToListAsync();
        }

        public async Task<Link> GetByUrl(string url)
        {
            return await context.Links.FirstOrDefaultAsync(x => x.Url == url);
        }
    }
}
