
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace cnc_web.Controllers
{
    [ApiController]
    [Route("api/dane")]
    public class DaneController : ControllerBase
    {
        private readonly string sciezka = "dane.json";

        [HttpPost]
        public IActionResult Zapisz([FromBody] object dane)
        {
            var lista = new List<object>();
            if (System.IO.File.Exists(sciezka))
            {
                var zawartosc = System.IO.File.ReadAllText(sciezka);
                lista = JsonSerializer.Deserialize<List<object>>(zawartosc) ?? new List<object>();
            }
            lista.Add(dane);
            System.IO.File.WriteAllText(sciezka, JsonSerializer.Serialize(lista));
            return Ok(new { status = "dodano" });
        }

        [HttpGet]
        public IActionResult Pobierz()
        {
            if (!System.IO.File.Exists(sciezka))
                return Ok(new List<object>());
            var zawartosc = System.IO.File.ReadAllText(sciezka);
            var dane = JsonSerializer.Deserialize<List<object>>(zawartosc);
            return Ok(dane);
        }
    }
}
