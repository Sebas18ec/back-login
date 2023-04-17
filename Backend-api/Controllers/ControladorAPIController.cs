using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Backend_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Plugins;

namespace Backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorAPIController : ControllerBase
    {
        // GET: api/ControladorAPI
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ControladorAPI/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ControladorAPI
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ControladorAPI/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ControladorAPI/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("api/v1/emisores")]
        public async Task<ActionResult<List<Emisor>>> GetEmisoresAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://apiservicios.ecuasolmovsa.com:3009/api/Varios/GetEmisor");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return Ok(json);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        private readonly HttpClient _httpClient;

        public ControladorAPIController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModel login)
        {
            _httpClient.BaseAddress = new Uri("http://apiservicios.ecuasolmovsa.com:3009");

            var response = await _httpClient.GetAsync($"/api/Usuarios?usuario={login.usuario}&password={login.contrasena}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }

            /*_httpClient.BaseAddress = new Uri("http://apiservicios.ecuasolmovsa.com:3009");
            


            var response = await _httpClient.GetAsync($"/api/Usuarios?usuario={usuario}&password={password}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }
        
        try
        {
            var requestUrl = $"http://apiservicios.ecuasolmovsa.com:3009/api/Usuarios?usuario={HttpUtility.UrlEncode(login.Usuario)}&password={HttpUtility.UrlEncode(login.Contrasena)}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var responseJson = JObject.Parse(responseString);

                bool success = (bool)responseJson["success"];
                if (success)
                {
                    // Lógica para cuando el login es exitoso
                    return Ok(new { success = true });
                }
                else
                {
                    string error = (string)responseJson["error"];
                    // Lógica para cuando el login falla
                    return Unauthorized(new { success = false, error = error });
                }
            }
            else
            {
                // Lógica para manejar el error de la llamada a la API
                var errorContent = await response.Content.ReadAsStringAsync();
                return BadRequest($"Error al llamar a la API de autenticación. Detalles del error: {errorContent}");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocurrió un error al realizar la solicitud. Detalles del error: {ex.Message}");
        }*/
        }




        /*[HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                var requestUrl = $"http://apiservicios.ecuasolmovsa.com:3009/api/Usuarios?usuario={login.Usuario}&password={login.Contrasena}";
                var response = await _httpClient.GetAsync(requestUrl);
                //var response = await _httpClient.PostAsJsonAsync(requestUrl, null);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoginResult>(content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var responseJson = JObject.Parse(responseString);

                    bool success = (bool)responseJson["success"];
                    if (success)
                    {
                        // Lógica para cuando el login es exitoso
                        return Ok(new { success = true });
                    }
                    else
                    {
                        string error = (string)responseJson["error"];
                        // Lógica para cuando el login falla
                        return Unauthorized(new { success = false, error = error });
                    }
                }
                else
                {
                    // Lógica para manejar el error de la llamada a la API
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return BadRequest($"Error al llamar a la API de autenticación. Detalles del error: {errorContent}");
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrió un error al realizar la solicitud. Detalles del error: {ex.Message}");
            }
        }

        */


    }
    
}
