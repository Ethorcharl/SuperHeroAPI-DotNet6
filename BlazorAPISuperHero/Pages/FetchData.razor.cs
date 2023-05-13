using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace BlazorAPISuperHero.Pages
{
    public class FetchDataBase : ComponentBase // i add Base for FetchData
    {
        protected WeatherForecast[]? forecasts;

        protected List<SuperHero> SuperHeroes { get; set; }

        [Inject]
        private HttpClient Http { get; set; } = null!; // null! not 

        protected override async Task OnInitializedAsync()
        {
            forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
            SuperHeroes = await Http.GetFromJsonAsync<List<SuperHero>>("https://localhost:7016/api/SuperHero/Get");
        }

        public class WeatherForecast
        {
            public DateTime Date { get; set; }

            public int TemperatureC { get; set; }

            public string? Summary { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}
