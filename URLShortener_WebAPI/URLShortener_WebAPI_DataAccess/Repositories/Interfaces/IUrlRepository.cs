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

        Url TryGetById(int id);

        Url TryGetByOriginal(string originalUrl);

        void Save(Url url);

        void UpdateViewCount(Url url);
    }
}
