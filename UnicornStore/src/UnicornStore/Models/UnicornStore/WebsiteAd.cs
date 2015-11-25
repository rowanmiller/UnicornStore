using System;

namespace UnicornStore.Models.UnicornStore
{
    public class WebsiteAd
    {
        public int WebsiteAdId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string TagLine { get; set; }
        public string Details { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
    }
}