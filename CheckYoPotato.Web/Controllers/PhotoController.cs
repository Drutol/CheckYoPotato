using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CheckYoPotato.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using System.Threading;

namespace CheckYoPotato.Web.Controllers
{
    

    [Route("api/[controller]")]
    public class PhotoController : Controller
    {
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(0);

        public IPhotosRepository Photos { get; set; }

        public PhotoController(IPhotosRepository todoItems)
        {
            Photos = todoItems;
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(int id)
        {
            var item = Photos.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await _semaphore.WaitAsync(TimeSpan.FromSeconds(3));
            return new ObjectResult(Photos.Find(0));
        }

        [HttpPost]
        public IActionResult Create([FromBody] Photo item)
        {
            if (item == null)
                return BadRequest();
            Photos.Add(item);
            if(_semaphore.CurrentCount > 0)
                _semaphore.Release();
            return CreatedAtRoute("GetTodo", new { id = item.camId }, item);
        }
    }
}
