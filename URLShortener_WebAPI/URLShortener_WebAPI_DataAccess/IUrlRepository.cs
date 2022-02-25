using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener_WebAPI.Model;

namespace URLShortener_WebAPI_DataAccess
{
    public interface IUrlRepository
    {
        List<Url> GetAll();

        Url TryGetByShortened(string shortenedUrl);

        void Save(Url url);

        void UpdateVisitCount(Url url);
    }
}
