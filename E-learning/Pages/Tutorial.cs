using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_learning.Pages
{
    public class Tutorial
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public ICollection<QuizQuestion> QuizQuestions { get; set; }
    }

    public class QuizQuestion
    {
        public int Id { get; set; }
        public int TutorialId { get; set; }
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string CorrectAnswer { get; set; }
        public Tutorial Tutorial { get; set; }
    
    }

}
