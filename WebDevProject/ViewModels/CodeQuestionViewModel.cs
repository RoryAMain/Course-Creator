using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDevProject.Models
{
    public class CodeQuestionViewModel
    {
        public Question theQuestion { get; set; }
        public List<Question> questionList { get; set; }
        public int orderListLength { get; set; }
        public IList<QuestionReferenceList> referenceList { get; set; }
        public string pythonResult { get; set; }
        public string pythonError { get; set; }

    }
}
