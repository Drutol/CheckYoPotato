using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckYoPotato.Web.Models;

namespace CheckYoPotato.Web.Repository
{
    public class PhotosRepository : IPhotosRepository
    {
        private static ConcurrentDictionary<int, Photo> _todos =
            new ConcurrentDictionary<int, Photo>();

        public PhotosRepository()
        {
            Add(new Photo { Link = "https://checkyopotato.blob.core.windows.net/fridge1/photo.png", Timestamp = DateTime.Now});
        }

        public IEnumerable<Photo> GetAll()
        {
            return _todos.Values;
        }

        public void Add(Photo item)
        {
            _todos[item.camId] = item;
        }

        public Photo Find(int key)
        {
            Photo item;
            _todos.TryGetValue(key, out item);
            return item;
        }

        public Photo Remove(int key)
        {
            Photo item;
            _todos.TryRemove(key, out item);
            return item;
        }

        public void Update(Photo item)
        {
            _todos[item.camId] = item;
        }
    }
}
