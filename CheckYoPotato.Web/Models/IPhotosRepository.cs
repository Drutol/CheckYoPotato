using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckYoPotato.Web.Models
{
    public interface IPhotosRepository
    {
        void Add(Photo item);
        IEnumerable<Photo> GetAll();
        Photo Find(int key);
        Photo Remove(int key);
        void Update(Photo item);
    }
}
