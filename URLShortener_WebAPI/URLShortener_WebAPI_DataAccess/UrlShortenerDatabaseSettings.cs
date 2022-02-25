using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener_WebAPI_DataAccess
{
    public class UrlShortenerDatabaseSettings : IUrlShortenerDatabaseSettings
    {
        public string UrlsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
