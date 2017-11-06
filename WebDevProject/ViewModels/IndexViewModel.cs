using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebDevProject.Models
{
    public class IndexViewModel
    {
        public IList<Module> Modules { get; set; }
        public Index theIndex { get; set; }
        public IList<IndexReferenceList> referenceList { get; set; }

    }
}