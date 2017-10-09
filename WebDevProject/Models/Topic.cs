using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDevProject.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }
        public ICollection<Question> questionList { get; set; }
        public string topicTitle { get; set; }
        public string videoURL { get; set; }
        public string lectureText { get; set; }
        public int topicOrder { get; set; }
    }
}
