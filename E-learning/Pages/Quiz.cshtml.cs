using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nest;
using Newtonsoft.Json;

namespace E_learning.Pages
{
    public class QuizModel : PageModel
    {
        public int Score { get; set; }

        public class Result
        {
            public string category { get; set; }
            public string type { get; set; }
            public string difficulty { get; set; }
            public string question { get; set; }
            public string correct_answer { get; set; }
            public List<string> incorrect_answers { get; set; }

            public List<string> Answers
            {
                get
                {
                    var answers = new List<string>();
                    answers.Add(correct_answer);
                    answers.AddRange(incorrect_answers);
                    var shuffledAnswers = new List<string>();
                    Random random = new Random(DateTime.Now.Millisecond);
                    while (answers.Count > 0)
                    {
                        int index = random.Next(0, answers.Count);
                        shuffledAnswers.Add(answers[index]);
                        answers.RemoveAt(index);
                    }
                    return shuffledAnswers;
                }
            }
        }


        public class Root
        {
            public int response_code { get; set; }
            public List<Result> results { get; set; }
        }

        private readonly IHttpClientFactory _clientFactory;
        private object aresponse;

        public IEnumerable<Result> Questions { get; set; }
        public Result CurrentQuestion { get; set; }
        public QuizModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


  

        public async Task OnGet()
        {
            //Console.WriteLine("randomnr:" + randomNumbers + "\n");
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://opentdb.com/api.php?amount=1&category=18&type=multiple");
            string json = await response.Content.ReadAsStringAsync();
            var root = JsonConvert.DeserializeObject<Root>(json);
            Questions = (IEnumerable<Result>)root.results.ToList();
            CurrentQuestion = Questions.First();
            //string answer = CurrentQuestion.correct_answer;
            //HttpContext.Session.SetString("answer",answer);
           
            TempData["json"] = json;
            if (TempData["Score"] == null) { Score = 0; }
            else
            {
                Score = (int)TempData["Score"];
            }
        }
        public async Task<IActionResult> OnPostAsync(string choice)
        {
            string json= TempData["json"].ToString();
            var root = JsonConvert.DeserializeObject<Root>(json);
            Questions = (IEnumerable<Result>)root.results.ToList();
            CurrentQuestion = Questions.First();
            

            string answer = CurrentQuestion.correct_answer;
            TempData["json"] = json;


            if (TempData["Score"] == null) { Score = 0; }
            else
            {
                Score = (int)TempData["Score"];
            }
            if (choice == answer)
            {
                TempData["Message"] = "Correct! Answer is "+answer;
               

                Score++;
                TempData["Score"] = Score;


                //HttpContext.Session.SetString("answer",answer);

            }
            else
            {
                TempData["Message"] = "Wrong! Correct answer is " + answer;

            
            }

           
            var client = _clientFactory.CreateClient();
         var response = await client.GetAsync("https://opentdb.com/api.php?amount=1&category=18&type=multiple");
          json = await response.Content.ReadAsStringAsync();
            TempData["json"] = json;
            root = JsonConvert.DeserializeObject<Root>(json);
            Questions = (IEnumerable<Result>)root.results.ToList();
            CurrentQuestion = Questions.First();

            return Page();
        }
    }


}