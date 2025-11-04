using System.Text.Json.Serialization;

namespace 翻译神器WPF
{
    public class UpdateInfo
    {
        [JsonPropertyName("info")]
        public string Info { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
