using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarketingBox.TrackingLink.Service.Domain.Models;
using MarketingBox.TrackingLink.Service.Postgres;
using MarketingBox.TrackingLink.Service.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketingBox.TrackingLink.Service.Repositories
{
    public class TrackingLinkRepository : ITrackingLinkRepository
    {
        private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;
        private IMapper _mapper;

        public TrackingLinkRepository(DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder, IMapper mapper)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
            _mapper = mapper;
        }

        public async Task<Domain.Models.TrackingLink> CreateAsync(TrackingLinkCreateModel request)
        {
            var trackingLink = _mapper.Map<Domain.Models.TrackingLink>(request);
            
            await using var ctx = new DatabaseContext(_dbContextOptionsBuilder.Options);
            var existing = await ctx.TrackingLinks
                .OrderBy(x=>x.Id)
                .LastOrDefaultAsync(x => x.UniqueId == request.UniqueId);
            
            trackingLink.ClickId = existing is null
                ? 1
                : existing.ClickId+1;
            trackingLink.LinkParameterValues.ClickId = trackingLink.ClickId.ToString(); 
            
            ctx.Add(trackingLink);
            
            await ctx.SaveChangesAsync();
            
            return trackingLink;
        }
    }
}