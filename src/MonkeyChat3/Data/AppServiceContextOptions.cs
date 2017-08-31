using AzureMobileClient.Helpers;
using MonkeyChat3.Helpers;

namespace MonkeyChat3.Data
{
    // This is only needed when you are using Authentication with the Azure Mobile Client
    public class AppServiceContextOptions : IAzureCloudServiceOptions
    {
        public string AppServiceEndpoint => Secrets.AppServiceEndpoint;
        
        public string AlternateLoginHost => Secrets.AlternateLoginHost;

        public string LoginUriPrefix => Secrets.LoginUriPrefix;
    }
}