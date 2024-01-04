using RestSharp;

namespace 腾讯会议摸鱼助手.Notice.Push
{
    internal class PushMessage
    {
        private static readonly string url = "https://courier.toptopn.com/api/v1/cui/notify/push";
        public static void Push(string uuid, string uids, string message)
        {
            RestRequest request = new(url);
            request.AddParameter("uuid", uuid);
            request.AddParameter("uids", uids);
            request.AddParameter("text", message);
            request.AddParameter("title", message);
            request.AddParameter("url", message);
            using RestClient client = new();
            var response = client.Post(request);
        }
    }
}
