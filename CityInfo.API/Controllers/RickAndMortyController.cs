using CityInfo.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using Microsoft.AspNetCore.Hosting.Server;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RickAndMortyController : Controller
    {

        [HttpGet("All-Character")]
        public async Task<RickAndMorty> Get()
        {
            List<RickAndMorty> InfoPersonaje = new List<RickAndMorty>();
            var url = "https://rickandmortyapi.com/api/";
            using (var request = new HttpClient())
            {
                request.BaseAddress = new Uri(url);
                request.DefaultRequestHeaders.Clear();
                request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await request.GetAsync("character");
                if (Res.IsSuccessStatusCode)
                {
                    var PersonajesResponse = Res.Content.ReadAsStringAsync().Result;
                    var listPersonajes = (RickAndMorty)Newtonsoft.Json.JsonConvert.DeserializeObject(PersonajesResponse, typeof(RickAndMorty));
                    return listPersonajes;
                }
            }
            
            return (RickAndMorty)Enumerable.Empty<RickAndMorty>();
        }
    }
}
