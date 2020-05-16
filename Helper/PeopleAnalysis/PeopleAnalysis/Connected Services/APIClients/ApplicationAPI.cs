using CommonCoreLibrary.APIClient;

namespace PeopleAnalysis.ApplicationAPI
{
    public partial class ApplicationAPIClient : BaseAPIClient
    {
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            base.PrepareRequest(client, request, url);
        }
    }
}
