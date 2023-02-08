using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using E_learning.Areas.Identity.Data;
using E_learning.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace E_learning
{
    public class CreateModel : PageModel
    {
        private readonly TutorialContext _context;
        //private readonly IHttpClientFactory _clientFactory;

        public CreateModel(TutorialContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
           // _clientFactory = clientFactory;
        }

        [BindProperty]
        public Tutorial Tutorial { get; set; }
      
        public QuizQuestion QuizQuestion { get; set; }
        //QuizAnswer

        HttpClient client = new HttpClient();
        

        public async Task<IActionResult> OnPostAsync()
        {

            _context.Tutorials.Add(Tutorial);
            await _context.SaveChangesAsync();




            client.DefaultRequestHeaders.Add("Authorization", "27|3JiVC8ohquNi3lFwhzvTY0U2nrGCxHQ4Fhlve6AS");
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                text = Tutorial.Content.ToString(),
                question_type = "multiple_choice",
                count = 5,
                difficulty = "easy"
            }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://quizgecko.com/api/v1/questions", content);
            var responseContent = await response.Content.ReadAsStringAsync();


            dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);

            var question = jsonResponse.questions[1].question;
            string correctanswer;
            List<string> otherAnswers = new List<string>();
            correctanswer = jsonResponse.questions[0].answer;
            foreach (var answer in jsonResponse.questions[0].options)
            {
                if (answer != correctanswer)
                {
                    otherAnswers.Add(answer);
                }
            }
            string answer1 = otherAnswers[0];
            string answer2 = otherAnswers[1];
            string answer3 = otherAnswers[1];


            if (!ModelState.IsValid)
            {
                return Page();
            }

        

           

   
           
            QuizQuestion.TutorialId = Tutorial.Id;
           QuizQuestion.Question = question;
           QuizQuestion.Answer1 = answer1;
            QuizQuestion.Answer2 = answer2;
            QuizQuestion.Answer3 = answer3;
            QuizQuestion.CorrectAnswer = correctanswer;
            _context.QuizQuestions.Update(QuizQuestion);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");



            //add an api call to guizgecko
            //and send the tutorial data
            //to it an get response and save to database



            return RedirectToPage("./Index");
        }

      
    }

}
