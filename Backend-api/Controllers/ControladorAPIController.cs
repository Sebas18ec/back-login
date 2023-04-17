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



        }
    }
    
}
