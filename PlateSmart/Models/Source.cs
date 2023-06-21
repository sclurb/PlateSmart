using System;

namespace PlateSmart.Models
{
    public class Source
    {
        public string Id { get; set; }
        public string Type { get; set; }
        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(!value.Equals(string.Empty) && value.Contains(','))
                {
                    value = value.Replace(',', ':');
                }
                _name = value;
            }
        }


    }
}
