using ShrtLy.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShrtLy.BLL
{
    public class ShorteningService : IShorteningService
    {
        private readonly ILinksRepository _repository;

        public ShorteningService(ILinksRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> ProcessLink(string url)
        {
            var entity = await _repository.GetLink(url);
            if (entity == null)
            {
                Thread.Sleep(1);//make everything unique while looping
                var ticks = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalMilliseconds;//EPOCH
                var baseChars = new char[] { '0','1','2','3','4','5','6','7','8','9',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x'};

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

                var shortUrl = new string(result);

                var link = new LinkEntity
                {
                    ShortUrl = shortUrl,
                    Url = url
                };

                await _repository.CreateLink(link);

                return link.ShortUrl;
            }
            else
            {
                return entity.ShortUrl;
            }
        }

        public async Task<IEnumerable<LinkDto>> GetShortLinks()
        {
            var linkEntities = await _repository.GetAllLinks();
            return linkEntities.Select(element => new LinkDto
            {
                Id = element.Id,
                ShortUrl = element.ShortUrl,
                Url = element.Url
            });
        }
    }
}
