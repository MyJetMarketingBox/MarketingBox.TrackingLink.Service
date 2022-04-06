using MyJetWallet.Sdk.Postgres;

namespace MarketingBox.TrackingLink.Service.Postgres.DesignTime
{
    public class ContextFactory : MyDesignTimeContextFactory<DatabaseContext>
    {
        public ContextFactory() : base(options => new DatabaseContext(options))
        {

        }
    }
}