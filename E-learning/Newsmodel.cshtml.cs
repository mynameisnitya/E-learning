using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace E_learning
{
    // NewsModel.cs
    public class NewsModel : PageModel

    {

        public class News
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            public DateTime PublishedAt { get; set; }
            public string Author { get; set; }
            public string ArticleUrl { get; set; }
        }

        private readonly HttpClient _client = new HttpClient();
        public List<News> news { get; set; } = new List<News>();

        public async Task OnGetAsync()
        {
            var response = await _client.GetAsync("https://newsdata.io/api/1/news?apikey=pub_15648ba4c60456181e597f81d884adc7eb434&q=technology&language=en&category=technology");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            news = JsonConvert.DeserializeObject<List<News>>(json);
        }
    }

  


}
