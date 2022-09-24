using ConsultaCNPJ.Data;
using ConsultaCNPJ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ConsultaCNPJ.Controllers
{

    [ApiController] //Definição que essa classe vai ser uma API controller
    [Route("v1/[controller]/")] //Essa vai ser a rota padrão de todos os end points dessa api

    public class ConsultasCNPJController : ControllerBase //serve para dizer que estou fazendo uma api , mas quero carregar apenas o necessário
    {
        private const string EndPointConsumo = "https://receitaws.com.br/v1/cnpj/"; //trata-se do endpoint. Só esse controle que pode ter acesso a esse endpoint. Ele nunca muda, por isso é const (constante)
        private readonly ApplicationDbContext _dbContext;
        public ConsultasCNPJController(ApplicationDbContext dbContext) //Fazendo um construtor da minha classe dbContext, que será o banco de dados
        {
            _dbContext = dbContext;

        }


        [HttpGet("CriaCNPJ")] //
        public async Task <IActionResult> CNPJ (string cnpj) //uma interface dizendo que vai retornar um resultado. Assincrono = pode fazer ele ou outras coisas. Sincrono = faz uma unica coisa, ele quebra sua aplicação
        {
            var httpclient = new HttpClient();
            var response = await httpclient.GetAsync(EndPointConsumo + cnpj); // buscando o cnpj na api
            var result = response.Content.ReadAsStringAsync().Result; // pegando só o corpo do 
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);  //esse vai ser o objeto que vamos passar para o Json
            _dbContext.Add(myDeserializedClass); //adicionando o json no banco de dados
            _dbContext.SaveChanges(); //salvando esses dados
            return Ok(new { msg = "Criado no banco com sucesso" });
            
        }


        [HttpGet("BuscaCNPJ")] //
        public async Task<IActionResult> BuscaCNPJ(string cnpj) //uma interface dizendo que vai retornar um resultado. Assincrono = pode fazer ele ou outras coisas. Sincrono = faz uma unica coisa, ele quebra sua aplicação
        {
            var httpclient = new HttpClient();
            var response = await httpclient.GetAsync(EndPointConsumo + cnpj); // buscando o cnpj na api
            var result = response.Content.ReadAsStringAsync().Result; // pegando só o corpo do 
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);  //esse vai ser o objeto que vamos passar para o Json
            if(myDeserializedClass == null)
            {
                return BadRequest(new { msg = "CNPJ inválido" });

            }
            _dbContext.Add(myDeserializedClass); //adicionando o json no banco de dados
            _dbContext.SaveChanges(); //salvando esses dados
            return Ok(new { msg = "Criado no banco com sucesso" });

        }


    }
}
