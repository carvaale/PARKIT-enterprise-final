using System.Buffers.Text;

namespace PARKIT_enterprise_final.Models
{
    public class MapPoint
    {
        public Guid id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string thumbnail { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public double price { get; set; }

    }
}
