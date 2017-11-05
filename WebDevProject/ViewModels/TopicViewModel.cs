using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDevProject.Models
{
    public class TopicViewModel
    {
        public IList<Question> Questions { get; set; }
        public Topic theTopic { get; set; }
        public IList<TopicReferenceList> referenceList { get; set; }

    }
}
