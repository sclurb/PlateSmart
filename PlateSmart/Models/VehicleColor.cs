using System.Text.Json.Serialization;


namespace PlateSmart.Models
{
    public enum Color {
      //  [EnumMember(Value = "black")]
        black = 0, 
        white = 1, 
        gray = 2, 
        blue = 3, 
        red = 4, 
        yellow = 5 }
    public class VehicleColor
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Color Code { get; set; }
    }
}
