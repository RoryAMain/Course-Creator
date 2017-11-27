using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevProject.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Progress { get; set; }
        public int modulesCompleted { get; set; }
        public int topicsCompleted { get; set; }
        public double questionsCompleted { get; set; }
        public double numberOfQuestions { get; set; }
    }
}
