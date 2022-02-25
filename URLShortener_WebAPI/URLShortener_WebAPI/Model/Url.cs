using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortener_WebAPI.Model
{
    public class Url
    {
        public string Id { get; set; }
        public string Original { get; set; }
        public string Shortened { get; set; }
        public string VisitCount { get; set; }
    }
}
