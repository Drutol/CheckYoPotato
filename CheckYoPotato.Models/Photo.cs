using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckYoPotato.Web.Models
{
    public class Photo
    {
        public int camId { get; set; }
        public string Link { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
