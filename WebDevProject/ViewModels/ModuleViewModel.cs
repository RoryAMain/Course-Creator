﻿using System;
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
    }
}
