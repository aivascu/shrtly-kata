using ShrtLy.BLL.Dtos;
using ShrtLy.DAL.Entities;
using ShrtLy.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShrtLy.BLL
{
    public class ShorteningService : IShorteningService
    {
        private readonly IRepository<Link> _linkRepository;

        public ShorteningService(IRepository<Link> repository)
        {
            this._linkRepository = repository;
        }

        public async Task<LinkDto> GenerateLinkAsync(string url)
        {
            var foundLink = await this._linkRepository.GetByUrl(url);
            if (foundLink is not null)
            {
                return this.MapLinkToLinkDto(foundLink);
            }

            var generatedLink = new Link
            {
                ShortUrl = this.GenerateShortUrl(),
                Url = url
            };
            await _linkRepository.CreateAsync(generatedLink);

            return this.MapLinkToLinkDto(generatedLink);
        }


        public async Task<IEnumerable<LinkDto>> GetLinksAsync()
        {
            var links = await _linkRepository.GetAllAsync();

            return links.Select(link => this.MapLinkToLinkDto(link));
        }

        private string GenerateShortUrl()
        {
            Thread.Sleep(1); //make everything unique while looping

            var ticks = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalMilliseconds;//EPOCH
            var baseChars = new char[] {
                '0','1','2','3','4','5','6','7','8','9',
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x'
            };

            var i = 32;
            var buffer = new char[i];
            var targetBase = baseChars.Length;
            do
            {
                buffer[--i] = baseChars[ticks % targetBase];
                ticks /= targetBase;
            }
            while (ticks > 0);

            var result = new char[32 - i];
            Array.Copy(buffer, i, result, 0, 32 - i);

            return new string(result);
        }

        private LinkDto MapLinkToLinkDto(Link source)
        {
            return new LinkDto
            {
                Id = source.Id,
                ShortUrl = source.ShortUrl,
                Url = source.Url
            };
        }
    }
}
