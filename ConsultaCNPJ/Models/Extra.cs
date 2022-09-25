using System.ComponentModel.DataAnnotations;

namespace ConsultaCNPJ.Models
{
    public class Extra
    {
        [Key]
        public int ID { get; set; }
        public Root root { get; set; }

    }
}
