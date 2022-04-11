using Autofac;
using MarketingBox.Affiliate.Service.MyNoSql.OfferAffiliates;
using MarketingBox.TrackingLink.Service.Repositories;
using MarketingBox.TrackingLink.Service.Repositories.Interfaces;
using MarketingBox.TrackingLink.Service.Services;
using MyJetWallet.Sdk.NoSql;

namespace MarketingBox.TrackingLink.Service.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var noSqlClient = builder.CreateNoSqlClient(Program.ReloadedSettings(e => e.MyNoSqlReaderHostPort));
            builder.RegisterMyNoSqlReader<OfferAffiliateNoSql>(noSqlClient, OfferAffiliateNoSql.TableName);

            builder.RegisterType<TrackingLinkRepository>()
                .As<ITrackingLinkRepository>()
                .SingleInstance();
        }
    }
}