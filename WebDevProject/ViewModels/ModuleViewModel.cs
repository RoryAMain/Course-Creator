using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDevProject.Models
{
    public class ModuleViewModel
    {
        public IList<Topic> Topics { get; set; }
        public Module theModule { get; set; }
        public IList<ModuleReferenceList> referenceList { get; set; }
        public List<Module> moduleList { get; set; }
        public int orderListLength { get; set; }

    }
}
