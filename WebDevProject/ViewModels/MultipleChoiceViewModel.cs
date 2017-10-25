using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDevProject.Models
{
    public class MultipleChoiceViewModel
    {
        public Question theQuestion { get; set; }
        public List<Question> questionList { get; set; }
        public int orderListLength { get; set; }
    }
}
