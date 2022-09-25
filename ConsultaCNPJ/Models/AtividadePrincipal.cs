using System.ComponentModel.DataAnnotations;

namespace ConsultaCNPJ.Models
{
    public class AtividadePrincipal
    {
        [Key]
        public int ID { get; set; }
        public string text { get; set; }
        public string code { get; set; }

        public Root root { get; set;}
    }
}
