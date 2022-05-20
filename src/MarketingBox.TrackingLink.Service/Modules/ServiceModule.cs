using Autofac;
using MarketingBox.Affiliate.Service.Client;
using MarketingBox.TrackingLink.Service.Engines;
using MarketingBox.TrackingLink.Service.Engines.Interfaces;
using MarketingBox.TrackingLink.Service.Repositories;
using MarketingBox.TrackingLink.Service.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.NoSql;

namespace MarketingBox.TrackingLink.Service.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var noSqlClient = builder.CreateNoSqlClient(
                Program.ReloadedSettings(e => e.MyNoSqlReaderHostPort).Invoke(),
                new LoggerFactory());
            builder.RegisterAffiliateClient(Program.Settings.AffiliateServiceUrl, noSqlClient);
            builder.RegisterBrandClient(Program.Settings.AffiliateServiceUrl, noSqlClient);
            builder.RegisterOfferClient(Program.Settings.AffiliateServiceUrl, noSqlClient);
            builder.RegisterOfferAffiliateClient(Program.Settings.AffiliateServiceUrl, noSqlClient);

            builder.RegisterType<TrackingLinkRepository>()
                .As<ITrackingLinkRepository>()
                .SingleInstance();
            builder.RegisterType<NoSqlDataReader>()
                .As<INoSqlDataReader>()
                .SingleInstance();

            builder.RegisterType<TrackingLinkUpdateSubscriber>()
                .As<IStartable>()
                .SingleInstance()
                .AutoActivate();
        }
    }
}