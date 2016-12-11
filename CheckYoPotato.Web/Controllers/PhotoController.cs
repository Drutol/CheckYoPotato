using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CheckYoPotato.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;

namespace CheckYoPotato.Web.Controllers
{
    [Route("api/[controller]")]
    public class PhotoController : Controller
    {
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

        [HttpPost]
        public IActionResult Create([FromBody] Photo item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            Photos.Add(item);

            return CreatedAtRoute("GetTodo", new { id = item.camId }, item);
        }
    }
}
