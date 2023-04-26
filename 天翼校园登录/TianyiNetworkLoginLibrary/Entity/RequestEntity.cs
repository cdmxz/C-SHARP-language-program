
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TianyiNetworkLoginLibrary.Entity
{
    /// <summary>
    /// 请求实体类
    /// </summary>
    internal class RequestEntity
    {

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("clientip")]
        public string ClientIp { get; set; }

        [JsonPropertyName("nasip")]
        public string NasIp { get; set; }

        [JsonPropertyName("mac")]
        public string Mac { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("authenticator")]
        public string Authenticator { get; set; }

        public const string Secret = "Eshore!@#";

        public RequestEntity()
        {
            Version = "214";
            ClientIp = string.Empty;
            NasIp = string.Empty;
            Mac = string.Empty;
            Timestamp = string.Empty;
            Authenticator = string.Empty;
            Username = string.Empty;
        }

        public RequestEntity(RequestEntity entity)
        {
            Version = entity.Version;
            ClientIp = entity.ClientIp;
            NasIp = entity.NasIp;
            Mac = entity.Mac;
            Timestamp = entity.Timestamp;
            Authenticator = entity.Authenticator;
            Username = entity.Username;
        }


        public virtual string GetJson() => JsonSerializer.Serialize(this);

        /// <summary>
        /// 设置Timestamp
        /// </summary>
        public void SetNowUnixTimestamp() => Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

        /// <summary>
        /// 设置Authenticator
        /// </summary>
        /// <param name="md5Str"></param>
        public void SetAuthenticator(string md5Str)
        {
            Authenticator = md5Str;
        }
        
        //public void ComputeMd5(RequestEntity entity) 
        //{

        //}
    }
}
