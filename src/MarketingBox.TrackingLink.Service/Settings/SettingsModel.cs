using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace MarketingBox.TrackingLink.Service.Settings
{
    public class SettingsModel
    {        
        [YamlProperty("MarketingBoxTrackingLinkService.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("MarketingBoxTrackingLinkService.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("MarketingBoxTrackingLinkService.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }
        
        [YamlProperty("MarketingBoxTrackingLinkService.PostgresConnectionString")]
        public string PostgresConnectionString { get; set; }
        
        [YamlProperty("MarketingBoxTrackingLinkService.MyNoSqlReaderHostPort")]
        public string MyNoSqlReaderHostPort { get; set; }
    }
}