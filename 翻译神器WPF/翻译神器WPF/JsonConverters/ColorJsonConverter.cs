using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace ∑≠“Î…Ò∆˜WPF.JsonConverters
{
    // Serialize/deserialize System.Windows.Media.Color as #AARRGGBB
    public sealed class ColorJsonConverter : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var s = reader.GetString();
                if (string.IsNullOrWhiteSpace(s))
                {
                    return Colors.Transparent;
                }

                // Accept formats: #AARRGGBB, #RRGGBB, or named colors
                if (s!.StartsWith('#'))
                {
                    return ParseHex(s);
                }

                // Try named color
                try
                {
                    var c = (Color)ColorConverter.ConvertFromString(s)!;
                    return c;
                }
                catch
                {
                    return Colors.Transparent;
                }
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                // Also accept {"A":...,"R":...,"G":...,"B":...}
                byte a = 255, r = 0, g = 0, b = 0;
                while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                {
                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        var name = reader.GetString();
                        reader.Read();
                        switch (name)
                        {
                            case "A": a = (byte)reader.GetInt32(); break;
                            case "R": r = (byte)reader.GetInt32(); break;
                            case "G": g = (byte)reader.GetInt32(); break;
                            case "B": b = (byte)reader.GetInt32(); break;
                        }
                    }
                }
                return Color.FromArgb(a, r, g, b);
            }

            throw new JsonException($"Unexpected token {reader.TokenType} when parsing Color");
        }

        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            // Serialize as #AARRGGBB
            writer.WriteStringValue(ToHex(value));
        }

        public static string ToHex(Color c)
        => string.Create(CultureInfo.InvariantCulture, $"#{c.A:X2}{c.R:X2}{c.G:X2}{c.B:X2}");

        private static Color ParseHex(string s)
        {
            s = s.Trim();
            if (s[0] == '#') s = s.Substring(1);

            // #RRGGBB -> add FF alpha
            if (s.Length == 6)
            {
                s = "FF" + s;
            }

            if (s.Length != 8)
            {
                return Colors.Transparent;
            }

            byte a = byte.Parse(s.AsSpan(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            byte r = byte.Parse(s.AsSpan(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            byte g = byte.Parse(s.AsSpan(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            byte b = byte.Parse(s.AsSpan(6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            return Color.FromArgb(a, r, g, b);
        }
    }
}
