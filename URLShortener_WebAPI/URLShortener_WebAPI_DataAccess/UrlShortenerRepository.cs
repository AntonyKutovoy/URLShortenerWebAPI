﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener_WebAPI.Model;

namespace URLShortener_WebAPI_DataAccess
{
    public class UrlShortenerRepository : IUrlShortenerRepository
    {
        private readonly IMongoCollection<Url> _urls;

        public UrlShortenerRepository(IUrlShortenerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _urls = database.GetCollection<Url>(settings.UrlsCollectionName);
        }

        public List<Url> GetAll() =>
           _urls.Find(url => true).ToList();

        public Url TryGetByShortened(string shortenedUrl) =>
            _urls.Find(url => url.Shortened == shortenedUrl).FirstOrDefault();

        public void Save(Url url) =>
            _urls.InsertOne(url);

        public void UpdateVisitCount(Url url) =>
            _urls.ReplaceOne(b => b.Id == url.Id, url);
    }
}
