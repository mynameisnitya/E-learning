using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using E_learning.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace E_learning.Pages
{ //[Authorize(Roles = "Administrator")]
    public class newTutorialModel : PageModel
    {
       
        private readonly TutorialContext _context;
        [BindProperty]
        public Tutorial Tutorial { get; set; }
        [BindProperty]
        public List<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
        public class Choices
        {
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
        }

        public class Datum
        {
            public string answer { get; set; }
            public string answer_text { get; set; }
            public string question { get; set; }
            public string type { get; set; }
            public Choices choices { get; set; }
            public string answer_info { get; set; }
        }

        public class Root
        {
            public List<Datum> data { get; set; }
        }

        public newTutorialModel(IHttpClientFactory httpClientFactory, TutorialContext context)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", " Bearer 68|ZZkIy2EkPMsxKWf9ym9CoNtvf5oTnuTQheh1i1ja");
        }

        public void OnGet()
        {

            Console.WriteLine("hello");
        }






        private readonly HttpClient _httpClient;

        public string Response { get; set; }



        public async Task OnPostAsync()

        {
            
            _context.Tutorials.Add(Tutorial);
            _context.SaveChanges();
            var apiUrl = "https://quizgecko.com/api/v1/questions";

            var quizcontent = Request.Form["content"].ToString();
            var questionType = "multiple_choice";

            var difficulty = Request.Form["difficulty"].ToString();

            var content = new StringContent(
                @$"{{

           


            ""question_type"": ""{questionType}"",
            ""text"": ""{quizcontent}"",
            ""count"": 4,
            ""difficulty"": ""{difficulty}""
        }}",
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(apiUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.Out.Write(responseContent);
            Console.Out.Write(quizcontent);
            // Deserialize the response JSON into a Quiz object
            var quiz = JsonConvert.DeserializeObject<Root>(responseContent);
            var quizQuestion = new QuizQuestion();

            if (quiz != null && quiz.data != null && quiz.data.Count > 0)
            {
                var question = quiz.data[0];

                var q = question.question;
                var correctAnswerLetter = question.answer;
                // var correctanws = question.choices?.answerletter;
                var A1 = "";
                var A2 = "";
                var A3 = "";
                var correctAnswer = "";

                switch (correctAnswerLetter)
                {
                    case "A":
                        A1 = question.choices?.B;
                        A2 = question.choices?.C;
                        A3 = question.choices?.D;
                        correctAnswer = question.choices.A;
                        break;
                    case "B":
                        A1 = question.choices?.A;
                        A2 = question.choices?.C;
                        A3 = question.choices?.D;
                        correctAnswer = question.choices.B;
                        break;
                    case "C":
                        A1 = question.choices?.A;
                        A2 = question.choices?.B;
                        A3 = question.choices?.D;
                        correctAnswer = question.choices.C;
                        break;
                    case "D":
                        A1 = question.choices?.A;
                        A2 = question.choices?.B;
                        A3 = question.choices?.C;
                        correctAnswer = question.choices.D;
                        break;
                }
                quizQuestion.Question = q;

                question.choices = null;

                //QuizQuestion
                quizQuestion.CorrectAnswer = correctAnswer;
                quizQuestion.Answer1 = A1;
                quizQuestion.Answer2 = A2;
                quizQuestion.Answer3 = A3;
                // Set the QuizQuestion's Tutorial property to the retrieved object






                // Add the quiz to the database


            }
            else {Console.WriteLine("empty");}
                quizQuestion.TutorialId = Tutorial.Id;

                _context.QuizQuestions.Add(quizQuestion);
                _context.SaveChanges();
                Response = "Quiz added to database.";
           

        }

    }
}
