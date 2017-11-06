using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevProject.Models
{
    public class ModuleReferenceList
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string Link { get; set; }
        public string Text { get; set; }
    }
}
