using System;

namespace PlateSmart.Models
{
    public enum ALPR
    {
        alpr
    }
    public class AlprEvent
    {

        public string Id { get; set; }
        public ALPR ALPR { get; set; }
        public string Version { get; set; }
        public Int64 TimeStamp { get; set; }
        public Source Source { get; set; }
        public Image Image { get; set; }
        public Device Device { get; set; }
        public Plate Plate { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}
