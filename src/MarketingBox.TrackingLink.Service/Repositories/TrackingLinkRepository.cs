using System.Threading.Tasks;
using AutoMapper;
using MarketingBox.Sdk.Common.Exceptions;
using MarketingBox.TrackingLink.Service.Domain.Models;
using MarketingBox.TrackingLink.Service.Grpc.Requests;
using MarketingBox.TrackingLink.Service.Postgres;
using MarketingBox.TrackingLink.Service.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MarketingBox.TrackingLink.Service.Repositories
{
    public class TrackingLinkRepository : ITrackingLinkRepository
    {
        private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;
        private readonly IMapper _mapper;

        public TrackingLinkRepository(DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder, IMapper mapper)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
            _mapper = mapper;
        }

        public async Task<Domain.Models.TrackingLink> CreateAsync(TrackingLinkCreateModel request)
        {
            var trackingLink = _mapper.Map<Domain.Models.TrackingLink>(request);

            await using var ctx = new DatabaseContext(_dbContextOptionsBuilder.Options);

            ctx.Add(trackingLink);

            await ctx.SaveChangesAsync();

            return trackingLink;
        }

        public async Task<Domain.Models.TrackingLink> GetAsync(TrackingLinkByClickIdRequest request)
        {
            await using var ctx = new DatabaseContext(_dbContextOptionsBuilder.Options);

            var trackingLink = await ctx.TrackingLinks.FirstOrDefaultAsync(x => x.ClickId == request.ClickId);

            if (trackingLink is null)
            {
                throw new NotFoundException($"Tracking link with {nameof(request.ClickId)}", request.ClickId);
            }

            return trackingLink;
        }

        public async Task<Domain.Models.TrackingLink> UpdateRegistrationIdAsync(
            TrackingLinkUpdateRegistrationIdRequest request)
        {
            await using var ctx = new DatabaseContext(_dbContextOptionsBuilder.Options);

            var trackingLink = await ctx.TrackingLinks.FirstOrDefaultAsync(x => x.ClickId == request.ClickId);

            if (trackingLink is null)
            {
                throw new NotFoundException($"Tracking link with {nameof(request.ClickId)}", request.ClickId);
            }

            trackingLink.RegistrationId = request.RegistrationId;

            await ctx.SaveChangesAsync();

            return trackingLink;
        }
    }
}