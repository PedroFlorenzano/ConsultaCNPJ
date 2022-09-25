using System.ComponentModel.DataAnnotations;

namespace ConsultaCNPJ.Models
{
    public class Qsa
    {
        [Key]
        public int ID { get; set; }
        public string nome { get; set; }
        public string qual { get; set; }
        public Root root { get; set; }

    }
}
