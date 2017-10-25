using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDevProject.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string moduleTitle { get; set; }
        public string videoURL { get; set; }
        public string lectureText { get; set; }
        public int moduleOrder { get; set; }
    }
}
