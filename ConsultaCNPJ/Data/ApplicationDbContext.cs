using ConsultaCNPJ.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConsultaCNPJ.Data
{
    //vai realizar a conexão com o meu banco de dados

    public class ApplicationDbContext : DbContext
    {
 
        public DbSet<Root>? roots { get; set; } //estou pegando os objetos do json e criando uma tabela no banco com o mesmo nome e inserindo eles

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //junta o MySQl e o .NET no banco de dados
        {

        }

        

    }

}
