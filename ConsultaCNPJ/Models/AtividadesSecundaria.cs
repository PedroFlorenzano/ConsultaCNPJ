using System.ComponentModel.DataAnnotations;

namespace ConsultaCNPJ.Models
{
    public class AtividadesSecundaria
    {
        [Key]
        public int ID { get; set; }
        public string text { get; set; }
        public string code { get; set; }
    }
}
