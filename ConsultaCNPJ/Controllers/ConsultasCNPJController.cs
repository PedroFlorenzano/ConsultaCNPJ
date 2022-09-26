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


        [HttpGet("Busca e Insere CNPJ")] //
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


        [HttpGet("Busca_CNPJ")] //
        public async Task<IActionResult> BuscaCNPJ (string cnpj) //uma interface dizendo que vai retornar um resultado. Assincrono = pode fazer ele ou outras coisas. Sincrono = faz uma unica coisa, ele quebra sua aplicação
        {
            var y = _dbContext.roots.Select(x => new Root { ID = x.ID, 
                atividade_principal = x.atividade_principal, 
                data_situacao = x.data_situacao, complemento = x.complemento, 
                tipo = x.tipo, nome = x.nome, abertura = x.abertura, telefone = x.telefone, 
                email = x.email, atividades_secundarias = x.atividades_secundarias, 
                qsa = x.qsa, situacao = x.situacao, bairro = x.bairro, logradouro = x.logradouro, 
                numero = x.numero, cep = x.cep, municipio = x.municipio, uf = x.uf, porte = x.porte, 
                natureza_juridica = x.natureza_juridica, fantasia = x.fantasia, cnpj = x.cnpj, 
                ultima_atualizacao = x.ultima_atualizacao, status = x.status, efr = x.efr, motivo_situacao = x.motivo_situacao, 
                situacao_especial = x.situacao_especial, data_situacao_especial = x.data_situacao_especial, 
                capital_social = x.capital_social }).ToList().AsReadOnly();
            
            return Ok(y);

        }



        [HttpDelete("Delete cnpj")]
        public async Task<IActionResult> Delete(string cnpj)
        {

            var x = _dbContext.roots.Include(x => x.atividade_principal).Include(x => x.atividades_secundarias).
                Include(x => x.qsa).FirstOrDefault(x => x.cnpj == cnpj);

            if (x == null)
            {
                return NotFound();

            }
            _dbContext.Remove(x);
            await _dbContext.SaveChangesAsync();
            return Ok("Deletado com Sucesso");
        }

    }
}
    