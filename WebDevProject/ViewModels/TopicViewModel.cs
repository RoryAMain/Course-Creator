using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDevProject.Models
{
    public class TopicViewModel : BaseViewModel
    {
        public IList<Question> Questions { get; set; }
        public Topic theTopic { get; set; }
    }
}
