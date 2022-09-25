using System.ComponentModel.DataAnnotations;

namespace ConsultaCNPJ.Models
{
    public class Billing
    {
        [Key]
        public int ID { get; set; }
        public bool free { get; set; }
        public bool database { get; set; }

        public Root root {get; set;}
    }
}
