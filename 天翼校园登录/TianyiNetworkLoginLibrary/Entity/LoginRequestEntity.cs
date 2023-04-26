using System.Text.Json;
using System.Text.Json.Serialization;

namespace TianyiNetworkLoginLibrary.Entity
{
    /// <summary>
    /// 登录请求实体类
    /// </summary>
    internal class LoginRequestEntity : RequestEntity
    {
        [JsonPropertyName("iswifi")]
        public string Iswifi { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
       
        public LoginRequestEntity(string password, RequestEntity baseEntity) : base(baseEntity)
        {
            Password = password;
            Iswifi = "4060";
        }

        /// <summary>
        ///  序列化本类
        /// </summary>
        /// <returns></returns>
        public override string GetJson()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
